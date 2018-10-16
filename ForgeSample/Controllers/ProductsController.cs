using System.Collections.Generic;
using System.Linq;
using ForgeSample.Models;
using Microsoft.AspNetCore.Mvc;

namespace ForgeSample.Controllers
{

    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        [HttpGet("all")]
        public IActionResult GetProducts()
        {
            return Ok(ProductService.Current.Products);
        }

        [HttpGet("{id}")]
        public IActionResult GetProduct(int id)
        {
            var product = ProductService.Current.Products.SingleOrDefault(x => x.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
    }
}