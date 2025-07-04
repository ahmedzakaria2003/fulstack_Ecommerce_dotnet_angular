using DomainLayer.Contracts;
using DomainLayer.Models;
using DomainLayer.Models.IdentityModule;
using DomainLayer.Models.OrderModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using Persistence.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Persistence
{
    public class DataSeed(StoreDbContext _dbContext
        , UserManager<ApplicationUser> _userManager,
        RoleManager<IdentityRole> _roleManager , 
        StoreIdentityDbContext _identityDbContext
        ) : IDataSeeding
    {


        public async Task DataSeedMethodAsync()
        {
            var PendingMigrations = await _dbContext.Database.GetPendingMigrationsAsync();
            try
            {
                if (PendingMigrations.Any())
                {
                    await _dbContext.Database.MigrateAsync();
                }

                if (!_dbContext.Set<ProductBrand>().Any())
                {
                    var ProductBrandsData = File.OpenRead(@"..\Infrastructure\Persistence\Data\DataSeed\brands.json");
                    var ProductBrands = await JsonSerializer.DeserializeAsync<List<ProductBrand>>(ProductBrandsData);

                    if (ProductBrands is not null && ProductBrands.Any())
                    {
                        await _dbContext.ProductBrands.AddRangeAsync(ProductBrands);

                    }

                }

                if (!_dbContext.Set<ProductType>().Any())
                {

                    var ProductTypesData = File.OpenRead(@"..\Infrastructure\Persistence\Data\DataSeed\types.json");
                    var ProductTypes = await JsonSerializer.DeserializeAsync<List<ProductType>>(ProductTypesData);
                    if (ProductTypes is not null && ProductTypes.Any())
                    {
                        await _dbContext.ProductTypes.AddRangeAsync(ProductTypes);

                    }
                }

                if (!_dbContext.Set<Product>().Any())
                {
                    var ProductsData = File.OpenRead(@"..\Infrastructure\Persistence\Data\DataSeed\products.json");
                    var Products = await JsonSerializer.DeserializeAsync<List<Product>>(ProductsData);
                    if (Products is not null && Products.Any())
                    {
                        await _dbContext.Products.AddRangeAsync(Products);
                    }
                }

                if (!_dbContext.Set<DeliveryMethod>().Any())
                {
                    var DeliveryMethodDataStream = File.OpenRead(@"..\Infrastructure\Persistence\Data\DataSeed\delivery.json");
                    var DeliveryMethod = await JsonSerializer.DeserializeAsync<List<DeliveryMethod>>(DeliveryMethodDataStream);
                    if (DeliveryMethod is not null && DeliveryMethod.Any())
                    {
                        await _dbContext.Set<DeliveryMethod>().AddRangeAsync(DeliveryMethod);
                    }
                }


                await _dbContext.SaveChangesAsync();

            }

            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred during data seeding: {ex.Message}");
            }

        }

        public async Task IdentityDataSeedMethodAsync()
        {
            try
            {
                if (!_roleManager.Roles.Any())
                {
                    await _roleManager.CreateAsync(new IdentityRole("Admin"));
                    await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
                }

                if (!_userManager.Users.Any())
                {
                    var user01 = new ApplicationUser
                    {
                        UserName = "AhmedMohamed",
                        Email = "ahmedmohamedzakaria423@gmail.com",
                        DisplayName = "Ahmed Mohamed",
                        PhoneNumber = "01011869193",
                    };
                    var user02 = new ApplicationUser
                    {
                        UserName = "Mohamed", 
                        Email = "mohamedzakaria423@gmail.com",
                        DisplayName = "Mohamed",
                        PhoneNumber = "01110409057",
                    };

                    var result1 = await _userManager.CreateAsync(user01, "Ahmed@010");
                    if (!result1.Succeeded)
                    {
                        foreach (var error in result1.Errors)
                            Console.WriteLine($"Error creating user1: {error.Description}");
                    }

                    var result2 = await _userManager.CreateAsync(user02, "Mohamed@010");
                    if (!result2.Succeeded)
                    {
                        foreach (var error in result2.Errors)
                            Console.WriteLine($"Error creating user2: {error.Description}");
                    }

                    if (result1.Succeeded)
                        await _userManager.AddToRoleAsync(user01, "Admin");

                    if (result2.Succeeded)
                        await _userManager.AddToRoleAsync(user02, "SuperAdmin");
                }

                await _identityDbContext.SaveChangesAsync();
            }

            catch (Exception ex)
            {


            }
        }
    }
}