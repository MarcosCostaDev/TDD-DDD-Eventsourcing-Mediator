using IntegrationTest.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTest.Infra.Seeds
{
    internal static class SeedMyContext
    {
        internal static void SeedProducts(this ModelBuilder builder)
        {
            builder.Entity<Product>(b =>
            {
                b.HasData(new
                {
                    Id = Guid.Parse("41c0f761-70c9-42a4-a0bc-058c1ecf4d57"),
                    Name = "Nescau",
                    Brand = "Nestle",
                    Price = 4.5
                });
            });

            builder.Entity<Product>(b =>
            {
                b.HasData(new
                {
                    Id = Guid.Parse("24911ce9-9174-4613-8ea0-91e6ab4a9f6f"),
                    Name = "Toddynho",
                    Brand = "Toddy",
                    Price = 2.5
                });
            });

            builder.Entity<Product>(b =>
            {
                b.HasData(new
                {
                    Id = Guid.Parse("0399951c-a322-4298-90cd-712958392496"),
                    Name = "Fanta",
                    Brand = "Coke",
                    Price = 7.3
                });
            });

            builder.Entity<Product>(b =>
            {
                b.HasData(new
                {
                    Id = Guid.Parse("33903b03-f351-4d7e-bec5-121444f38444"),
                    Name = "Coke",
                    Brand = "Coke",
                    Price = 9.25
                });
            });

            builder.Entity<Product>(b =>
            {
                b.HasData(new
                {
                    Id = Guid.Parse("5e70170e-884d-4e73-ae4a-8855e19e349c"),
                    Name = "Double Flex bread",
                    Brand = "Health-option",
                    Price = 5.5
                });
            });

        }
    }
}
