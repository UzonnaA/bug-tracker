using BugTracker.Model;

namespace BugTracker.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Username { get; set; }

        public UserDTO(User user)
        {
            Id = user.Id;
            Username = user.Username;
        }
    }
}
