using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductApp.Models;
using ProductApp.Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProductApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductsController> _logger; 

        public ProductsController(IProductService productService, ILogger<ProductsController> logger)
        {
            _productService = productService;
            _logger = logger;
        }


        [HttpGet]
        [Authorize]
        public IActionResult GetAll()
        {
            var products = _productService.GetAllProducts();
            return Ok(products);
        }

        [HttpGet("{id}")]
        [Authorize]
        public IActionResult GetById(int id)
        {
            var product = _productService.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        
        [HttpPost]
        [Authorize]
        public IActionResult Create([FromBody] Product product)
        {
            _productService.AddProduct(product);
            return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
        }

        [HttpPut("{id}")]
        [Authorize]
        public IActionResult Update(int id, [FromBody] Product product)
        {
            if(id != product.Id)
            {
                return BadRequest();
            }

            var existingProduct = _productService.GetProductById(id);

            if (existingProduct == null)
            {
                return NotFound();
            }

            _productService.UpdateProduct(product);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Delete(int id)
        {
            var product = _productService.GetProductById(id);

            if(product == null)
            {
                return NotFound();
            }

            _productService.DeleteProduct(id);
            return NoContent();
        }
    }
}
