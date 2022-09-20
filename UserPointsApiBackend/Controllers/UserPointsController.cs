using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserPointsApiBackend.DataAccess;
using UserPointsApiBackend.Models.DataModels;
using UserPointsApiBackend.Services;

namespace UserPointsApiBackend.Controllers
{
    public class UserWithPoint
    {
        public int Id { get; set; }
        public Rank Rank { get; set; }
        public float? Points { get; set; }
        public float? TotalPoints { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class UserPointsController : ControllerBase
    {
        private readonly UserPointsDBContext _context;

        public UserPointsController(UserPointsDBContext context)
        {
            _context = context;
        }

        // GET: api/UserPoints/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserWithPoint>> GetUserPoint(int id)
        {
            var userPoint = await _context.UserPoints.FindAsync(id);

            if (userPoint == null)
            {
                return NotFound();
            }

            var userWithPoints = new UserWithPoint()
            {
                Id = userPoint.Id,
                TotalPoints = userPoint.TotalPoints,
                Points = userPoint.Points,
                Rank = userPoint.Rank
            };

            if (userWithPoints == null)
            {
                return NotFound();
            }

            return userWithPoints;
        }

        // PUT: api/UserPoints/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserPoint(int id, int quantityOfProducts)
        {
            var userPoint = await _context.UserPoints.FindAsync(id);

            if (userPoint == null)
            {
                return NotFound();
            }
            
            float totalSum = 0;

            switch (userPoint.Rank)
            {
                case (Rank)0:
                    totalSum = quantityOfProducts * 0.2f;
                    break;
                case (Rank)1:
                    totalSum = quantityOfProducts * 0.4f;
                    break;
                case (Rank)2:
                    totalSum = quantityOfProducts * 0.6f;
                    break;
            }

            userPoint.TotalPoints = userPoint.TotalPoints + totalSum;
            userPoint.Points = userPoint.Points + totalSum;

            _context.Entry(userPoint).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserPointExists(id))
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

        private bool UserPointExists(int id)
        {
            return _context.UserPoints.Any(e => e.Id == id);
        }
    }
}
