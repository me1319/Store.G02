
using AutoMapper;
using Domain.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Services;
using Services.Abstraction;
using Shared.ErrorModels;
using Store.G02.Api.Extensions;
using Store.G02.Api.Middlewares;
using System.Threading.Tasks;
using AssemblyMapping = Services.AssemblyReference;

namespace Store.G02.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.RegisterAllServices(builder.Configuration);
            var app = builder.Build();
            await app.ConfirureMiddleWares();
            app.Run();
        }
    }
}
