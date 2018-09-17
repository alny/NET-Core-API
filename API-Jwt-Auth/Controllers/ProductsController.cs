using API_Jwt_Auth.Data.Entity;
using API_Jwt_Auth.Data.Interfaces;
using API_Jwt_Auth.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Jwt_Auth.Controllers {

    [Route("api/[Controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProductsController : Controller {
        private readonly IProductRepository _repository;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IProductRepository repository, ILogger<ProductsController> logger) {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get() {
            try {
                return Ok(_repository.GetAllProducts());

            } catch (Exception ex) {

                _logger.LogError($"Failed to fetch products: {ex}");
                return BadRequest("Failed to fetch products!");
            }
        }



    }
}
