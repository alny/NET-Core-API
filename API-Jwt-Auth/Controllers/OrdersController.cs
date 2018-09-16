using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_Jwt_Auth.Data.Entity;
using API_Jwt_Auth.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_Jwt_Auth.Controllers {

    [Route("api/[Controller]")]
    public class OrdersController : Controller {

        private readonly IOrderRepository _repository;
        private readonly ILogger<OrdersController> _logger;

        public OrdersController(IOrderRepository repository, ILogger<OrdersController> logger) {

            _repository = repository;
            _logger = logger;

        }


        // GET: api/<controller>
        [HttpGet]
        public IActionResult Get() {
            try {
                _logger.LogInformation("Orders has successfully fetched!");
                return Ok(_repository.GetAllOrders());

            } catch (Exception ex) {
                _logger.LogError($"Failed to fetch orders: {ex}");

                return BadRequest("Failed to fetch orders");
            }
        }

        // GET api/<controller>/1
        [HttpGet("{id}")]
        public IActionResult Get(int id) {
            try {

                var order = _repository.GetOrderById(id);
                if (order != null) {
                    return Ok(order);
                } else { return NotFound(); }

            } catch (Exception ex) {
                _logger.LogError($"Failed to fetch orders: {ex}");

                return BadRequest("Failed to fetch orders");
            }
        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Post([FromBody]Order model) {
            try {

                _repository.Create(model);

                return Created($"/api/orders/{model.Id}", model);
            } catch (Exception ex) {

                _logger.LogError($"Failed to create order: {ex}");
                return BadRequest("Failed to create order");
            }

        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value) {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id) {
            try {
                _logger.LogInformation("Order has been removed");
                var order = _repository.GetById(id);
                if(order != null) {
                    _repository.Delete(order);
                    return Ok();
                } else {
                    return NotFound();
                }
            } catch (Exception ex) {
                _logger.LogError($"Order didnt get removed: {ex}");
                return BadRequest();
            }
        }
    }
}
