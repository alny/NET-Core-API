using API_Jwt_Auth.Data.Entity;
using API_Jwt_Auth.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Jwt_Auth.Data.Repository {
    public class OrderRepository : Repository<Order>, IOrderRepository {
        private readonly Db_Context _ctx;
        private readonly ILogger<OrderRepository> _logger;


        public OrderRepository(Db_Context ctx, ILogger<OrderRepository> logger) : base(ctx) {
            _ctx = ctx;
            _logger = logger;

        }

        public IEnumerable<Order> GetAllOrders() {
            try {

                _logger.LogInformation("Orders has been fetched!");

                return _ctx.Orders
                           .Include(o => o.Items)
                           .ThenInclude(i => i.Product)
                           .ToList();

            } catch (Exception ex) {
                _logger.LogError($"Failed to fetch orders: {ex}");
                return null;
            }
        }
    }
}
