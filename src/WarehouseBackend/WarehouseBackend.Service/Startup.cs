using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Serilog;
using WarehouseBackend.Core.AutoMapper;
using WarehouseBackend.Core.Exceptions;
using WarehouseBackend.Core.Services;
using WarehouseBackend.DataAccess;

namespace WarehouseBackend.Service
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<WarehouseDbContext>(
                // Note: I have tried the Steeltoe library to connect, but it kept giving me errors.
                // "Cannot assign requested address"
                // Best practice is to add the connection string into the appsetting.
                options => options.UseNpgsql(
                    "Host=host.docker.internal;Port=5436;Database=warehouse;Username=postgres;Password=Steeltoe789;SSL Mode=Prefer;Trust Server Certificate=true"));
            services.AddTransient<IWarehouseDataAccess, WarehouseDataAccess>();
            services.AddTransient<IWarehouseService, WarehouseService>();
            services.AddMapper();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WarehouseBackend", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WarehouseBackend v1"));

            app.UseCustomExceptionHandler();
            app.UseSerilogRequestLogging();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
