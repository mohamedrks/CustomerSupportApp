using ChatAPI.Interfaces;
using ChatAPI.Services;
using ChatAPI.Utilities;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatAPI
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
            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod()
                .AllowAnyHeader());
            });

            services.AddControllers();
            services.AddSignalR();


            services.Configure<RabbitMqConfiguration>(
                Configuration.GetSection(nameof(RabbitMqConfiguration)));

            services.AddSingleton<IRabbitMqConfiguration>(sp =>
                sp.GetRequiredService<IOptions<RabbitMqConfiguration>>().Value);

            services.AddSingleton<IRabbitMQService, RabbitMqService>();
            services.AddSingleton<IConsumerService, ConsumerService>();
            services.AddSingleton<IAgentConsumerService, AgentConsumerService>();

            services.AddHostedService<ConsumerHostedService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();


            //app.Use(async (context, next) =>
            //{
            //    var hubContext = context.RequestServices
            //                            .GetRequiredService<IHubContext<ChatHub>>();

            //    //...

            //    if (next != null)
            //    {
            //        await next.Invoke();
            //    }
            //});

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                endpoints.MapHub<ChatHub>("/ChatHub");
            });
        }
    }
}
