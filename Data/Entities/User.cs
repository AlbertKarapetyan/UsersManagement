using Infrastructure.DataEntities;

namespace Data.Entities
{
    public class User : IUser
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public bool IsActive { get; set; }

    }
}
