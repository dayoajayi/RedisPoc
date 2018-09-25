using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace RedisPoc.Web
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
            ConnectionMultiplexer RedisConn = null;


            services.AddDistributedRedisCache(options =>
            {
                options.Configuration = "localhost:6379";
            });

            var endpointCollection = new EndPointCollection();
            endpointCollection.Add("localhost:6379");

            if (RedisConn == null)
            {
                ConfigurationOptions option = new ConfigurationOptions
                {
                    AbortOnConnectFail = false,
                    //EndPoints = endpointCollection 
                };
                RedisConn = ConnectionMultiplexer.Connect(option);
            }


            services.AddMvc();
            services.AddTransient<IFoo, Foo>();      
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseMvc();
        }
    }
}
