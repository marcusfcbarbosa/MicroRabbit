# MicroRabbit
Microservices whit RabbitMQ
RabbitMq
	1- Download e Instalaçao
		Download
		https://www.rabbitmq.com/install-windows.html
		
		necessita desse pacote de implementações 
		https://www.erlang.org/downloads
		OTP 22.0 Windows 64-bit Binary File
=================================================		
  2- após instalação devemos ativar o Dashboard de administrador do RabbitMq
	 RabbitMq Command prompt
	 
	 rabbitmq-plugins enable rabbitmq_management
	 
	 por padrão esta rodando no 
	 http://localhost:15672/
	  
	  e por padrão possui login e senha 
	  login:guest
	  senha:guest
=================================================	  
	 3-Comandos em linha de codigo
		parar o applicativo
		rabbitmqctl stop_app
	 
		start o aplicativo
		rabbitmqctl start_app
		
		resetar
			rabbitmqctl stop_app
			rabbitmqctl reset
			rabbitmqctl start_app
			
		adicionar usuario
								 (login)(senha)
			rabbitmqctl add_user  rafael  123
		
		dando privilegios ao usuario
	
			rabbitmqctl set_user_tags rafael administrator
		
		atribuindo permissoes de leitura e escrita para tudo ao usario
			rabbitmqctl set_permissions -p / rafael ".*" ".*" ".*"
=================================================
4- Criando as migrations e rodando
Contexto de Banking
Apontando para .Data
add-migration "Initial Migration" -Context BankingDbContext	

update-database -Context BankingDbContext

Contexto de Transfer
Apontando para .Data
Add-Migration "Initial_migration_Transfer" -Context TransferDbContext

update-database -Context TransferDbContext
=================================================
