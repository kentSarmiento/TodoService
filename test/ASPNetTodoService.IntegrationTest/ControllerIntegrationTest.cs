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

namespace ASPNetTodoService.IntegrationTest
{
    public class ControllerIntegrationTest
    {
        protected readonly string TODO_ITEM_ID = "1234";
        protected readonly string TODO_ITEM_NAME = "NUnit + Moq Testing";
        protected readonly string TODO_ITEM_SECRET = "4321";

        protected WebApplicationFactory<Startup> GenerateFullServer()
        {
            return new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        var descriptor = services.SingleOrDefault(
                            d => d.ServiceType == typeof(DbContextOptions<TodoContext>));

                        services.Remove(descriptor);
                        services.AddDbContext<TodoContext>(
                            options => { options.UseInMemoryDatabase("TodoList"); });

                        var sp = services.BuildServiceProvider();

                        using (var scope = sp.CreateScope())
                        {
                            var scopedServices = scope.ServiceProvider;
                            var db = scopedServices.GetRequiredService<TodoContext>();
                            var logger = scopedServices
                                .GetRequiredService<ILogger<ControllerIntegrationTest>>();

                            db.Database.EnsureCreated();

                            try
                            {
                                InitializeDatabase(db);
                            }
                            catch (Exception ex)
                            {
                                logger.LogError(ex, "An error occurred initializing the database. " +
                                    "Error: {Message}", ex.Message);
                            }
                        }
                    });
                });
        }

        protected WebApplicationFactory<Startup> GenerateEmptyServer()
        {
            return new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        var descriptor = services.SingleOrDefault(
                            d => d.ServiceType == typeof(DbContextOptions<TodoContext>));

                        services.Remove(descriptor);
                        services.AddDbContext<TodoContext>(
                            options => { options.UseInMemoryDatabase("TodoList"); });
                    });
                });
        }

        private void InitializeDatabase(TodoContext db)
        {
            db.TodoItems.Add(
                new TodoItem() { Id = TODO_ITEM_ID, Name = TODO_ITEM_NAME, IsComplete = true, Secret = TODO_ITEM_SECRET });
            db.SaveChanges();
        }
    }
}