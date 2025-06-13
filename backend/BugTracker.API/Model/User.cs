using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BugTracker.Model
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public required string Username { get; set; }

        public ICollection<Bug> BugsReported { get; set; } = new List<Bug>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}
