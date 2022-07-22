using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using offler_backend_api.Configs;
using offler_backend_api.Services;
using offler_db_context.Context;
using offler_script_runner.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace offler_backend_api
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

            services.AddControllers();

            // Interfaces registry for dependency injection 
            services.AddScoped<ITalendScriptExecutorService, TalendScriptExecutorService>();
            services.AddScoped<ITalendConfigService, TalendConfigService>();
            services.AddScoped<IAttachmentFileService, AttachmentFileService>();
            services.AddScoped<IPowershellScriptLauncherService, PowershellScriptLauncherService>();

            // Mapper profile registration
            var mapperConfig = new MapperConfiguration(mc => mc.AddProfile(new MapperProfile()));
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            // Swagger registration and configuration
            services.AddSwaggerGen(c =>
            {

                c.ParameterFilter<ConfigurationFilterService>();

                c.SwaggerDoc("module1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Workflow",
                    Description = "...",
                    Contact = new OpenApiContact
                    {
                        Email = "jaroslaw.maleszyk@spravia.pl",
                    },
                });
                c.SwaggerDoc("module2", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Config",
                    Description = "...",
                    Contact = new OpenApiContact
                    {
                        Email = "jaroslaw.maleszyk@spravia.pl",
                    },
                });
            });

            services.AddDbContext<OfflerDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("OfflerDbConnection")));

            services.AddCors(options =>
            {
                options.AddPolicy("api", builder =>
                {
                    builder.AllowAnyMethod()
                           .AllowAnyHeader()
                           .SetIsOriginAllowed(origin => true) // allow any origin
                           .AllowCredentials(); // allow credentials
                    //AllowAnyHeader()
                    //.WithOrigins(Configuration.GetSection("OtherSettings:AllowedOrigins").Get<string[]>())
                    //.AllowAnyOrigin()
                    //.AllowAnyMethod();
                    //.WithMethods("GET", "POST");
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseHttpsRedirection();

            // Added swagger doc json generator
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/module1/swagger.json", "Main workflow");
                c.SwaggerEndpoint("/swagger/module2/swagger.json", "Konfiguracja");
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
