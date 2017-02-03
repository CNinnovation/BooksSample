using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BooksViewModels.Services
{
    public class HttpHService : IHttpHService
    {
        private HttpClient _client;

        public HttpHService()
        {
            if (_client == null)
            {
                _client = new HttpClient();
            }
        }

        public async Task<IEnumerable<T>> GetItemsAsync<T>(string url)
        {
            var response = await _client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            string json = await response.Content.ReadAsStringAsync();
            var items = JsonConvert.DeserializeObject<IEnumerable<T>>(json);
            return items;
        }

        public async Task<T> GetItemById<T>(string url, int id)
        {
            var response = await _client.GetAsync($"{url}?id={id}");
            response.EnsureSuccessStatusCode();
            string json = await response.Content.ReadAsStringAsync();
            var item = JsonConvert.DeserializeObject<T>(json);
            return item;
        }

        public async Task<T> AddItem<T>(string url, T item)
        {
            string json = JsonConvert.SerializeObject(item);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(url, content);
            response.EnsureSuccessStatusCode();
            json = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<T>(json);
            return result;
        }

        public async Task UpdateItem<T>(string url, T item)
        {
            string json = JsonConvert.SerializeObject(item);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PutAsync(url, content);
            response.EnsureSuccessStatusCode();
        }

        public void Dispose()
        {
            _client?.Dispose();
        }
    }
}
