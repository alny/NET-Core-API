using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id) {
            try {
                _logger.LogInformation("Order has successfully fetched!");
                return Ok(_repository.GetById(id));

            } catch (Exception ex) {
                _logger.LogError($"Failed to fetch orders: {ex}");

                return BadRequest("Failed to fetch orders");
            }
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]string value) {
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value) {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id) {
        }
    }
}
