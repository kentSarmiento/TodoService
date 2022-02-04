using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Filters;
using AutoMapper;
using ASPNetTodoAPI.Entities;
using ASPNetTodoAPI.Datastores;
using ASPNetTodoAPI.Repositories;
using ASPNetTodoAPI.Mappings;

namespace ASPNetTodoAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureDevelopmentServices(IServiceCollection services)
        {
            if (Configuration.GetValue<string>("Database", "sql").ToLower() == "sql")
            {
                services.AddDbContext<TodoContext>(opt =>
                                       opt.UseInMemoryDatabase("TodoList"));
                services.AddTransient<ITodoItemsDatastore, TodoItemsInMemoryDatastore>();
                services.AddTransient<ITodoItemsRepository, TodoItemsRepository>();
            }
            else
            {
                services.Configure<TodoItemsDatabaseSettings>(
                    Configuration.GetSection(nameof(TodoItemsDatabaseSettings)));

                services.AddSingleton<ITodoItemsDatabaseSettings>(sp =>
                    sp.GetRequiredService<IOptions<TodoItemsDatabaseSettings>>().Value);

                services.AddSingleton<ITodoItemsDatastore, TodoItemsMongoDatastore>();
                services.AddSingleton<ITodoItemsRepository, TodoItemsRepository>();
            }

            services
                .AddControllers()
                .AddNewtonsoftJson();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "ToDo API",
                    Description = "A simple example Web API using ASP.NET Core 5.0",
                    Contact = new OpenApiContact
                    {
                        Name = "Kent Tristan Yves Sarmiento",
                        Email = "kentsarmiento@gmail.com"
                    }
                });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);

                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "Standard Authorization header using the Bearer scheme (\"Bearer {token}\")",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });

                options.OperationFilter<SecurityRequirementsOperationFilter>();
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8
                            .GetBytes(Configuration.GetValue("AppToken", ""))),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            services.AddAutoMapper(typeof(TodoItemMappingProfile));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ASPNetTodoAPI v1"));

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
