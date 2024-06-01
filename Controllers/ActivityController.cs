using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Maxula_project.Models;

namespace Maxula_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivityController : ControllerBase
    {
        private readonly DataContext _context;
        public ActivityController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Activity>>> GetAllActivities()
        {
            try
            {
                var activities = await _context.Activities.ToListAsync();
                return Ok(activities);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{actId}")]
        public async Task<ActionResult<Activity>> GetActivityById(int actId)
        {
            try
            {
                var activity = await _context.Activities.FindAsync(actId);
                if (activity == null)
                    return NotFound("Activity not found.");

                return Ok(activity);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Activity>> CreateActivity(Activity activity)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                _context.Activities.Add(activity);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetActivityById), new { actId = activity.ActivityId }, activity);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("checkout")]
        public async Task<ActionResult<Activity>> CheckoutActivity([FromQuery] string sector, [FromQuery] int deskId)
        {
            try
            {
                var activity = await _context.Activities
                    .Where(a => a.Sector == sector && a.DeskId == deskId && a.CheckOut == null)
                    .FirstOrDefaultAsync();

                if (activity == null)
                {
                    return NotFound("Activity not found or already checked out.");
                }

                activity.CheckOut = TimeOnly.FromDateTime(DateTime.Now);
                await _context.SaveChangesAsync();

                return Ok(activity);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("checkin")]
        public async Task<ActionResult<Activity>> CheckInActivity([FromQuery] int userId, [FromQuery] string sector, [FromQuery] int deskId)
        {
            try
            {
                var activity = new Activity
                {
                    UserId = userId,
                    Sector = sector,
                    DeskId = deskId,
                    Date = DateOnly.FromDateTime(DateTime.Now),
                    CheckIn = TimeOnly.FromDateTime(DateTime.Now)
                };

                _context.Activities.Add(activity);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetActivityById), new { actId = activity.ActivityId }, activity);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }
    }
}
