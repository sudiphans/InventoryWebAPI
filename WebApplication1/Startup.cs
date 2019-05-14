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
using Microsoft.OData;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNet.OData.Builder;
using Microsoft.OData.Edm;
using EmployeeService.CommService;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.OData;
using Swashbuckle.AspNetCore.SwaggerGen;

using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNet.OData.Formatter;
using Microsoft.Net.Http.Headers;
using System.IO;

namespace WebApplication1
{
    public class Startup
    {
      
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            Environment = env;

           Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(configuration).CreateLogger();
            Log.Information("++++++++Data Logging Started+++++++++++");
        
        }

        public IConfiguration Configuration { get; }
        public IHostingEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            
            
            services.AddDbContext<InventoryContext>(options =>
            {
                options.UseOracle(Configuration["ConnectionString"]);
                //options.EnableSensitiveDataLogging();
            });




            services.AddMvc(options =>
            {
                
                //adding filters in MVC
                options.Filters.Add(typeof(ModelStateFilter));
            }).AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>())//adding fluent validation DI
               .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                });

           

          
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Inventory Manager API",
                    Description = "Inventory Manager using ASP.NET Core Web API",
                    TermsOfService = "None",
                    Contact = new Contact
                    {
                        Name = "Sudip Kumar Prasad",
                        Email = "sudiphansraj1@gmail.com",
                        Url = string.Empty,
                    },
                    License = new License
                    {
                        Name = "Use GPL",
                        Url = "https://example.com/license"
                    }
                });

                var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });


            services.AddOData();
           services.AddTransient<CdetailModelBuilder>();
            services.AddTransient<MailService>();

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

            // Workaround: https://github.com/OData/WebApi/issues/1177
            services.AddMvcCore(options =>
            {
                foreach (var outputFormatter in options.OutputFormatters.OfType<ODataOutputFormatter>().Where(_ => _.SupportedMediaTypes.Count == 0))
                {
                    outputFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/prs.odatatestxx-odata"));
                }
                foreach (var inputFormatter in options.InputFormatters.OfType<ODataInputFormatter>().Where(_ => _.SupportedMediaTypes.Count == 0))
                {
                    inputFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/prs.odatatestxx-odata"));
                }
            });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory,CdetailModelBuilder cdetailModelBuilder )
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });



            loggerFactory.AddSerilog();
           

           app.OnConfiguringException();

            app.UseAuthentication();

            app.UseMvc(routeBuilder =>
            {
                routeBuilder.EnableDependencyInjection();
                routeBuilder.Expand().Select().OrderBy().Filter().MaxTop(null).Count();
                routeBuilder.MapODataServiceRoute("odata", "odata", cdetailModelBuilder.GetEdmModel(app.ApplicationServices));


            });

           


        }
    }
}
