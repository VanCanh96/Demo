// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System;
using System.Linq;
using System.Reflection;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace IdentityServer
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public IWebHostEnvironment Environment { get; }

        public Startup(IWebHostEnvironment environment, IConfiguration configuration)
        {
            Environment = environment;
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            const string connectionString = "User ID=postgres;Password=123456;Server=localhost;Port=5432;Database=identity;Integrated Security=true;Pooling=true;";
            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            // uncomment, if you want to add an MVC-based UI
            //services.AddControllersWithViews();
            //services.AddDbContext<PersistedGrantDbContext>(options =>
            //    options.UseNpgsql(connectionString));

            //services.AddDbContext<ConfigurationDbContext>(options =>
            //    options.UseNpgsql(connectionString));

            var builder = // configure identity server with in-memory stores, keys, clients and scopes
                services.AddIdentityServer()
                    .AddTestUsers(Config.GetUsers())
                    // this adds the config data from DB (clients, resources)
                    .AddConfigurationStore(options =>
                    {
                        options.ConfigureDbContext = b =>
                            b.UseNpgsql(connectionString,
                                sql => sql.MigrationsAssembly(migrationsAssembly));
                    })
                    // this adds the operational data from DB (codes, tokens, consents)
                    .AddOperationalStore(options =>
                    {
                        options.ConfigureDbContext = b =>
                            b.UseNpgsql(connectionString,
                                sql => sql.MigrationsAssembly(migrationsAssembly));
                
                        // this enables automatic token cleanup. this is optional.
                        options.EnableTokenCleanup = true;
                    });

            if (Environment.IsDevelopment())
            {
                builder.AddDeveloperSigningCredential();
            }
            else
            {
                throw new Exception("need to configure key material");
            }

            services.AddMvc();

        }

        public void Configure(IApplicationBuilder app)
        {
            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            InitializeDatabase(app);

            // uncomment if you want to support static files
            app.UseStaticFiles();

            app.UseIdentityServer();

            app.UseRouting();
            // uncomment, if you want to add an MVC-based UI
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }

        private void InitializeDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();

                var context = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
                context.Database.Migrate();
                if (!context.Clients.Any())
                {
                    foreach (var client in Config.GetClients())
                    {
                        context.Clients.Add(client.ToEntity());
                    }
                    context.SaveChanges();
                }

                if (!context.IdentityResources.Any())
                {
                    foreach (var resource in Config.GetIdentityResources())
                    {
                        context.IdentityResources.Add(resource.ToEntity());
                    }
                    context.SaveChanges();
                }

                if (!context.ApiResources.Any())
                {
                    foreach (var resource in Config.GetApis())
                    {
                        context.ApiResources.Add(resource.ToEntity());
                    }
                    context.SaveChanges();
                }
            }
        }
    }
}
