using System;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Services.DTOs.Account;
using Services.Helpers.Responses;
using Services.Services.Interfaces;

namespace Services.Services
{
	public class AccountService: IAccountService
	{
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public AccountService(UserManager<AppUser> userManager,
                              IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }



        public async Task<AuthResponse> SignUpAsync(RegisterDto model)
        {
            AppUser user = _mapper.Map<AppUser>(model);

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                return new AuthResponse
                {
                    StatusMessage = "Failed",
                    Errors = result.Errors.Select(e => e.Description).ToList()
                };
            }

            return new AuthResponse
            {
                StatusMessage = "Success",
                Errors = null
            };
        }
    }
}