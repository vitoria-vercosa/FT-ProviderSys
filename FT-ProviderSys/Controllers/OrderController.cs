using FT_ProviderSys.DTOs;
using FT_ProviderSys.Models;
using FT_ProviderSys.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FT_ProviderSys.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {

        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("get-all")]
        public async Task<ActionResult<List<Order>>> GetAll()
        {
            return Ok(await _orderService.GetAll());
        }

        [HttpGet("get-by-id")]
        public async Task<ActionResult<Order>> GetById(int id)
        {
            var result = await _orderService.GetById(id);

            if (result != null) return Ok(result);

            return NotFound();
        }

        [HttpGet("get-by-provider")]
        public async Task<IEnumerable<Order>> GetByProviderId(int providerId)
        {
            return await _orderService.GetByProviderId(providerId);
        }

        [HttpPost("create-order")]
        public async Task<ActionResult> Create([FromBody] OrderCreateRequestDTO inputOrder)
        {
            var result = await _orderService.Create(inputOrder);
            return Created("", result);
        }

        [HttpPut("update-order")]
        public async Task<ActionResult> Update(OrderUpdateRequestDTO inputOrder)
        {
            if (await _orderService.Update(inputOrder)) return NoContent();

            return BadRequest();
        }

        [HttpDelete("delete-order")]
        public async Task<ActionResult> Delete(int idOrder)
        {
            if (await _orderService.Delete(idOrder) ) return NoContent();

            return NotFound();
        }
    }
}
