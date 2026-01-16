using DLL.Interfaces;
using DLL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Repository
{
    public class CryptoAPIRepository : IRpository
    {
        public async Task<CryptoMonet> AddCryptoAsync(CryptoMonet crypto)
        {
            var httpClient = new HttpClient();
            string uri = "http://localhost:12000";
            httpClient.BaseAddress = new Uri(uri);
            httpClient.Timeout = TimeSpan.FromMinutes(5);
            var response = await httpClient.PostAsJsonAsync("/cryptoToken", crypto);

            if (response.IsSuccessStatusCode)
            {
                var returnedCrypto = await response.Content.ReadFromJsonAsync<CryptoMonet>();
            }

            return crypto;
        }

        public async Task DeleteCryptoAsync(int id)
        {
            var httpClient = new HttpClient();
            string uri = "http://localhost:12000/";
            httpClient.BaseAddress = new Uri(uri);
            httpClient.Timeout = TimeSpan.FromMinutes(5);
            var response = await httpClient.DeleteAsync($"/cryptoToken{id}");

            if (response.IsSuccessStatusCode)
            {
                var returnedCrypto = await response.Content.ReadFromJsonAsync<CryptoMonet>();
            }
        }

        public async Task<CryptoMonet[]> Get200CryptosAsync()
        {
            var httpClient = new HttpClient();
            string uri = "http://localhost:12000/";
            httpClient.BaseAddress = new Uri(uri);
            httpClient.Timeout = TimeSpan.FromMinutes(5);
            var response = await httpClient.GetAsync($"/cryptoToken/latest");
            if (response.IsSuccessStatusCode)
            {
                var returnedCrypto = await response.Content.ReadFromJsonAsync<CryptoMonet[]>();
                return returnedCrypto;
            }
            return null;
        }

        public async Task<CryptoMonet> GetCryptoByIdAsync(int id)
        {
            var httpClient = new HttpClient();
            string uri = "http://localhost:12000/";
            httpClient.BaseAddress = new Uri(uri);
            httpClient.Timeout = TimeSpan.FromMinutes(5);
            var response = await httpClient.GetAsync($"/cryptoToken/{id}");
            if (response.IsSuccessStatusCode)
            {
                var returnedCrypto = await response.Content.ReadFromJsonAsync<CryptoMonet>();
                return returnedCrypto;
            }
            return null;
        }

        public async Task<CryptoMonet> GetCryptosBySymbolAsync(string Symble)
        {
            var httpClient = new HttpClient();
            string uri = "http://localhost:12000/";
            httpClient.BaseAddress = new Uri(uri);
            httpClient.Timeout = TimeSpan.FromMinutes(5);
            var response = await httpClient.GetAsync($"/cryptoToken?symbol={Symble}");
            if (response.IsSuccessStatusCode)
            {
                var returnedCrypto = await response.Content.ReadFromJsonAsync<CryptoMonet>();
                return returnedCrypto;
            }
            return null;
        }

        public async Task<CryptoMonet[]> SortCryptosAsync()
        {
            var httpClient = new HttpClient();
            string uri = "http://localhost:12000/";
            httpClient.BaseAddress = new Uri(uri);
            httpClient.Timeout = TimeSpan.FromMinutes(5);
            var response = await httpClient.GetAsync($"/cryptoToken/sort");
            if (response.IsSuccessStatusCode)
            {
                var returnedCrypto = await response.Content.ReadFromJsonAsync<CryptoMonet[]>();
                return returnedCrypto;
            }
            return null;
        }

        public async Task<CryptoMonet> UpdateCryptoAsync(CryptoMonet crypto)
        {
            var httpClient = new HttpClient();
            string uri = "http://localhost:12000/";
            httpClient.BaseAddress = new Uri(uri);
            httpClient.Timeout = TimeSpan.FromMinutes(5);
            var response = await httpClient.PostAsJsonAsync("cryptoToken", crypto);

            if (response.IsSuccessStatusCode)
            {
                var returnedCrypto = await response.Content.ReadFromJsonAsync<CryptoMonet>();
            }
            return crypto;
        }

        public async Task<CryptoMonet> СhangeСurrencyCryptosAsync(string Symble, string Convert)
        {
            var httpClient = new HttpClient();
            string uri = "http://localhost:12000/";
            httpClient.BaseAddress = new Uri(uri);
            httpClient.Timeout = TimeSpan.FromMinutes(5);
            var response = await httpClient.GetAsync($"/cryptoToken?symbol={Symble}&convert={Convert}");
            if (response.IsSuccessStatusCode)
            {
                var returnedCrypto = await response.Content.ReadFromJsonAsync<CryptoMonet>();
                return returnedCrypto;
            }
            return null;
        }
    }
}
