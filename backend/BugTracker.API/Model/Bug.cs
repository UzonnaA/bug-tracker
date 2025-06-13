using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BugTracker.Models
{
    public enum BugStatus
    {
        Open,
        InProgress,
        Resolved,
        Closed
    }

    public class Bug
    {
        public int Id { get; set; }

        [Required]
        public required string Title { get; set; }

        public string? Description { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public BugStatus Status { get; set; } = BugStatus.Open;

        // Foreign key to Reporter
        public int ReporterId { get; set; }
        public required User Reporter { get; set; }

        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}
