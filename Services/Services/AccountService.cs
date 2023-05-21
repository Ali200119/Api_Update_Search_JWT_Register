using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
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

        public async Task<LoginResponse> SignInAsync(LoginDto model)
        {
            throw new NotImplementedException();
        }

        public async Task<RegisterResponse> SignUpAsync(RegisterDto model)
        {
            AppUser user = _mapper.Map<AppUser>(model);

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                return new RegisterResponse
                {
                    StatusMessage = "Failed",
                    Errors = result.Errors.Select(e => e.Description).ToList()
                };
            }

            return new RegisterResponse
            {
                StatusMessage = "Success",
                Errors = null
            };
        }

        //private string GenerateJwtToken(string username, List<string> roles)
        //{
        //    var claims = new List<Claim>
        //    {
        //        new Claim(JwtRegisteredClaimNames.Sub, username),
        //        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        //        new Claim(ClaimTypes.NameIdentifier, username)
        //    };

        //    roles.ForEach(role =>
        //    {
        //        claims.Add(new Claim(ClaimTypes.Role, role));
        //    });

        //    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtKey"]));
        //    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        //    var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["JwtExpireDays"]));

        //    var token = new JwtSecurityToken(
        //        _configuration["JwtIssuer"],
        //        _configuration["JwtIssuer"],
        //        claims,
        //        expires: expires,
        //        signingCredentials: creds
        //    );

        //    return new JwtSecurityTokenHandler().WriteToken(token);
        //}
    }
}