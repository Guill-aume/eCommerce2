using eCommerce.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System;

namespace eCommerce.Data
{
    public class AppDbInitializer
    {
        public static void Seed(ApplicationDbContext appDbContext)
        {
            // using(var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = appDbContext;//serviceScope.ServiceProvider.GetService<AppDbContext>();
                //var roleManager = roleMngr;

                context.Database.EnsureCreated();

                //var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

                //Product
                if (!context.Products.Any())
                {
                    context.Products.AddRange(new List<Product>()
                    {
                        new Product()
                        {
                            Name = "Product 1",
                            ImageURl = "http://dotnethow.net/images/cinemas/cinema-1.jpeg",
                            Description = "This is the description of the first product",
                            Price = 25
                        },
                        new Product()
                        {
                            Name = "Product 2",
                            ImageURl = "http://dotnethow.net/images/cinemas/cinema-2.jpeg",
                            Description = "This is the description of the second cinema",
                            Price = 30
                        }
                    });
                    context.SaveChanges();

                }
            }

        }

    }
}
