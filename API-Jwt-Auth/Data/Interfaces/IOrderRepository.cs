using API_Jwt_Auth.Data.Entity;
using API_Jwt_Auth.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Jwt_Auth.Data.Interfaces {
    public interface IOrderRepository : IRepository<Order> {

        IEnumerable<Order> GetAllOrders();
        Order GetOrderById(int id);

    }
}
