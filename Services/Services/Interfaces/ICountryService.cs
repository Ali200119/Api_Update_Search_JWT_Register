using System;
using Services.DTOs.Country;

namespace Services.Services.Interfaces
{
	public interface ICountryService
	{
        Task<IEnumerable<CountryDto>> GetAllAsync();
        Task<CountryDto> GetByIdAsync(int? id);
        Task CreateAsync(CountryCreateDto country);
        Task DeleteAsync(int? id);
        Task UpdateAsync(CountryEditDto country, int? id);
        Task<IEnumerable<CountryDto>> Search(string searchText);
        Task SoftDeleteAsync(int? id);
    }
}