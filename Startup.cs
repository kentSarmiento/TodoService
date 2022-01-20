using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
using ASPNetCore5TodoAPI.Models;
using ASPNetCore5TodoAPI.Datastores;
using ASPNetCore5TodoAPI.Repositories;

namespace ASPNetCore5TodoAPI
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
            services.Configure<TodoItemsDatabaseSettings>(
                Configuration.GetSection(nameof(TodoItemsDatabaseSettings)));

            services.AddSingleton<ITodoItemsDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<TodoItemsDatabaseSettings>>().Value);

            services.AddSingleton<ITodoItemsDatastore, TodoItemsDatastore>();
            services.AddSingleton<ITodoItemsRepository, TodoItemsRepository>();

            services.AddControllers();
            services.AddDbContext<TodoContext>(opt =>
                                   opt.UseInMemoryDatabase("TodoList"));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ASPNetCore5TodoAPI", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ASPNetCore5TodoAPI v1"));
            }

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
