using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductsAPI.DTO;
using ProductsAPI.Models;

namespace ProductsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ProductsContext _context;
        public ProductsController(ProductsContext context)
        {
            _context = context;
        }
        // GET: api/Products
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _context
                                        .Products
                                        .Where(p=>p.IsActive)
                                        .Select(p => MapToDTO(p))
                                        .ToListAsync();
                
            return Ok(products);
        }
        
        [Authorize]
        [HttpGet("{id}")]
        public  async Task<IActionResult> GetProduct(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var product = await _context
                .Products
                .Where(p => p.ProductId == id)
                .Select(p => MapToDTO(p) )
                .FirstOrDefaultAsync();
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            
            return CreatedAtAction(nameof(GetProduct), new {id = product.ProductId}, product);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, Product product)
        {
            if (id != product.ProductId)
            {
                return BadRequest();
            }
            var updatedProduct = await _context.Products.FirstOrDefaultAsync(x => x.ProductId == id);

            if (updatedProduct == null)
            {
                return NotFound();
            }
            
            updatedProduct.ProductName = product.ProductName;
            updatedProduct.Price = product.Price;
            updatedProduct.IsActive = product.IsActive;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception err)
            {
                return BadRequest(new {message = err.Message});
            }
            
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var product = await _context.Products.FirstOrDefaultAsync(x => x.ProductId == id);
            
            if (product == null)
            {
                return NotFound();
            }
            
            _context.Products.Remove(product);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch(Exception err)
            {
                return BadRequest(new {message = err.Message});
            }
            
            return NoContent();
        }

        private static ProductDTO MapToDTO(Product product)
        {
            var entity = new ProductDTO();
            if (product != null)
            {
                entity.ProductId = product.ProductId;
                entity.ProductName = product.ProductName;
                entity.Price = product.Price;
            }

            return entity;
        }
    }
}
