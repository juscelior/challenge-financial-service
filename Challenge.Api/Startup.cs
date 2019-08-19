using Challenge.Api.Consumer.Accounts;
using Challenge.Api.Handler.Accounts;
using Challenge.Api.HostedService;
using Challenge.Api.Middlewares;
using Challenge.Core.Events.Accounts.Request;
using Challenge.CrossCutting.IoC;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NSwag;
using NSwag.AspNetCore;
using System.Reflection;

namespace Challenge.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddApiVersioning(x => x.ApiVersionReader = new HeaderApiVersionReader("api-version"));

            APIContainer.RegisterServices(services);


            services.AddMassTransit(c =>
            {
                c.AddConsumer<SearchByAccountConsumer>(config =>
                {
                    config.UseConcurrentMessageLimit(1);
                });
                c.AddConsumer<TransactionConsumer>(config => config.UseConcurrentMessageLimit(1));

                c.AddBus(provider => Bus.Factory.CreateUsingInMemory(cfg =>
                {
                    cfg.ConfigureEndpoints(provider);
                }));

                c.AddRequestClient<SearchByAccountRequest>();
                c.AddRequestClient<TransactionRequest>();
            });

            services.AddSingleton<IBus>(provider => provider.GetRequiredService<IBusControl>());
            services.AddSingleton<IHostedService, BusService>();
        }

        public void Configure(IApplicationBuilder app, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseExceptionHandler(new ExceptionHandlerOptions
            {
                ExceptionHandler = new ErrorHandlerMiddleware(env).Invoke
            });

            app.UseMiddleware<AuthenticationMiddleware>();


            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
