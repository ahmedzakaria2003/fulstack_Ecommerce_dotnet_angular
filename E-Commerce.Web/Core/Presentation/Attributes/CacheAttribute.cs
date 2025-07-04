using ServiceAbstraction;
using System;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Presentation.Attributes
{
    // This attribute can only be applied to controller methods
    [AttributeUsage(AttributeTargets.Method)]
    public class CacheAttribute(int seconds) : Attribute, IAsyncActionFilter
    {
        // Store duration (in seconds) passed to the attribute
        private readonly int _seconds = seconds;

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // Try to get the caching service from the DI container
            var cacheService = context.HttpContext.RequestServices.GetService(typeof(ICacheService)) as ICacheService;

            // If no cache service found, continue normally
            if (cacheService is null)
            {
                await next();
                return;
            }

            // Generate a unique cache key based on the request path and query
            var cacheKey = GenerateCacheKeyFromRequest(context.HttpContext.Request);

            // Try to get cached response by the key
            var cachedResponse = await cacheService.GetAsync(cacheKey);

            if (!string.IsNullOrEmpty(cachedResponse))
            {
                // If value is found in cache, deserialize and return it as JSON
                var deserialized = JsonSerializer.Deserialize<object>(cachedResponse);

                context.Result = new OkObjectResult(deserialized); // returns 200 OK with deserialized data
                return;
            }

            // If no cached value, continue executing the controller action
            var executedContext = await next();

            // If the action result is valid and contains a response body
            if (executedContext.Result is ObjectResult objectResult && objectResult.Value is not null)
            {
                // Serialize the response and store it in the cache
                var responseToCache = JsonSerializer.Serialize(objectResult.Value);
                await cacheService.SetAsync(cacheKey, responseToCache, TimeSpan.FromSeconds(_seconds));
            }
        }

        // Build a unique cache key using path and query string
        private string GenerateCacheKeyFromRequest(HttpRequest request)
        {
            var keyBuilder = new StringBuilder();
            keyBuilder.Append($"{request.Path}");

            foreach (var (key, value) in request.Query.OrderBy(x => x.Key))
            {
                keyBuilder.Append($"|{key}-{value}");
            }

            return keyBuilder.ToString();
        }
    }
}
