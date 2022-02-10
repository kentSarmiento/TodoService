using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Autofac;
using ASPNetTodoService.Domain.Interfaces;
using ASPNetTodoService.Infrastructure.Repositories;

namespace ASPNetTodoService.Infrastructure
{
    public class InfrastructureModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(x =>
            {
                var optionsBuilder = new DbContextOptionsBuilder<TodoContext>();
                optionsBuilder.UseInMemoryDatabase("TodoList");
                return new TodoContext(optionsBuilder.Options);
            }).InstancePerLifetimeScope();

            builder.RegisterType<TodoItemsInMemoryRepository>().As<ITodoItemsRepository>()
                .InstancePerLifetimeScope();
        }
    }

    //public static class InfrastructureStartup
    //{
    //    // This method gets called by the runtime. Use this method to add services to the container.
    //    public static IServiceCollection ConfigureServices(
    //        IServiceCollection services
    //        // this IServiceCollection services, IConfiguration config
    //    )
    //    {
    //        //if (config.GetValue<string>("Database", "sql").ToLower() == "sql")
    //        //{
    //            services.AddDbContext<TodoContext>(opt =>
    //                                   opt.UseInMemoryDatabase("TodoList"));
    //            services.AddTransient<ITodoItemsRepository, TodoItemsInMemoryRepository>();
    //        //}
    //        //else
    //        //{
    //        //    services.Configure<TodoItemsDatabaseSettings>(
    //        //        config.GetSection(nameof(TodoItemsDatabaseSettings)));

    //        //    services.AddSingleton<ITodoItemsDatabaseSettings>(sp =>
    //        //        sp.GetRequiredService<IOptions<TodoItemsDatabaseSettings>>().Value);

    //        //    services.AddSingleton<ITodoItemsDatastore, TodoItemsMongoDatastore>();
    //        //    services.AddSingleton<ITodoItemsRepository, TodoItemsRepository>();
    //        //}

    //        return services;
    //    }
    //}
}
