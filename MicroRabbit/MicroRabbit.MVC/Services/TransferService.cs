using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using MicroRabbit.MVC.Models.DTO;
using Newtonsoft.Json;

namespace MicroRabbit.MVC.Services
{
    public class TransferService : ITransferService
    {
        private readonly HttpClient _httpClient;
        public TransferService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task Transfer(TransferDTO dto)
        {
            var uri = "https://localhost:44393/api/Banking";
            var transferContent = new StringContent(JsonConvert.SerializeObject(dto),
                System.Text.Encoding.UTF8,"application/json");

            var reponse = await _httpClient.PostAsync(uri, transferContent);
            reponse.EnsureSuccessStatusCode();
        }
    }
}
