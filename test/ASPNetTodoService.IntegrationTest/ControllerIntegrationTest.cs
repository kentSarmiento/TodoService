using NUnit.Framework;
using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ASPNetTodoService.API;
using ASPNetTodoService.Infrastructure.Repositories;
using ASPNetTodoService.Domain.Entities;
using Microsoft.AspNetCore.TestHost;
using Autofac;

namespace ASPNetTodoService.IntegrationTest
{
    public class ControllerIntegrationTest
    {
        protected readonly string TODO_ITEM_ID = "1234";
        protected readonly string TODO_ITEM_NAME = "NUnit + Moq Testing";
        protected readonly string TODO_ITEM_SECRET = "4321";

        protected WebApplicationFactory<Startup> GenerateServer()
        {
            return new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(builder =>
                {
                    //builder.ConfigureServices(services =>
                    //{
                    //    var descriptor = services.SingleOrDefault(
                    //        d => d.ServiceType == typeof(DbContextOptions<TodoContext>));

                    //    services.Remove(descriptor);
                    //    services.AddDbContext<TodoContext>(
                    //        options => { options.UseInMemoryDatabase("TodoList"); });
                    //});
                    builder.ConfigureTestContainer<ContainerBuilder>(containerBuilder =>
                    {
                        containerBuilder.Register(x =>
                        {
                            var optionsBuilder = new DbContextOptionsBuilder<TodoContext>();
                            optionsBuilder.UseInMemoryDatabase("TodoList");

                            return new TodoContext(optionsBuilder.Options);
                        }).InstancePerLifetimeScope();
                    });
                });
        }
    }
}