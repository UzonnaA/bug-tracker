using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using BugTracker.Data;
using BugTracker.DTO;
using BugTracker.Model;
using Microsoft.EntityFrameworkCore;

namespace BugTracker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly BugContext _context;

        public UserController(BugContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _context.Users.ToListAsync();
            var dtos = users.Select(u => new UserDTO(u)).ToList();
            return Ok(dtos);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] User user)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetAll), new { id = user.Id }, new UserDTO(user));
        }
    }
}
