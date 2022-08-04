﻿using MyASPProject.Models;
using MyASPProject.Services.IRepository;
using Newtonsoft.Json;
using System.Text;

namespace MyASPProject.Services
{
    public class SamuraiServices : ISamurai
    {
        public async Task<IEnumerable<Samurai>> GetAll()
        {
            List<Samurai> samurais = new List<Samurai>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7276/api/Samurais"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    samurais = JsonConvert.DeserializeObject<List<Samurai>>(apiResponse);
                }
            }
            return samurais;
        }

        public async Task<Samurai> GetById(int id)
        {
            Samurai samurai = new();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"https://localhost:7276/api/Samurais/{id}"))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        samurai = JsonConvert.DeserializeObject<Samurai>(apiResponse);
                    }
                }
            }
            return samurai;
        }

        public async Task<Samurai> Insert(Samurai obj)
        {
            Samurai samurai = new();

            using (var httpClient = new HttpClient())
            {
                // Data yang maudikirim dalam bentuk json
                // Serialize
                StringContent content = new StringContent(
                    JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync("https://localhost:7276/api/Samurais", content))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.Created)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        samurai = JsonConvert.DeserializeObject<Samurai>(apiResponse);
                    }
                }
            }
            return samurai;
        }
    }
}