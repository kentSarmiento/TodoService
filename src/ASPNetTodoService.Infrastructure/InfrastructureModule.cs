using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Autofac;
using ASPNetTodoService.Domain.Interfaces;
using ASPNetTodoService.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Autofac.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using OpenIddict.Validation.AspNetCore;

namespace ASPNetTodoService.Infrastructure
{
    public class InfrastructureModule : Module
    {
        private readonly string ENCRYPTION_KEY = "Q8R9TBUCVEXFYG2J3K4N6P7Q9SATBVDWEXGZH2J4M5N=";

        private readonly string CLIENT_ID = "tasklist";
        private readonly string CLIENT_SECRET = "846B62D0-DEF9-4215-A99D-86E6B8DAB342";

        public RepositoryType RepositoryType { get; set; }
        // public TodoItemsDatabaseSettings DatabaseSettings { get; set; }

        protected override void Load(ContainerBuilder builder)
        {
            var services = new ServiceCollection();

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme;
            });

            services.AddOpenIddict()
                // Register the OpenIddict validation components.
                .AddValidation(options =>
                {
                    // Note: the validation handler uses OpenID Connect discovery
                    // to retrieve the issuer signing keys used to validate tokens.
                    options.SetIssuer("https://localhost:5001/");

                    // Register the encryption credentials. This sample uses a symmetric
                    // encryption key that is shared between the server and the Api2 sample
                    // (that performs local token validation instead of using introspection).
                    //
                    // Note: in a real world application, this encryption key should be
                    // stored in a safe place (e.g in Azure KeyVault, stored as a secret).
                    //options.AddEncryptionKey(new SymmetricSecurityKey(
                    //    Convert.FromBase64String(ENCRYPTION_KEY)));

                    // Configure the validation handler to use introspection and register the client
                    // credentials used when communicating with the remote introspection endpoint.
                    options.UseIntrospection()
                           .SetClientId(CLIENT_ID)
                           .SetClientSecret(CLIENT_SECRET);

                    // Register the System.Net.Http integration.
                    options.UseSystemNetHttp();

                    // Import the configuration from the local OpenIddict server instance.
                    //options.UseLocalServer();

                    // Register the ASP.NET Core host.
                    options.UseAspNetCore();
                });

            builder.Populate(services);

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
