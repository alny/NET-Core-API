using API_Jwt_Auth.Data.Entity;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<User> _userManager;

        public SeederData(Db_Context ctx, IHostingEnvironment hosting, UserManager<User> userManager) {
            _ctx = ctx;
            _hosting = hosting;
            _userManager = userManager;
        }

        public async Task SeedAsync() {

            _ctx.Database.EnsureCreated();

            User user = await _userManager.FindByEmailAsync("admin@admin.com");
            if (user == null) {
                user = new User() {
                    FirstName = "admin",
                    LastName = "admin",
                    Email = "admin@admin.com",
                    UserName = "admin"
                };
                var result = await _userManager.CreateAsync(user, "P@ssw0rd!");
                if (result != IdentityResult.Success) {
                    throw new InvalidOperationException("Could not create new user in seeder");
                }
            }

            if (!_ctx.Products.Any()) {
                // If no products = Create sample data
                var filepath = Path.Combine(_hosting.ContentRootPath, "Data/art.json");
                var json = File.ReadAllText(filepath);
                var products = JsonConvert.DeserializeObject<IEnumerable<Product>>(json);
                _ctx.Products.AddRange(products);

                var order = new Order() {
                    OrderDate = DateTime.Now,
                    OrderNumber = "1234",
                    User = user,
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
