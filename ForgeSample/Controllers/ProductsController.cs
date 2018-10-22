using System;
using System.Collections.Generic;
using System.Linq;
using ForgeSample.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ForgeSample.Controllers
{

    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private ILogger<ProductController> _logger;

        public ProductController(ILogger<ProductController> logger)
        {
            _logger = logger;
        }
        
        
        [HttpGet("all")]
        public IActionResult GetProducts()
        {
            return Ok(ProductService.Current.Products);
        }

        [Route("{id}", Name = "GetProduct")]
        public IActionResult GetProduct(int id)
        {
            try
            {
                throw new Exception("来个异常S!");
                var product = ProductService.Current.Products.SingleOrDefault(x => x.Id == id);
                if (product == null)
                {
                    _logger.LogInformation($"id为{id}的产品没找到!");
                    return NotFound();
                }
                return Ok(product);
            }
            catch(Exception ex)
            {
                _logger.LogCritical($"查找Id为{id}的产品时出现了错误",ex);
                return StatusCode(500,"处理请求的时候发生了错误!");
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] ProductCreation production)
        {
            if (production == null)
            {
                return BadRequest();
            }

            if (production.Name == "产品")
            {
                ModelState.AddModelError("Name", "产品的名称不可以是'名称'二字");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var maxId = ProductService.Current.Products.Max(x => x.Id);
            var newProduct = new Product
            {
                Id = ++maxId,
                Name = production.Name,
                Description =production.Description,
                Price = production.Price
            };
            ProductService.Current.Products.Add(newProduct);
            return CreatedAtRoute("GetProduct", new { id = newProduct.Id }, newProduct);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ProductModification product)
        {
            if (product == null)
            {
                return BadRequest();
            }
            if (product.Name == "产品")
            {
                ModelState.AddModelError("Name", "产品的名称不可以是'产品'二字");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var model = ProductService.Current.Products.SingleOrDefault(x => x.Id == id);
            if (model == null)
            {
                return NotFound();
            }
            model.Name = product.Name;
            model.Price = product.Price;
            model.Description=product.Description;
            return Ok(model);
            // return NoContent();

        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id,[FromBody] JsonPatchDocument<ProductModification> patchDoc)
        {
            if(patchDoc==null)
            {
                return BadRequest();
            }
            var model=ProductService.Current.Products.SingleOrDefault(x=>x.Id==id);
            if(model==null)
            {
                return NotFound();
            }

            var toPatch=new ProductModification
            {
                Name=model.Name,
                Description=model.Description,
                Price=model.Price                
            };
            patchDoc.ApplyTo(toPatch,ModelState);
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (toPatch.Name == "产品")
            {
                ModelState.AddModelError("Name", "产品的名称不可以是'产品'二字");
            }
            TryValidateModel(toPatch);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            model.Name=toPatch.Name;
            model.Description=toPatch.Description;
            model.Price=toPatch.Price;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var model = ProductService.Current.Products.SingleOrDefault(x => x.Id == id);
            if (model == null)
            {
                return NotFound();
            }
            ProductService.Current.Products.Remove(model);
            return NoContent();
        }
    }
}