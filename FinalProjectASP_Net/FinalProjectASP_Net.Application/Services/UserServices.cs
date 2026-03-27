using FinalProjectASP_Net.Core.Abstractions.Base;
using FinalProjectASP_Net.Core.Abstractions.IRepo;
using FinalProjectASP_Net.Core.Abstractions.IServ;
using FinalProjectASP_Net.Core.Exceptions;
using FinalProjectASP_Net.Core.Models;
using FinalProjectASP_Net.Core.Models.RequestModels;
using FinalProjectASP_Net.Core.Models.ResponseModels;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace FinalProjectASP_Net.Application.Services
{
    public class UserServices : IUserServices
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
                Role = Role.HR
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
       

        public async Task<IEnumerable<UserResponse>> GetAll(int limit, int offset)
        {
            return MapToUserResponse(await _userRepository.GetAll(limit, offset));
        }

        public async Task<UserResponse?> GetById(int id)
        {
            return MapToUserResponse(await _userRepository.GetById(id)) ?? throw new InvalidCredentialsException();
        }
        public Task Add(UserBase entity)
        {
            throw new NotImplementedException("Use Register method to create a new user");
        }

        public async Task Update(int id,UserBase entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var existingUser = await _userRepository.GetById(entity.Id);

            existingUser.PasswordHash = BCrypt.Net.BCrypt.HashPassword(entity.PasswordHash);

            if (existingUser == null)
                throw new Exception("User not found");

            await _userRepository.Update(id, entity);

        }

        public async Task Delete(int id)
        {
            var user = await _userRepository.GetById(id);
            if (user == null)
                throw new UserNotFoundException();
            await _userRepository.Delete(user);
        }

        public async Task<IEnumerable<UserResponse>> GetEmployee(int limit, int offset)
        {
            var employees = await _userRepository.GetEmployee(limit, offset);
            var response = MapToUserResponse( employees);
            return response;
        }

        public async Task<IEnumerable<UserResponse>> GetHR(int limit, int offset)
        {
            var hrUsers = await _userRepository.GetHRUsers(limit, offset);
            var response = MapToUserResponse(hrUsers);
            return response;
        }

        private IEnumerable<UserResponse> MapToUserResponse(IEnumerable<HRUser> hrUsers)=>
             hrUsers.Select(u => new UserResponse
             {
                 Email = u.Email,
                 Name = u.Name,
                 Role = u.Role.ToString()
             });
        private IEnumerable<UserResponse> MapToUserResponse(IEnumerable<Employee> emplUser)=>
            emplUser.Select(u => new UserResponse
            {
                Email = u.Email,
                Name = u.Name,
                Role = u.Role.ToString()
            });

        private IEnumerable<UserResponse> MapToUserResponse(IEnumerable<UserBase> baseUser)=>
            baseUser.Select(u => new UserResponse
            {
                Email = u.Email,
                Name = u.Name,
                Role = u.Role.ToString()
            });

        private UserResponse MapToResponse(UserBase user, string? token = null) =>
           new()
           {
               Email = user.Email,
               Name = user.Name,
               Token = token,
               Role = user.Role.ToString()
           };
        private UserResponse MapToUserResponse(UserBase user)=>
            new()
            {
                Email = user.Email,
                Name = user.Name,
                Role = user.Role.ToString()
            };


    }
}
