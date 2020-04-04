using GameOfLife.CSharp.Web.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace GameOfLife.CSharp.Web.Data
{
    public class PatternsService : IPatternsService
    {
        private readonly HttpClient _httpClient;

        public PatternsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ICollection<Pattern>> GetPattersByUserId(int userId)
        {
            var url = $"api/users/{userId}/patterns";

            _httpClient.BaseAddress = new Uri("https://localhost:44370");

            var response = await _httpClient.GetJsonAsync<ICollection<Pattern>>(url);

            return response;
        }
    }
}
