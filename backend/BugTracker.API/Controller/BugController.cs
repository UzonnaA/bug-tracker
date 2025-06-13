using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using BugTracker.Model;
using BugTracker.Service;
using BugTracker.DTO;

namespace BugTracker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BugController : ControllerBase
    {
        private readonly BugService _bugService;

        public BugController(BugService bugService)
        {
            _bugService = bugService;
        }

        [HttpGet]
        public async Task<ActionResult<List<BugDTO>>> GetAll()
        {
            var bugs = await _bugService.GetAllBugsAsync();
            var bugDTOs = bugs.ConvertAll(b => new BugDTO(b));
            return Ok(bugDTOs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BugDTO>> GetById(int id)
        {
            var bug = await _bugService.GetBugByIdAsync(id);
            if (bug == null) return NotFound();
            return Ok(new BugDTO(bug));
        }

        [HttpPost]
        public async Task<ActionResult<BugDTO>> Create([FromBody] Bug bug)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var created = await _bugService.CreateBugAsync(bug);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, new BugDTO(created));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _bugService.DeleteBugAsync(id);
            return success ? NoContent() : NotFound();
        }

        [HttpPatch("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, [FromQuery] BugStatus status)
        {
            var success = await _bugService.UpdateBugStatusAsync(id, status);
            return success ? NoContent() : NotFound();
        }
    }
}
