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
        public async Task<ActionResult<List<Product>>> GetAll()
        {
            return Ok(await _productService.GetAll());
        }

        [HttpGet("get-by-id")]
        public async Task<ActionResult<Product>> GetById(int id)
        {
            var result = await _productService.GetById(id);

            if (result != null) return Ok(result);

            return NotFound();
        }


        [HttpPost("create")]
        public async Task<ActionResult> Create([FromBody] ProductCreateRequestDTO inputProduct)
        {
            var id = await _productService.Create(inputProduct);
            return Created("", id);
        }

        [HttpPut("update")]
        public async Task<ActionResult> Update([FromBody] ProductUpdateRequestDTO inputProduct)
        {
            if (await _productService.Update(inputProduct) ) return NoContent();

            return BadRequest();
        }

        [HttpDelete("delete")]
        public async Task<ActionResult> Delete(int idProduct)
        {
            if (await _productService.Delete(idProduct) ) return NoContent();

            return BadRequest();
        }

    }
}
