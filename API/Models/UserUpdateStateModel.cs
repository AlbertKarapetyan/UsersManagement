namespace API.Models
{
    public class UserUpdateStateModel
    {
        public int Id { get; set; }
        public bool OldState { get; set; }
        public bool NewState { get; set; }
    }
}
