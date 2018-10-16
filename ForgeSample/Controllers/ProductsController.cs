using System.Collections.Generic;
using ForgeSample.Models;
using Microsoft.AspNetCore.Mvc;

namespace ForgeSample.Controllers
{

[Route("api/[controller]")]
public class ProductController: ControllerBase
    {
        [HttpGet("all")]
        public JsonResult GetProducts()
        {
            return new JsonResult(ProductService.Current.Products);
        }

        [HttpGet("{id}")]
        public JsonResult GetProduct(int id)
        {
            return new JsonResult(ProductService.Current.Products.Find(p=>p.Id==id));
        }
    }
}