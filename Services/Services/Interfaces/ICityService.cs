using System;
using Services.DTOs.City;

namespace Services.Services.Interfaces
{
	public interface ICityService
	{
        Task<IEnumerable<CityDto>> GetAllAsync();
        Task<CityDto> GetByIdAsync(int? id);
        Task CreateAsync(CityCreateDto country);
        Task DeleteAsync(int? id);
        Task UpdateAsync(CityEditDto country, int? id);
        Task<IEnumerable<CityDto>> Search(string searchText);
        Task SoftDeleteAsync(int? id);
    }
}