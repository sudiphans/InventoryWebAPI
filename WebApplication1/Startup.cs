using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Oracle.EntityFrameworkCore;
using EmployeeService.infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using FluentValidation.AspNetCore;
using EmployeeService.Validators;
using FluentValidation;
using EmployeeService.Models;
using EmployeeService.ActionFilter;
using EmployeeService.ExceptionHandler;
using Serilog;



namespace WebApplication1
{
    public class Startup
    {
      
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            Environment = env;

           Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(configuration).CreateLogger();
        
        }

        public IConfiguration Configuration { get; }
        public IHostingEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            
         
            services.AddDbContext<InventoryContext>(options =>
            options.UseOracle(Configuration["ConnectionString"]));

            services.AddMvc(options =>
            { 
                //adding filters in MVC
                 options.Filters.Add(typeof(ModelStateFilter));
            }).AddFluentValidation(fv=> fv.RegisterValidatorsFromAssemblyContaining<Startup>()); //adding fluent validation DI

            //JWT authentication add on for keycloak 
            //refer keycloak auth for more info

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.Authority = Configuration["Jwt:Authority"];
                o.Audience = Configuration["Jwt:ClientId"];
               
              
                o.RequireHttpsMetadata = true; //only for development /remove before deploying in production
                o.Events = new JwtBearerEvents()
                {

                    OnAuthenticationFailed = c =>
                    {
                        c.NoResult();
                        c.Response.StatusCode = 500;
                        c.Response.ContentType = "text/plain";
                        if (Environment.IsDevelopment())
                        {
                            return c.Response.WriteAsync(c.Exception.ToString());
                        }
                        return c.Response.WriteAsync(c.Exception.ToString()); //change it in production server

                    }
                };
            });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

           loggerFactory.AddSerilog();
            Log.Information("Data Logging started .");
           app.OnConfiguringException();
            app.UseAuthentication();
            app.UseMvc();
           
        }
    }
}
