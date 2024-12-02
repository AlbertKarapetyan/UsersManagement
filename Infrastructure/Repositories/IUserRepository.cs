using Infrastructure.DataEntities;

namespace Infrastructure.Repositories
{
    public interface IUserRepository
    {
        Task<IUser?> GetAsync(int id);
        Task<IUser?> CheckLoginAndPasswordAsync(string username, string password);
        Task<IEnumerable<IUser>> GetAllAsync();
        Task<int> UpdateStateAsync(int id, bool state);
    }
}
