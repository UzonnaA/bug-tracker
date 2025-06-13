using Microsoft.EntityFrameworkCore;
using BugTracker.Model;

namespace BugTracker.Data
{
    public class BugContext : DbContext
    {
        public BugContext(DbContextOptions<BugContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Bug> Bugs { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Store enum as string (optional but readable in DB)
            modelBuilder.Entity<Bug>()
                .Property(b => b.Status)
                .HasConversion<string>();

            modelBuilder.Entity<User>()
                .HasMany(u => u.BugsReported)
                .WithOne(b => b.Reporter)
                .HasForeignKey(b => b.ReporterId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Comments)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Bug>()
                .HasMany(b => b.Comments)
                .WithOne(c => c.Bug)
                .HasForeignKey(c => c.BugId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
