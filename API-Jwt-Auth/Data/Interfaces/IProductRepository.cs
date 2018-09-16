using API_Jwt_Auth.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Jwt_Auth.Data.Interfaces {
    public interface IProductRepository : IRepository<Product> {

        ICollection<Product> GetAllProducts();
        ICollection<Product> GetProductsByCategory(string category);
    }
}
