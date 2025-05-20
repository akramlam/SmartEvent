using SmartEvent.Core.Models;
using SmartEvent.Data.Repositories;
using SmartEvent.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartEvent.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDTO> RegisterAsync(RegisterDTO registerDto)
        {
            // Check if email is already taken
            bool isEmailUnique = await _userRepository.IsEmailUniqueAsync(registerDto.Email);
            if (!isEmailUnique)
                throw new Exception("Email is already registered");

            // Validate password
            if (registerDto.Password != registerDto.ConfirmPassword)
                throw new Exception("Passwords do not match");

            // Hash password
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);

            // Create user
            var user = new User
            {
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Email = registerDto.Email,
                PasswordHash = passwordHash,
                IsActive = true,
                CreatedAt = DateTime.Now
            };

            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();

            return new UserDTO
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email
            };
        }

        public async Task<UserDTO> LoginAsync(LoginDTO loginDto)
        {
            var user = await _userRepository.GetByEmailAsync(loginDto.Email);
            if (user == null)
                throw new Exception("Invalid email or password");

            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash);
            if (!isPasswordValid)
                throw new Exception("Invalid email or password");

            if (!user.IsActive)
                throw new Exception("Account is disabled");

            // Update last login time
            user.LastLogin = DateTime.Now;
            await _userRepository.UpdateAsync(user);
            await _userRepository.SaveChangesAsync();

            return new UserDTO
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email
            };
        }

        public async Task<UserDTO> GetUserByIdAsync(string id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
                return null;

            return new UserDTO
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email
            };
        }

        public async Task<UserDTO> GetUserByEmailAsync(string email)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            if (user == null)
                return null;

            return new UserDTO
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email
            };
        }

        public async Task<IEnumerable<UserDTO>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return users.Select(u => new UserDTO
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email
            });
        }

        public async Task UpdateUserAsync(UserDTO userDto)
        {
            var user = await _userRepository.GetByIdAsync(userDto.Id);
            if (user == null)
                throw new Exception($"User with ID {userDto.Id} not found");

            user.FirstName = userDto.FirstName;
            user.LastName = userDto.LastName;
            
            // Check if email is changed and if it's unique
            if (user.Email != userDto.Email)
            {
                bool isEmailUnique = await _userRepository.IsEmailUniqueAsync(userDto.Email);
                if (!isEmailUnique)
                    throw new Exception("Email is already taken");
                
                user.Email = userDto.Email;
            }

            await _userRepository.UpdateAsync(user);
            await _userRepository.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(string id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
                throw new Exception($"User with ID {id} not found");

            await _userRepository.RemoveAsync(user);
            await _userRepository.SaveChangesAsync();
        }
    }
} 