using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Services.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Attributes
{
    public class CacheAttribute(int duration) : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var cacheService = context.HttpContext.RequestServices.GetRequiredService<IServiceManager>().cacheServices;
            var cacheKey = GenerateCacheKey(context.HttpContext.Request);
            var result = await cacheService.GetCacheValueAsync(cacheKey);
            if (string.IsNullOrEmpty(result))
            {
                context.Result = new ContentResult()
                {
                    ContentType = "application/json",
                    StatusCode = StatusCodes.Status200OK,
                    Content = result

                };
                return;
            }
            var contextResult = await next.Invoke();
            if(contextResult.Result is OkObjectResult okObject)
            {
                cacheService.SetCacheValueAsync(cacheKey, okObject.Value, TimeSpan.FromSeconds(duration));

            }

        }
        private string GenerateCacheKey(HttpRequest request)
        {
            var key = new StringBuilder();
            key.Append(request.Path);
            foreach(var item in request.Query.OrderBy(q=>q.Key))
            {
                key.Append( $"|{item.Key}-{item.Value}");
            }
            return key.ToString();
        }
    }
}
