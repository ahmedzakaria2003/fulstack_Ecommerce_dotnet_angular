using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.DataTransferObjects.IdentityDTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
   
    public class AuthenticationController(IServiceManager _serviceManager) : ApiBaseController
    {
        [HttpPost("Login")]
        public async Task<ActionResult> Login(LoginDTO loginDTO)
        {
         
            var user = await _serviceManager.AuthenticationService.LoginAsync(loginDTO);
            return Ok(user);
        }

        [HttpPost("Register")]

        public async Task<ActionResult> Register(RegisterDTO registerDTO)
        {
            var user = await _serviceManager.AuthenticationService.RegisterAsync(registerDTO);
            return Ok(user);
        }

        // Check Email Existed

        [HttpGet("emailexists")]

        public async Task<ActionResult<bool>> CheckEmailExisted(string email)
        {
            var isExisted = await _serviceManager.AuthenticationService.CheckEmailExistedAsync(email);
            return Ok(isExisted);
        }

        // Get Current User 
        [Authorize]
        [HttpGet("CurrentUser")]

        public async Task<ActionResult<UserDTO>> GetCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
          
            var user = await _serviceManager.AuthenticationService.GetCurrentUserAsync(email!);
            return Ok(user);
        }
        // Get Current User Address
        [Authorize]
        [HttpGet("Address")]
        public async Task<ActionResult<AdressDTO>> GetCurrentUserAddress()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var address = await _serviceManager.AuthenticationService.GetCurrentUserAddressAsync(email!);
            return Ok(address);
        }

        // Update Current User Address
        [Authorize]
        [HttpPut("Address")]
        
        public async Task<ActionResult<AdressDTO>> UpdateCurrentUserAddress(AdressDTO adressDTO)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var updatedAddress = await _serviceManager.AuthenticationService
                .UpdateCurrentUserAddressAsync(email!, adressDTO);
            return Ok(updatedAddress);
        }




    }
}
