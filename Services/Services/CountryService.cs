using System;
using AutoMapper;
using Domain.Models;
using Repository.Repositories.Interfaces;
using Services.DTOs.Country;
using Services.Services.Interfaces;

namespace Services.Services
{
	public class CountryService: ICountryService
	{
        private readonly ICountryRepository _countryRepo;
        private readonly IMapper _mapper;

        public CountryService(ICountryRepository countryRepo,
                              IMapper mapper)
        {
            _countryRepo = countryRepo;
            _mapper = mapper;
        }

        public async Task CreateAsync(CountryCreateDto country) => await _countryRepo.CreateAsync(_mapper.Map<Country>(country));

        public async Task<IEnumerable<CountryDto>> GetAllAsync() => _mapper.Map<IEnumerable<CountryDto>>(await _countryRepo.FindAllAsync());

        public async Task<CountryDto> GetByIdAsync(int? id) => _mapper.Map<CountryDto>(await _countryRepo.GetByIdAsync(id));

        public async Task DeleteAsync(int? id) => await _countryRepo.DeleteAsync(await _countryRepo.GetByIdAsync(id));

        //public async Task UpdateAsync(EmployeeEditDto employee) => await _employeeRepo.UpdateAsync(_mapper.Map<Employee>(employee));

        public async Task UpdateAsync(CountryEditDto country, int? id)
        {
            Country existedCountry = await _countryRepo.GetByIdAsync(id);

            await _countryRepo.UpdateAsync(_mapper.Map(country, existedCountry));
        }

        public async Task<IEnumerable<CountryDto>> Search(string seacrhText) => _mapper.Map<IEnumerable<CountryDto>>(await _countryRepo.FindAllAsync(e => e.Name.ToLower().Trim().Contains(seacrhText.ToLower().Trim())));

        public async Task SoftDeleteAsync(int? id) => await _countryRepo.SoftDeleteAsync(await _countryRepo.GetByIdAsync(id));
    }
}