using System;
using System.ComponentModel.DataAnnotations;

namespace BugTracker.Models
{
    public class Comment
    {
        public int Id { get; set; }

        [Required]
        public required string Content { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Foreign keys
        public int BugId { get; set; }
        public required Bug Bug { get; set; }

        public int UserId { get; set; }
        public required User User { get; set; }
    }
}
