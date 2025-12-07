using Domain.Contracts;
using Microsoft.AspNetCore.Mvc;
using Persistence;
using Services;
using Shared.ErrorModels;
using Store.G02.Api.Middlewares;


namespace Store.G02.Api.Extensions
{
    public static class Extensions
    {
        public static object Builder { get; private set; }

        public static IServiceCollection RegisterAllServices(this IServiceCollection services,IConfiguration configuration)
        {
            services.ConfirureServices();
            services.AddSwaggerServices();
            services.AddBuiltInServices();
            //Dbcontext,unitOfWork,
            services.AddInfrastructureService(configuration);
            services.AddApplicationServices();
            
            return services;
        }
        private static IServiceCollection AddBuiltInServices(this IServiceCollection services)
        {

            services.AddControllers();
           
            return services;
        }
        private static IServiceCollection AddSwaggerServices(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            return services;
        }
        private static IServiceCollection ConfirureServices(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(config =>
            {
                config.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    var errors = actionContext.ModelState.Where(m => m.Value.Errors.Any())
                                     .Select(m => new ValidationError()
                                     {
                                         Field = m.Key,
                                         Errors = m.Value.Errors.Select(errors => errors.ErrorMessage)

                                     });
                    var response = new ValidationErrorResponse()
                    {
                        Errors = errors
                    };
                    return new BadRequestObjectResult(response);
                };
            });
            return services;
        }
        public static async Task<WebApplication> ConfirureMiddleWares(this WebApplication app)
        {
            //for using GlobalErrorHandlingMiddleware
            await app.InitializeDatabaseAsync();
            app.UseMiddleware<GlobalErrorHandlingMiddleware>();


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseStaticFiles();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            return app;
        }
        private static async Task<WebApplication> InitializeDatabaseAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var dbIntializer = scope.ServiceProvider.GetRequiredService<IDbIntializer>();
            await dbIntializer.IntializeAsync();


            return app;
        }




    }
}
