using API_Jwt_Auth.Data.Entity;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace API_Jwt_Auth.Data {
    public class SeederData {
        private readonly Db_Context _ctx;
        private readonly IHostingEnvironment _hosting;

        public SeederData(Db_Context ctx, IHostingEnvironment hosting) {
            _ctx = ctx;
            _hosting = hosting;
        }

        public void Seed() {

            _ctx.Database.EnsureCreated();

            if (!_ctx.Products.Any()) {
                // If no products = Create sample data
                var filepath = Path.Combine(_hosting.ContentRootPath, "Data/art.json");
                var json = File.ReadAllText(filepath);
                var products = JsonConvert.DeserializeObject<IEnumerable<Product>>(json);
                _ctx.Products.AddRange(products);

                var order = new Order() {
                    OrderDate = DateTime.Now,
                    OrderNumber = "1234",
                    Items = new List<OrderItem>()
                      {
                        new OrderItem()
                        {
                            Product = products.First(),
                            Quantity = 5,
                            UnitPrice = products.First().Price
                        }
                    }
                };

                _ctx.Orders.Add(order);

                _ctx.SaveChanges();
            }
        }
    }
}
