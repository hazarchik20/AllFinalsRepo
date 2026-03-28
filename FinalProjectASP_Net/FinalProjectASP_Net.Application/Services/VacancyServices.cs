using FinalProjectASP_Net.Core.Abstractions.Base;
using FinalProjectASP_Net.Core.Abstractions.IRepo;
using FinalProjectASP_Net.Core.Abstractions.IServ;
using FinalProjectASP_Net.Core.Models;
using FinalProjectASP_Net.Core.Models.RequestModels;
using FinalProjectASP_Net.Core.Models.ResponseModels;
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

        public async Task Add(ShortVacancyRequest vacancy)
        {
            var vacancyEntity = MapToMain(vacancy);
            await _repository.Add(vacancyEntity);

            await InvalidateCache(vacancyEntity.Id, vacancyEntity.CompanyId);
        }
        public async Task Update(int id, ShortVacancyRequest vacancy)
        {
            var existing = await _repository.GetById(id);
            if (existing == null)
                throw new Exception("Vacancy not found");
            var vacancyEntity = MapToMain(vacancy);

            await _repository.Update(id,vacancyEntity);

            await InvalidateCache(id, vacancyEntity.CompanyId);
        }
        public async Task Delete(int id)
        {
            var vacancy = await _repository.GetById(id);
            if (vacancy == null) return;

            await _repository.Delete(vacancy);
            await InvalidateCache(vacancy.Id, vacancy.CompanyId);
        }

        public async Task<IEnumerable<VacancyResponse>> GetAll(int limit, int offset)
        {
            var cacheKey = GetAllKey(limit, offset);
            var cached = await _cache.GetStringAsync(cacheKey);
            if (cached != null)
            {
                return JsonSerializer.Deserialize<IEnumerable<VacancyResponse>>(cached)!;
            }

            var data = await _repository.GetAll(limit, offset);
            await SetCache(cacheKey, data);

            var response = MapToResponse(data);
            return response;
        }

        public async Task<VacancyResponse?> GetById(int id)
        {
            var cacheKey = GetByIdKey(id);
            var cached = await _cache.GetStringAsync(cacheKey);
            if (cached != null)
            {
                return JsonSerializer.Deserialize<VacancyResponse>(cached);
            }

            var vacancy = await _repository.GetById(id);
            if (vacancy != null)
            {
                await SetCache(cacheKey, vacancy);
            }
            
            var response = MapToResponse(vacancy);
            return response;
        }

        public async Task<IEnumerable<VacancyResponse>> GetActive(int limit, int offset)
        {
            var cacheKey = GetActiveKey(limit, offset);
            var cached = await _cache.GetStringAsync(cacheKey);

            if (cached != null)
            {
                return JsonSerializer.Deserialize<IEnumerable<VacancyResponse>>(cached)!;
            }

            var data = await _repository.GetActiveVacancies(limit, offset);
            await SetCache(cacheKey, data);

            var response = MapToResponse(data);
            return response;
        }

        public async Task<IEnumerable<VacancyResponse>> GetByCompany(int companyId, int limit, int offset)
        {
            var cacheKey = GetByCompanyKey(companyId, limit, offset);

            var cached = await _cache.GetStringAsync(cacheKey);

            if (cached != null)
            {
                return JsonSerializer.Deserialize<IEnumerable<VacancyResponse>>(cached)!;
            }

            var data = await _repository.GetByCompany(companyId, limit, offset);
            await SetCache(cacheKey, data);

            var response = MapToResponse(data);
            return response;
        }
        private IEnumerable<VacancyResponse> MapToResponse(IEnumerable<Vacancy> vacancies) =>
            vacancies.Select(v => new VacancyResponse
            {
                Salary = v.Salary,
                Title = v.Title,
                Description = v.Description,
                CompanyId = v.CompanyId,
                Applications = v.Applications
            }).ToList();
        private VacancyResponse MapToResponse(Vacancy vacancies) =>
            new()
            {
                Salary = vacancies.Salary,
                Title = vacancies.Title,
                Description = vacancies.Description,
                CompanyId = vacancies.CompanyId,
                Applications = vacancies.Applications
            };
        private Vacancy MapToMain(ShortVacancyRequest request) =>
            new()
            {
                PostedDate = DateTime.UtcNow,
                IsActive = true,
                Salary = request.Salary,
                Title = request.Title,
                Description = request.Description,
                CompanyId = request.CompanyId,
                Applications = new List<Core.Models.Application>()

            };

    }
}
