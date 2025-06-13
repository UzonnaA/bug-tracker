using BugTracker.Model;

namespace BugTracker.DTO
{
    public class BugDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public string Status { get; set; }
        public string ReporterUsername { get; set; }

        public BugDTO(Bug bug)
        {
            Id = bug.Id;
            Title = bug.Title;
            Description = bug.Description;
            Status = bug.Status.ToString();
            ReporterUsername = bug.Reporter?.Username ?? "Unknown";
        }
    }
}
