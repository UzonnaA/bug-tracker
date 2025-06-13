using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BugTracker.Data;
using BugTracker.Model;

namespace BugTracker.Service
{
    public class BugService
    {
        private readonly BugContext _context;

        public BugService(BugContext context)
        {
            _context = context;
        }

        public async Task<List<Bug>> GetAllBugsAsync()
        {
            return await _context.Bugs
                .Include(b => b.Reporter)
                .Include(b => b.Comments)
                .ToListAsync();
        }

        public async Task<Bug?> GetBugByIdAsync(int id)
        {
            return await _context.Bugs
                .Include(b => b.Reporter)
                .Include(b => b.Comments)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<Bug> CreateBugAsync(Bug bug)
        {
            _context.Bugs.Add(bug);
            await _context.SaveChangesAsync();
            return bug;
        }

        public async Task<bool> DeleteBugAsync(int id)
        {
            var bug = await _context.Bugs.FindAsync(id);
            if (bug == null) return false;

            _context.Bugs.Remove(bug);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateBugStatusAsync(int id, BugStatus status)
        {
            var bug = await _context.Bugs.FindAsync(id);
            if (bug == null) return false;

            bug.Status = status;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
