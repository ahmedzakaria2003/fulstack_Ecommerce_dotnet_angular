using DomainLayer.Contracts;
using E_Commerce.Web.CustomMiddleWare;

namespace E_Commerce.Web.Extensions
{
    public static class WebApplicationRegistration
    {
        public static async Task  SeedDataBaseAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var DataSeedingObject = scope.ServiceProvider.GetRequiredService<IDataSeeding>();
            await DataSeedingObject.DataSeedMethodAsync();
            await DataSeedingObject.IdentityDataSeedMethodAsync();

       
        }

        public static IApplicationBuilder UseCustomExceptionMiddleWare(this IApplicationBuilder app)
        {
         
              app.UseMiddleware<customExceptionHandlerMiddleWare>();
                return app;
        }
        public static IApplicationBuilder UsesSwaggerMiddleWares(this IApplicationBuilder app)
        {
                app.UseSwagger();
                app.UseSwaggerUI();
           
            return app;
        }

    }
}
