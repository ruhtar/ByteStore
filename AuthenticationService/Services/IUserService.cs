﻿using AuthenticationService.DTO;
using AuthenticationService.Entities;

namespace AuthenticationService.Services
{
    public interface IUserService
    {
        Task AddUser(User user);
        Task<bool> ValidateUser(User user);
        Task<UserDTO> GetUserByUsername(string username);
    }
}