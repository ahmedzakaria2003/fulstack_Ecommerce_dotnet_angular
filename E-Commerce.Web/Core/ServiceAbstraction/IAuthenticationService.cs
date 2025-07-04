using Shared.DataTransferObjects.IdentityDTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction
{
    public interface IAuthenticationService
    {

        Task<UserDTO> LoginAsync(LoginDTO loginDTO);

        Task<UserDTO> RegisterAsync(RegisterDTO registerDTO);
        // Check Email
        // Take string email then rerturn bool
        Task<bool>CheckEmailExistedAsync(string Email);

        // Get Current User Address 
        // Take String Email then Return AddressDTO 

        Task<AdressDTO> GetCurrentUserAddressAsync(string Email);

        // UpdateCurrent User Address 
        // Take AddressDTO Updated Address then Return AddressDTO Address after Update 
        Task<AdressDTO> UpdateCurrentUserAddressAsync(string Email, AdressDTO adressDTO);

        // Get Current User 
        // Take String Email then Return UserDTO Current User [Token , Email , DisplayName]

        Task<UserDTO> GetCurrentUserAsync(string Email);

    }
}
