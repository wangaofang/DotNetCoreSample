using Microsoft.AspNetCore.Mvc;
using ForgeSample.Models;
using System.Linq;

namespace ForgeSample.Controllers
{
    [Route("api/product")]
    public class MaterialController:ControllerBase
    {
        [HttpGet("{productId}/materials/{id}")]
        public IActionResult GetMaterail(int productId,int id)
        {
            var product=ProductService.Current.Products.SingleOrDefault(x=>x.Id==productId);
            if(product==null)
            {
                return NotFound();
            }
            var material=product.Materials.SingleOrDefault(x=>x.Id==id);
            if(material==null)
            {
                return NotFound();
            }
            return Ok(material);
        }

        [HttpGet("{ProductId}/materials")]
        public IActionResult GetMaterails(int productId)
        {
            var product=ProductService.Current.Products.SingleOrDefault(x=>x.Id==productId);
            if(product==null)
            {
                return NotFound();
            }
            return Ok(product.Materials);
        }
    }
}