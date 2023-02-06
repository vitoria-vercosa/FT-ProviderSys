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
        public ActionResult<List<Provider>> GetAll()
        {
            return Ok(_providerService.GetAll());
        }

        [HttpGet("get-by-id")]
        public ActionResult<Provider> GetById(int id)
        {
            var result = _providerService.GetById(id);

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
        public ActionResult Update([FromBody]ProviderUpdateRequestDTO inputProvider)
        {
            if( _providerService.Update(inputProvider) ) return NoContent();

            return BadRequest();
        }

        [HttpDelete("delete")]
        public ActionResult Delete([FromRoute]int idProvider)
        {
            if(_providerService.Delete(idProvider)) return NoContent();

            return BadRequest();
        }
    }
}
