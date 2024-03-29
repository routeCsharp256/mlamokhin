﻿using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using OzonEdu.MerchandiseServiceInfrastructure.Filters;
using OzonEdu.MerchandiseServiceInfrastructure.StartupFilters;


namespace OzonEdu.MerchandiseServiceInfrastructure
{
    public static class HostBuilderExtensions
    {
        public static IHostBuilder AddInfrastructure(this IHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                 services.AddSingleton<IStartupFilter, TerminalStartupFilter>();
                
                 services.AddSingleton<IStartupFilter, SwaggerStartupFilter>();
                 
                 
                services.AddSwaggerGen(options =>
                {
                    options.SwaggerDoc("v1", new OpenApiInfo {Title = "OzonEdu.MerchandiseServiceApi", Version = "v1"});
                
                    options.CustomSchemaIds(x => x.FullName);
                });
            });
            return builder;
        }
        
        public static IHostBuilder AddHttp(this IHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.AddControllers(options => options.Filters.Add<GlobalExceptionFilter>());
            });
            
            return builder;
        }
    }
}