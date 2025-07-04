using Azure;
using DomainLayer.Contracts;
using E_Commerce.Web.CustomMiddleWare;
using E_Commerce.Web.Extensions;
using E_Commerce.Web.Factories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Data;
using Persistence.Repositories;
using Presentation.Controllers;
using Service;
using ServiceAbstraction;
using Shared.ErrorModels;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Text.Json;
using System.Threading.Tasks;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        #region Add services to the container

        builder.Services.AddControllers();
        builder.Services.AddSwaggerServices();
        builder.Services.AddInfrastructureServices(builder.Configuration);
        builder.Services.AddApplicationServices();
        builder.Services.AddWebApplicationServices();
        builder.Services.AddScoped<IDataSeeding, DataSeed>();
        builder.Services.AddJWTServices(builder.Configuration);

        // ✅ سياسة CORS المعدلة
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAllOrigins", builder =>
            {
                builder.WithOrigins("http://localhost:4200") // Angular dev server
                       .AllowAnyHeader()
                       .AllowAnyMethod()
                       .AllowCredentials(); // لو بتبعت Authorization header أو Cookies
            });
        });

        #endregion

        var app = builder.Build();

        #region DataSeed
        await app.SeedDataBaseAsync();
        #endregion

        #region Configure the HTTP request pipeline

        app.UseCustomExceptionMiddleWare();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.ConfigObject = new ConfigObject()
                {
                    DisplayRequestDuration = true,
                };

                options.DocumentTitle = "Talabat API";

                options.JsonSerializerOptions = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };

                options.EnableFilter();
                options.DocExpansion(DocExpansion.None);
                options.EnablePersistAuthorization();
            });
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        // ✅ ضع CORS هنا قبل Auth
        app.UseCors("AllowAllOrigins");

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        #endregion

        app.Run();
    }
}
