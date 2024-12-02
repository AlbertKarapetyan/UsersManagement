using DTO;

namespace Domain.Interfaces
{
    public interface IUserService
    {
        Task<string?> AuthenticateAsync(string username, string password);
        Task<UserDTO?> GetAsync(int id);
        Task<UserDTO?> GetAsync(string token);
        Task<IEnumerable<UserDTO>> GetAllAsync();
        Task<IEnumerable<UserDTO>> UpdateStateAsync(IEnumerable<UserUpdateStateDTO> modifiedUserStates);

    }
}
