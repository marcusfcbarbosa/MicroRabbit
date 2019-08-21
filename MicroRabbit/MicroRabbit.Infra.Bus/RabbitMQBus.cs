using MediatR;
using MicroRabbit.Domain.Core.Bus;
using MicroRabbit.Domain.Core.Commands;
using MicroRabbit.Domain.Core.Events;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroRabbit.Infra.Bus
{
    public sealed class RabbitMQBus : IEventBus
    {
        private readonly IMediator _mediator;
        private readonly Dictionary<string, List<Type>> _handlers;
        private readonly List<Type> _eventTypes;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public RabbitMQBus(IMediator mediator, IServiceScopeFactory serviceScopeFactory)
        {
            _mediator = mediator;
            _handlers = new Dictionary<string, List<Type>>();
            _eventTypes = new List<Type>();

            _serviceScopeFactory = serviceScopeFactory;
        }

        //o mediator define para qual fila vai enviar o command
        public Task SendCommand<T>(T command) where T : Command
        {
            return _mediator.Send(command);
        }
        //usado por varios  microserviços para publicar eventos no servidor RabbitMQ
        //Para isso faz uso do RabbitMQ.Client
        public void Publish<T>(T @event) where T : Event
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost"
            };
            //enviando para a fila
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    var eventName = @event.GetType().Name;
                    channel.QueueDeclare(eventName, false, false, false, null);
                    var message = JsonConvert.SerializeObject(@event);
                    var body = Encoding.UTF8.GetBytes(message);
                    channel.BasicPublish("", eventName, null, body);
                }
            }
        }

        //quando um evento envia a serviços publicados
        //e para cada evento T , existe um eventHandler TH
        public void Subscribe<T, TH>()
            where T : Event
            where TH : IEventHandler<T>
        {
            var eventName = typeof(T).Name;
            var handlerType = typeof(TH);

            if (!_eventTypes.Contains(typeof(T)))
            {
                _eventTypes.Add(typeof(T));
            }

            if (!_handlers.ContainsKey(eventName))
            {
                _handlers.Add(eventName, new List<Type>());
            }

            if (_handlers[eventName].Any(s => s.GetType() == handlerType))
            {
                throw new ArgumentException($"Handler Type {handlerType.Name} já esta registrado  para o {eventName}.", nameof(handlerType));
            }
            _handlers[eventName].Add(handlerType);
            StartBasicConsume<T>();
        }

        private void StartBasicConsume<T>() where T : Event
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost"
                ,DispatchConsumersAsync = true
            };
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();
            var eventName = typeof(T).Name;
            channel.QueueDeclare(eventName, false, false, false, null);

            //faz uso do RabbitMQ.Client.Events
            var consumer = new AsyncEventingBasicConsumer(channel);

            consumer.Received += Consumer_Received;

            channel.BasicConsume(eventName, true, consumer);
        }

        private async Task Consumer_Received(object sender, BasicDeliverEventArgs @e)
        {
            var eventName = e.RoutingKey;
            var message = Encoding.UTF8.GetString(e.Body);

            try
            {
                await ProcessEvent(eventName, message).ConfigureAwait(false);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private async Task ProcessEvent(string eventName, string message)
        {
            if (_handlers.ContainsKey(eventName))
            {
                var subscriptions = _handlers[eventName];
                using (var scope = _serviceScopeFactory.CreateScope()) {
                    foreach (var subscription in subscriptions)
                    {
                        //dessa forma pode-se usar um EventHandler, que não tenha um construtor vazio
                        var handler = scope.ServiceProvider.GetService(subscription);
                        if (handler == null) continue;

                        var eventype = _eventTypes.SingleOrDefault(t => t.Name == eventName);
                        var @event = JsonConvert.DeserializeObject(message, eventype);
                        var concreteType = typeof(IEventHandler<>).MakeGenericType(eventype);
                        await (Task)concreteType.GetMethod("Handle").Invoke(handler, new object[] { @event });
                    }
                }
            }
        }

        //private async Task ProcessEvent(string eventName, string message)
        //{
        //    if (_handlers.ContainsKey(eventName))
        //    {
        //        var subscriptions = _handlers[eventName];
        //        foreach (var subscription in subscriptions)
        //        {
        //            //Para que o Activator.CreateInstance possa ocorrer necessita que o 
        //            //EventHandler em questao tenha um construtor sem parametros
        //            // o que nao permite que se faça uma injeçao de dependencia
        //            var handler = Activator.CreateInstance(subscription);

        //            if (handler == null) continue;

        //            var eventype = _eventTypes.SingleOrDefault(t => t.Name == eventName);
        //            var @event = JsonConvert.DeserializeObject(message, eventype);
        //            var concreteType = typeof(IEventHandler<>).MakeGenericType(eventype);
        //            await (Task)concreteType.GetMethod("Handle").Invoke(handler, new object[] { @event });
        //        }
        //    }
        //}
    }
}