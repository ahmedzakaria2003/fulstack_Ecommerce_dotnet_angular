using AutoMapper;
using DomainLayer.Exceptions;
using DomainLayer.Models.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ServiceAbstraction;
using Shared.DataTransferObjects.IdentityDTOS;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class AuthenticationService(UserManager<ApplicationUser> _userManager
        , IConfiguration _configuration , IMapper _mapper) : IAuthenticationService
    {
        public async Task<UserDTO> LoginAsync(LoginDTO loginDTO)
        {
            //Check Email Is Exists
            var user =  await _userManager.FindByEmailAsync(loginDTO.Email);
            if (user is null)
            {
                throw new UserNotFoundException(loginDTO.Email);
            }

            //Check Password Is Correct
            var IsPassworValid = await _userManager.CheckPasswordAsync(user, loginDTO.Password);
            if (IsPassworValid)
            {
                //Return DTO
                return new UserDTO
                {
                    DisplayName = user.DisplayName,
                    Email = user.Email!,
                    Token = await CreateTokenAsync(user)
                };


            }
            else
            {
                throw new UnauthorizedException();
            }

        }

        private  async Task<string> CreateTokenAsync(ApplicationUser user)
        {
            var Claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier , user.Id),
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim(ClaimTypes.Name, user.UserName!)

            };

            var Roles = await _userManager.GetRolesAsync(user);

            foreach (var role in Roles)
            {
                Claims.Add(new Claim(ClaimTypes.Role , role));
            }

            var SecretKey = _configuration.GetSection("JWTOptions")["SecretKey"] ;

            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));  
            var Creds = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);
            var Token = new JwtSecurityToken
            (
                issuer: _configuration["JWTOptions:Issuer"]  ,
                audience: _configuration["JWTOptions:Audience"] ,
                claims: Claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: Creds
            );

            return new JwtSecurityTokenHandler().WriteToken(Token);

        }

      
        public async Task<UserDTO> RegisterAsync(RegisterDTO registerDTO)
        {
            // Mapping Register DTO => ApplicationUser
            var user = new ApplicationUser
            {
                DisplayName = registerDTO.DisplayName,
                Email = registerDTO.Email,
                UserName = registerDTO.Email
            };
            // Create ApplicationUser Object

            var Result = await _userManager.CreateAsync(user, registerDTO.Password);

            if (Result.Succeeded)
            {
                // Return UserDTO
                return new UserDTO
                {
                    DisplayName = user.DisplayName,
                    Email = user.Email,
                    Token = await CreateTokenAsync(user)
                };
            }
            else
            {
                // Throw Bad Request Exception If User Already Exists
                var Errors = Result.Errors.Select(e => e.Description).ToList();
                throw new BadRequestException(Errors);
            }
        }

        public async Task<bool> CheckEmailExistedAsync(string Email)
        {
            var User = await _userManager.FindByEmailAsync(Email);
            return User is not null;
        }

        public async Task<AdressDTO> GetCurrentUserAddressAsync(string Email)
        {
            var User = await _userManager.Users.Include(U => U.Address).FirstOrDefaultAsync(U => U.Email == Email) 
                ?? throw new UserNotFoundException(Email);
           
                return _mapper.Map<Address, AdressDTO>(User.Address);

        }

        public async Task<AdressDTO> UpdateCurrentUserAddressAsync(string Email, AdressDTO adressDTO)
        {
            var User = await _userManager.Users.Include(U => U.Address).FirstOrDefaultAsync(U => U.Email == Email)
              ?? throw new UserNotFoundException(Email);
            if (User.Address is not null)
            {  // Update

                User.Address.City = adressDTO.City;
                User.Address.Country = adressDTO.Country;
                User.Address.Street = adressDTO.Street;
                User.Address.FirstName = adressDTO.FirstName;
                User.Address.LastName = adressDTO.LastName;
            }

            else
            {  // Add To Db

                User.Address =  _mapper.Map<AdressDTO, Address>(adressDTO);


            }
                 
            
            await _userManager.UpdateAsync(User); // Update User In Db
            return _mapper.Map<AdressDTO>(User.Address);
        }

        public async Task<UserDTO> GetCurrentUserAsync(string Email)
        {
            var User = await _userManager.FindByEmailAsync(Email) ?? throw new UserNotFoundException(Email);

            return new UserDTO()
            {
                DisplayName = User.DisplayName,
                Email = User.Email!,
                Token = await CreateTokenAsync(User)

            };
        }
    }
}
