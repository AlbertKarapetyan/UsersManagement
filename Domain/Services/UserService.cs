using AutoMapper;
using Domain.Helpers;
using Domain.Interfaces;
using DTO;
using Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;

namespace Domain.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public UserService(IUserRepository userRepository, IMapper mapper, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<string?> AuthenticateAsync(string username, string password)
        {
            var pswd = Methods.ComputeSha256Hash(password);

            var user = await _userRepository.CheckLoginAndPasswordAsync(username, pswd);
            if (user == null)
            {
                return null;
            }

            if (!user.IsActive)
            {
                throw new UnauthorizedAccessException();
            }

            if(_configuration["JWTSecurityKey"] == null)
                throw new ArgumentNullException("JWTSecurityKey not found!");

            return JwtSecurity.GenerateJwtToken(user.Id, _configuration["JWTSecurityKey"]!);
        }

        public async Task<IEnumerable<UserDTO>> GetAllAsync()
        {
            return _mapper.Map<List<UserDTO>>(await _userRepository.GetAllAsync());
        }

        public async Task<UserDTO?> GetAsync(int id)
        {
            var user = await _userRepository.GetAsync(id);
            if (user == null)
            {
                return null;
            }

            return _mapper.Map<UserDTO>(user);
        }

        public async Task<UserDTO?> GetAsync(string token)
        {
            var userId = JwtSecurity.GetClaimFromDecodedJwt("userId", token);
            if (userId == null)
            {
                return null;
            }

            return _mapper.Map<UserDTO>(await this.GetAsync(Convert.ToInt32(userId)));
        }

        public async Task<IEnumerable<UserDTO>> UpdateStateAsync(IEnumerable<UserUpdateStateDTO> modifiedUserStates)
        {
            if (modifiedUserStates == null || !modifiedUserStates.Any())
            {
                throw new ArgumentException("The collection of modified user states cannot be null or empty.");
            }

            var updateTasks = modifiedUserStates
                .Select(userUpdateState => _userRepository.UpdateStateAsync(userUpdateState.Id, userUpdateState.NewState));

            try
            {
                // Perform all updates concurrently
                await Task.WhenAll(updateTasks);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating user states.", ex);
            }

            return await GetAllAsync();
        }
    }
}
