using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserPointsApiBackend.DataAccess;
using UserPointsApiBackend.Models.DataModels;
using UserPointsApiBackend.Services;

namespace UserPointsApiBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserPointsDBContext _context;
        private readonly IUserPointServices _userPointServices;

        public UsersController(UserPointsDBContext context, IUserPointServices userPointServices)
        {
            _context = context;
            _userPointServices = userPointServices;
        }

        // GET: api/Users https://localhost:7190/api/users
        [HttpGet] // Funcion asincrona de tipo GET
        public async Task<ActionResult<IEnumerable<UserPoint>>> GetUsers()
        {
            if (_context.UserPoints == null)
            {
                return NotFound();
            }
            return await _context.UserPoints.ToListAsync();
        }

        // GET: api/Users/5 https://localhost:7190/api/users/1
        [HttpGet("{id}")]
        public async Task<ActionResult<UserPoint>> GetUser(int id)
        {
            if (_context.UserPoints == null)
            {
                return NotFound();
            }

            var user = await _context.UserPoints.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // PUT: api/Users/5 https://localhost:7190/api/users/1
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, UserPoint user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Users https://localhost:7190/api/users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserPoint>> PostUser(UserPoint user)
        {
            if (_context.UserPoints == null)
            {
                return Problem("Entity set 'UserPointsDBContext.Users' is null.");
            }
            _context.UserPoints.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        // DELETE: api/Users/5 https://localhost:7190/api/users/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (_context.UserPoints == null)
            {
                return NotFound();
            }
            var user = await _context.UserPoints.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.UserPoints.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return (_context.UserPoints?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
