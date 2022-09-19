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

// System.InvalidOperationException: Unable to resolve service for type 'UserPointsApiBackend.Services.UserPointServices' while attempting to activate 'UserPointsApiBackend.Controllers.UserPointsController'.


namespace UserPointsApiBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserPointsController : ControllerBase
    {
        private readonly UserPointsDBContext _context;
        private readonly IUserPointServices _userPointServices;

        public UserPointsController(UserPointsDBContext context, IUserPointServices userPointServices)
        {
            _context = context;
            _userPointServices = userPointServices;
        }

        // TODO: Implementar metodos de Recopilar puntos y Agregar puntos al usuario
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
            userPoint.Points = userPoint.TotalPoints + totalSum;

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
