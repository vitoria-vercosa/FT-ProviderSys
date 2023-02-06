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
        public ActionResult<List<Order>> GetAll()
        {
            return Ok(_orderService.GetAll());
        }

        [HttpGet("get-by-id")]
        public ActionResult<Order> GetById(int id)
        {
            var result = _orderService.GetById(id);

            if (result != null) return Ok(result);

            return NotFound();
        }

        [HttpPost("create-order")]
        public ActionResult Create([FromBody] OrderCreateRequestDTO inputOrder)
        {
            _orderService.Create(inputOrder);
            return Created("", true);
        }

        [HttpPut("update-order")]
        public ActionResult Update(OrderUpdateRequestDTO inputOrder)
        {
            if (_orderService.Update(inputOrder)) return NoContent();

            return BadRequest();
        }

        [HttpDelete("delete-order")]
        public ActionResult Delete(int idOrder)
        {
            if( _orderService.Delete(idOrder) ) return NoContent();

            return NotFound();
        }
    }
}
