using System;
using AutoMapper;
using Domain.Models;
using Repository.Repositories.Interfaces;
using Services.DTOs.Country;

namespace Services.Services
{
	public class CountryService
	{
        private readonly ICountryRepository _countryRepo;
        private readonly IMapper _mapper;

        public CountryService(ICountryRepository countryRepo, IMapper mapper)
        {
            _countryRepo = countryRepo;
            _mapper = mapper;
        }

        public async Task CreateAsync(CountryCreateDto employee) => await _countryRepo.CreateAsync(_mapper.Map<Country>(employee));

        public async Task<IEnumerable<CountryDto>> GetAllAsync() => _mapper.Map<IEnumerable<CountryDto>>(await _countryRepo.GetAllAsync());

        public async Task<CountryDto> GetByIdAsync(int? id) => _mapper.Map<CountryDto>(await _countryRepo.GetByIdAsync(id));

        public async Task DeleteAsync(int? id) => await _countryRepo.DeleteAsync(await _countryRepo.GetByIdAsync(id));

        //public async Task UpdateAsync(EmployeeEditDto employee) => await _employeeRepo.UpdateAsync(_mapper.Map<Employee>(employee));

        public async Task UpdateAsync(CountryEditDto country, int? id)
        {
            Country existedCountry = await _countryRepo.GetByIdAsync(id);

            await _countryRepo.UpdateAsync(_mapper.Map(country, existedCountry));
        }

        public async Task<CountryDto> SearchByName(string seacrhText) => _mapper.Map<CountryDto>(await _countryRepo.SearchByName(seacrhText));

        public async Task SoftDeleteAsync(int? id) => await _countryRepo.SoftDeleteAsync(await _countryRepo.GetByIdAsync(id));
    }
}