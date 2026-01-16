using Azure;
using DLL.Interfaces;
using DLL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DLL.Reposytory
{
    public class CryptoRepository : ICryptoRepository
    {
        private string _path = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName + "\\Client_HZR\\LogFolder\\LogTxt";
        private readonly DataContext _context;
       
        public CryptoRepository(DataContext context)
        {
            _context = context;
            
        }

       
        public async Task<CryptoMonet> AddCryptoAsync(CryptoMonet crypto)
        {
            _context.Add(crypto);
            await _context.SaveChangesAsync();
            LogActoins(_path, DateTime.Now + " Crypto Monet Added");
            return crypto;
        }

        public async Task DeleteCryptoAsync(int id)
        {
            var crypto = await GetCryptoByIdAsync(id);
            _context.Remove(crypto);
            LogActoins(_path, DateTime.Now + " Crypto Monet Added");
            await _context.SaveChangesAsync();

        }

        public async Task<CryptoMonet[]> Get200CryptosAsync()
        {
            var _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("http://localhost:12000");
            _httpClient.DefaultRequestHeaders.Add("X-CMC_PRO_API_KEY", "bda10759-8e47-437c-8e58-3c62fb888a9e");
            _httpClient.DefaultRequestHeaders.Add("Accepts", "application/json");
            var response = await _httpClient.GetAsync($"/v1/cryptocurrency/listings/latest?limit=200");
            var content = await response.Content.ReadAsStringAsync();
            var Monet = System.Text.Json.JsonSerializer.Deserialize<CmcResponseSingle<CryptoMonet[]>>(content);

            return Monet.data;
        }

        public async Task<CryptoMonet> GetCryptoByIdAsync(int id)
        {
            return await _context.Set<CryptoMonet>().FirstAsync(x => x.id == id);
        }

        public async Task<CryptoMonet> GetCryptosBySymbolAsync(string Symbol)
        {
            try
            {
                var _httpClient = new HttpClient();
                _httpClient.BaseAddress = new Uri("http://localhost:12000");
                _httpClient.DefaultRequestHeaders.Add("X-CMC_PRO_API_KEY", "bda10759-8e47-437c-8e58-3c62fb888a9e");
                _httpClient.DefaultRequestHeaders.Add("Accepts", "application/json");
                var response = await _httpClient.GetAsync($"/v1/cryptocurrency/quotes/latest?symbol={Symbol}");
                var content = await response.Content.ReadAsStringAsync();
                var Monet = JsonSerializer.Deserialize<CmcResponseDictionary<CryptoMonet>>(content);
                if (Monet?.data != null && Monet.data.ContainsKey(Symbol))
                {
                    return Monet.data[Symbol];
                }
                Console.WriteLine("Symbol not found in API response.");
            }
            catch (Exception ex)
            {
                return null;
            }
            return await _context.Set<CryptoMonet>().FirstAsync(x => x.symbol == Symbol);
        }

        public async Task<CryptoMonet[]> SortCryptosAsync()
        {
            var MonetsApi = await Get200CryptosAsync();
            var MonetsBD = await _context.Set<CryptoMonet>()
                .OrderBy(x => x.name)
                .ToArrayAsync();
            return MonetsApi
                .Concat(MonetsBD)
                .GroupBy(x => x.symbol)
                .Select(g => g.First())
                .OrderBy(x => x.name)
                .ToArray();
        }
         public async Task<CryptoMonet[]> GettAll()
        { 
            return await _context.Set<CryptoMonet>().ToArrayAsync();
        }

        public async Task<CryptoMonet> UpdateCryptoAsync( CryptoMonet crypto)
        {
            _context.Update(crypto);
            await _context.SaveChangesAsync();
            LogActoins(_path, DateTime.Now + " Crypto Monet Added");
            return crypto;
        }

        public async Task<CryptoMonet> СhangeСurrencyCryptosAsync(string Symbol, string Convert)
        {
            try
            {
                var _httpClient = new HttpClient();
                _httpClient.BaseAddress = new Uri("http://localhost:12000");
                _httpClient.DefaultRequestHeaders.Add("X-CMC_PRO_API_KEY", "bda10759-8e47-437c-8e58-3c62fb888a9e");
                _httpClient.DefaultRequestHeaders.Add("Accepts", "application/json");
                var response = await _httpClient.GetAsync($"/v1/cryptocurrency/quotes/latest?symbol={Symbol}&convert={Convert}");
                var content = await response.Content.ReadAsStringAsync();
                var Monet = JsonSerializer.Deserialize<CmcResponseDictionary<CryptoMonet>>(content);
                if (Monet?.data != null && Monet.data.ContainsKey(Symbol))
                {
                    return Monet.data[Symbol];
                }
            }
            catch (Exception ex) 
            {

                Console.WriteLine(ex.Message);
                return null;
            }
            return await _context.Set<CryptoMonet>().FirstOrDefaultAsync(x => x.symbol == Symbol);
        }
        public async Task LogActoins(string path, string Text)
        {
            using (StreamWriter sw = new StreamWriter(path, true))
            {
                sw.WriteLine(Text);
            }
        }
    }
}
