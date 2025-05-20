using SmartEvent.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartEvent.Services
{
    public interface IUserService
    {
        Task<UserDTO> RegisterAsync(RegisterDTO registerDto);
        Task<UserDTO> LoginAsync(LoginDTO loginDto);
        Task<UserDTO> GetUserByIdAsync(string id);
        Task<UserDTO> GetUserByEmailAsync(string email);
        Task<IEnumerable<UserDTO>> GetAllUsersAsync();
        Task UpdateUserAsync(UserDTO userDto);
        Task DeleteUserAsync(string id);
    }
} 