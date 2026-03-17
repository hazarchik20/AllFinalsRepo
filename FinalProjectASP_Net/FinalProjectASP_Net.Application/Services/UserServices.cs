using FinalProjectASP_Net.Core.Abstractions.IRepo;
using FinalProjectASP_Net.Core.Exceptions;
using FinalProjectASP_Net.Core.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace FinalProjectASP_Net.Application.Services
{
    public class UserServices
    {

        private readonly IUserRepository _userRepository;
        private readonly JwtTokenGenerator _jwtTokenGenerator;
        private readonly ILogger<UserServices> _logger;
        public UserServices(IUserRepository userRepository, JwtTokenGenerator jwtTokenGenerator, ILogger<UserServices> logger)
        {
            _userRepository = userRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
            _logger = logger;
        }
        public async Task<UserResponse> Register(RegisterUserRequest request)
        {
            if (await _userRepository.IsEmailTaken(request.Email))
            {
                throw new EmailAlreadyTakenException();
            }

            var user = new Employee
            {
                Name = request.Username,
                Email = request.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                Role = Role.Employee
            };



            await _userRepository.Add(user);
            _logger.LogInformation("User registered. UserId: {UserId}", user.Id);

            var token = _jwtTokenGenerator.GenerateToken(user);

            return MapToResponse(user, token);
        }

        public async Task<UserResponse> Login(LoginUserRequest request)
        {
            var user = await _userRepository.GetByEmail(request.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                _logger.LogWarning("Failed login attempt for Email: {Email}", request.Email);
                throw new InvalidCredentialsException();
            }

            _logger.LogInformation("User logged in. UserId: {UserId}", user.Id);

            var token = _jwtTokenGenerator.GenerateToken(user);

            return MapToResponse(user, token);
        }
        private UserResponse MapToResponse(UserBase user, string? token = null) =>
            new()
            {
                Email = user.Email,
                Name = user.Name,
                Token = token,
                Role = user.Role.ToString() 
            };
    }
}
