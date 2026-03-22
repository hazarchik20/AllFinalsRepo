using FinalProjectASP_Net.Core.Abstractions.IRepo;
using FinalProjectASP_Net.Core.Abstractions.IServ;
using FinalProjectASP_Net.Core.Models;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FinalProjectASP_Net.Application.Services
{
    public class VacancyServices : IVacancyServices
    {
        private readonly IVacancyRepository _repository;
        private readonly IDistributedCache _cache;

        public VacancyServices(IVacancyRepository repository, IDistributedCache cache)
        {
            _repository = repository;
            _cache = cache;
        }

        private string GetAllKey(int limit, int offset) => $"vacancies_all_{limit}_{offset}";
        private string GetActiveKey(int limit, int offset) => $"vacancies_active_{limit}_{offset}";
        private string GetByCompanyKey(int companyId, int limit, int offset)
            => $"vacancies_company_{companyId}_{limit}_{offset}";
        private string GetByIdKey(int id) => $"vacancy_{id}";

        public async Task<IEnumerable<Vacancy>> GetAll(int limit, int offset)
        {
            var cacheKey = GetAllKey(limit, offset);

            var cached = await _cache.GetStringAsync(cacheKey);

            if (cached != null)
            {
                return JsonSerializer.Deserialize<IEnumerable<Vacancy>>(cached)!;
            }

            var data = await _repository.GetAll(limit, offset);

            await SetCache(cacheKey, data);

            return data;
        }

        public async Task<IEnumerable<Vacancy>> GetActive(int limit, int offset)
        {
            var cacheKey = GetActiveKey(limit, offset);

            var cached = await _cache.GetStringAsync(cacheKey);

            if (cached != null)
            {
                return JsonSerializer.Deserialize<IEnumerable<Vacancy>>(cached)!;
            }

            var data = await _repository.GetActiveVacancies(limit, offset);

            await SetCache(cacheKey, data);

            return data;
        }

        public async Task<IEnumerable<Vacancy>> GetByCompany(int companyId, int limit, int offset)
        {
            var cacheKey = GetByCompanyKey(companyId, limit, offset);

            var cached = await _cache.GetStringAsync(cacheKey);

            if (cached != null)
            {
                return JsonSerializer.Deserialize<IEnumerable<Vacancy>>(cached)!;
            }

            var data = await _repository.GetByCompany(companyId, limit, offset);

            await SetCache(cacheKey, data);

            return data;
        }

        public async Task<Vacancy?> GetById(int id)
        {
            var cacheKey = GetByIdKey(id);

            var cached = await _cache.GetStringAsync(cacheKey);

            if (cached != null)
            {
                return JsonSerializer.Deserialize<Vacancy>(cached);
            }

            var vacancy = await _repository.GetById(id);

            if (vacancy != null)
            {
                await SetCache(cacheKey, vacancy);
            }

            return vacancy;
        }
        public async Task Add(Vacancy vacancy)
        {
            await _repository.Add(vacancy);

            await InvalidateCache(vacancy.Id, vacancy.CompanyId);
        }
        public async Task Update(Vacancy vacancy)
        {
            await _repository.Update(vacancy);

            await InvalidateCache(vacancy.Id, vacancy.CompanyId);
        }
        public async Task Delete(int id)
        {
            var vacancy = await _repository.GetById(id);
            if (vacancy == null) return;

            await _repository.Delete(vacancy);
            await InvalidateCache(vacancy.Id, vacancy.CompanyId);
        }

        private async Task SetCache<T>(string key, T data)
        {
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
            };

            await _cache.SetStringAsync(
                key,
                JsonSerializer.Serialize(data),
                options
            );
        }

        
        private async Task InvalidateCache(int vacancyId, int companyId)
        {
            await _cache.RemoveAsync(GetByIdKey(vacancyId));

            await _cache.RemoveAsync("vacancies_all_0_0");
            await _cache.RemoveAsync("vacancies_active_0_0");
            await _cache.RemoveAsync($"vacancies_company_{companyId}_0_0");
        }

    }
}
