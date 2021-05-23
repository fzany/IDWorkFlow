using IDWorkFlow.Data;
using IDWorkFlow.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IDWorkFlow.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductHistoriesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public ProductHistoriesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: api/ProductHistories
        //[Authorize(Roles = "Worker")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductHistory>>> GetProductHistory(bool limit = false)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);
            List<ProductHistory> data;
            if (limit)
                data = await _context.ProductHistory.Where(d => d.UserId == user.Id).ToListAsync();
            else
                data = await _context.ProductHistory.ToListAsync();

            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }
    }
}
