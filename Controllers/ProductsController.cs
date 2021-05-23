using IDWorkFlow.Data;
using IDWorkFlow.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IDWorkFlow.Controllers
{
    // [Authorize(Roles = "Worker")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;


        public ProductsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: api/Products
        /// <summary>
        /// Get all the products from this role Worker. 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProduct(bool limit = false)
        {
            try
            {
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var user = await _userManager.FindByIdAsync(userId);

                List<Product> products;
                if (limit)
                    products = await _context.Product.Where(d => d.UserId == user.Id).ToListAsync();
                else
                    products = await _context.Product.ToListAsync();
                return products;
            }
            catch (Exception ex)
            {

                return NotFound();
            }
        }


        // GET: api/Products/5
        /// <summary>
        /// Get a single Product from the Worker.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(string id)
        {
            var product = await _context.Product.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // PUT: api/Products/5
        /// <summary>
        /// Edit a Product
        /// </summary>
        /// <param name="id"></param>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(string id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);
            if (product.UserId != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;

            _context.ProductHistory.Add(new ProductHistory() { Action = "Modify", Date = DateTime.UtcNow,  ProductName = product.Name, ProductId = product.Id, ProductSummary = product.Summary,  UserId = userId, Id = Guid.NewGuid().ToString() });
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
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

        // POST: api/Products
        /// <summary>
        /// Create a product.
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> PostProduct(Product product)
        {

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            product.UserId = userId;
            product.Id = Guid.NewGuid().ToString();
            _context.Product.Add(product);
            _context.ProductHistory.Add(new ProductHistory() { Action = "Add", Date = DateTime.UtcNow, ProductName = product.Name, ProductId = product.Id, ProductSummary = product.Summary, UserId = userId, Id = Guid.NewGuid().ToString() });

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                if (ProductExists(product.Id))
                {
                    return Conflict();
                }
                else
                {
                    return BadRequest(ex);
                }
            }

            return CreatedAtAction("GetProduct", new { id = product.Id });
        }

        // DELETE: api/Products/5
        /// <summary>
        /// Delete a product. 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);
            if (product.UserId != user.Id)
            {
                return BadRequest();
            }
            _context.ProductHistory.Add(new ProductHistory() { Action = "Delete", Date = DateTime.UtcNow, ProductName = product.Name, ProductId = product.Id, ProductSummary = product.Summary, UserId = userId, Id = Guid.NewGuid().ToString() });
            _context.Product.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(string id)
        {
            return _context.Product.Any(e => e.Id == id);
        }
    }
}
