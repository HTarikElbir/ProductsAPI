using Microsoft.AspNetCore.Mvc;
using ProductsAPI.Models;

namespace ProductsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private static List<Product>? _products;

        public ProductsController()
        {
            _products =
            [
                new Product { ProductId = 1, ProductName = "Product 1", Price = 100, IsActive = true },
                new Product { ProductId = 2, ProductName = "Product 2", Price = 200, IsActive = true },
                new Product { ProductId = 3, ProductName = "Product 3", Price = 300, IsActive = true },
                new Product { ProductId = 4, ProductName = "Product 4", Price = 400, IsActive = true },
                new Product { ProductId = 5, ProductName = "Product 5", Price = 500, IsActive = true }
            ];
        }
        // GET: api/Products
        [HttpGet]
        public IActionResult GetProducts()
        {
            if (_products == null)
            {
                return NotFound();
            }
            
            return Ok(_products);
        }
        
        [HttpGet("{id}")]
        public IActionResult GetProduct(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var product = _products?.FirstOrDefault(p => p.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
    }
}
