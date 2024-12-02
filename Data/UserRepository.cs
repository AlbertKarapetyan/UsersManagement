using Data.Entities;
using Infrastructure.DataEntities;
using Infrastructure.Repositories;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Data
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;

        public UserRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<IUser>> GetAllAsync()
        {
            const string query = "SELECT Id, Username, IsActive FROM Users ORDER BY Id";
            var users = new List<IUser>();

            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand(query, connection);

            await connection.OpenAsync();

            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                users.Add(new User
                {
                    Id = reader.GetInt32(0),
                    Username = reader.GetString(1),
                    IsActive = reader.GetBoolean(2)
                });
            }

            return users;
        }

        public async Task<IUser?> GetAsync(int id)
        {
            const string query = "SELECT Id, Username, IsActive FROM Users WHERE Id = @Id";

            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@Id", id);

            await connection.OpenAsync();
            using var reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return new User
                {
                    Id = reader.GetInt32(0),
                    Username = reader.GetString(1),
                    IsActive = reader.GetBoolean(2)
                };
            }

            return null;
        }

        public async Task<IUser?> CheckLoginAndPasswordAsync(string username, string password)
        {
            const string query = "SELECT Id, Username, IsActive FROM Users WHERE Username = @Username AND Password = @Password";

            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@Username", username);
            command.Parameters.AddWithValue("@Password", password);

            await connection.OpenAsync();
            using var reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return new User
                {
                    Id = reader.GetInt32(0),
                    Username = reader.GetString(1),
                    IsActive = reader.GetBoolean(2)
                };
            }

            return null;
        }

        public async Task<int> UpdateStateAsync(int id, bool state)
        {
            const string query = "UPDATE Users SET IsActive = @IsActive WHERE Id = @Id";

            try
            {
                using var connection = new SqlConnection(_connectionString);
                using var command = new SqlCommand(query, connection);

                command.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = id });
                command.Parameters.Add(new SqlParameter("@IsActive", SqlDbType.Bit) { Value = state });

                await connection.OpenAsync();
                return await command.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the user's state.", ex);
            }
        }
    }
}
