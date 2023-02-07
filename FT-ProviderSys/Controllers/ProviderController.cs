using FT_ProviderSys.DTOs;
using FT_ProviderSys.Models;
using FT_ProviderSys.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FT_ProviderSys.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProviderController : ControllerBase
    {
        private readonly IProviderService _providerService;

        public ProviderController(IProviderService providerService)
        {
            _providerService = providerService;
        }

        [HttpGet("get-all")]
        public async Task<ActionResult<List<Provider>>> GetAll()
        {
            return Ok(await _providerService.GetAll());
        }

        [HttpGet("get-by-id")]
        public async Task<ActionResult<Provider>> GetById(int id)
        {
            var result = await _providerService.GetById(id);

            if (result != null) return Ok(result);

            return NotFound();
        }

        [HttpPost("create")]
        public async Task<ActionResult> Create([FromBody] ProviderCreateRequestDTO inputProvider)
        {
            var id = await _providerService.Create(inputProvider);
            return Created("", id);
        }
        
        [HttpPut("update")]
        public async Task<ActionResult> Update([FromBody]ProviderUpdateRequestDTO inputProvider)
        {
            if( await _providerService.Update(inputProvider) ) return NoContent();

            return BadRequest();
        }

        [HttpDelete("delete")]
        public async Task<ActionResult> Delete([FromRoute]int idProvider)
        {
            if(await _providerService.Delete(idProvider)) return NoContent();

            return BadRequest();
        }
    }
}
