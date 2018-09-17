using API_Jwt_Auth.Data.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Jwt_Auth.Data {
    public class Db_Context : IdentityDbContext {

        public Db_Context(DbContextOptions<Db_Context> options) : base(options) {

        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }

    }
}
