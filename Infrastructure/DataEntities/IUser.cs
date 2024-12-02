namespace Infrastructure.DataEntities
{
    public interface IUser
    {
        int Id { get; set; }
        string Username { get; set; }
        string Password { get; set; }
        bool IsActive { get; set; }
    }
}
