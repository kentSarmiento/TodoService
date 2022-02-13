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
        public RepositoryType RepositoryType { get; set; }
        // public TodoItemsDatabaseSettings DatabaseSettings { get; set; }

        protected override void Load(ContainerBuilder builder)
        {
            switch (RepositoryType)
            {
                case RepositoryType.MongoDb:
                    // services.Configure<TodoItemsDatabaseSettings>(
                    //     config.GetSection(nameof(TodoItemsDatabaseSettings)));

                    // services.AddSingleton<ITodoItemsDatabaseSettings>(sp =>
                    //     sp.GetRequiredService<IOptions<TodoItemsDatabaseSettings>>().Value);
                    break;

                case RepositoryType.Sqlite:
                    builder.Register(x => TodoContextFactory.CreateDbContext())
                        .InstancePerLifetimeScope();
                    break;

                case RepositoryType.SqlInMemory:
                default:
                    builder.Register(x =>
                    {
                       var optionsBuilder = new DbContextOptionsBuilder<TodoContext>();
                       optionsBuilder.UseInMemoryDatabase("TodoList");

                       return new TodoContext(optionsBuilder.Options);
                    }).InstancePerLifetimeScope();
                    break;
            }

            switch (RepositoryType)
            {
                case RepositoryType.MongoDb:
                    builder.RegisterType<TodoItemsMongoRepository>().As<ITodoItemsRepository>()
                        .InstancePerLifetimeScope();
                    break;

                case RepositoryType.Sqlite:
                case RepositoryType.SqlInMemory:
                default:
                    builder.RegisterType<TodoItemsEFRepository>().As<ITodoItemsRepository>()
                        .InstancePerLifetimeScope();
                    break;
            }

        }
    }
}
