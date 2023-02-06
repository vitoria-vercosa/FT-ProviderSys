using FT_ProviderSys.DTOs;
using FT_ProviderSys.Models;
using FT_ProviderSys.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FT_ProviderSys.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("get-all")]
        public ActionResult<List<Product>> GetAll()
        {
            return Ok(_productService.GetAll());
        }

        [HttpGet("get-by-id")]
        public ActionResult<Product> GetById(int id)
        {
            var result = _productService.GetById(id);

            if (result != null) return Ok(result);

            return NotFound();
        }

        [HttpPost("create")]
        public ActionResult Create([FromBody] ProductCreateRequestDTO inputProduct)
        {
            var id = _productService.Create(inputProduct);
            return Created("", id);
        }

        [HttpPut("update")]
        public ActionResult Update([FromBody] ProductUpdateRequestDTO inputProduct)
        {
            if ( _productService.Update(inputProduct) ) return NoContent();

            return BadRequest();
        }

        [HttpDelete("delete")]
        public ActionResult Delete(int idProduct)
        {
            if( _productService.Delete(idProduct) ) return NoContent();

            return BadRequest();
        }

    }
}
