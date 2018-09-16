using API_Jwt_Auth.Data.Entity;
using API_Jwt_Auth.Data.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Jwt_Auth.Data.Repository {
    public class ProductRepository : Repository<Product>, IProductRepository {
        private readonly Db_Context _ctx;
        private readonly ILogger<ProductRepository> _logger;

        public ProductRepository(Db_Context ctx, ILogger<ProductRepository> logger) : base(ctx) {
            _ctx = ctx;
            _logger = logger;
        }

        public ICollection<Product> GetAllProducts() {

            try {

                _logger.LogInformation("GetAllProducts was called!");

                return _ctx.Products.ToList();

            } catch (Exception ex) {

                _logger.LogError($"Failed to fetch all products{ex}");
                return null;
            }

        }

        public ICollection<Product> GetProductsByCategory(string category) {
            return _ctx.Products.Where(p => p.Category == category).ToList();
        }

    }
}
