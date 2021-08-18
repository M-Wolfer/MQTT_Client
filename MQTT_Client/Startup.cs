using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MQTT_Client.ViewModels;
using MQTTnet.Client;

namespace MQTT_Client
{
    public class Startup
    {
        public IMqttClient clientX;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            //create Client and connect
          //  MQTT_Client.Models.MQTT_Client_Connect clientObj = new MQTT_Client.Models.MQTT_Client_Connect();
            //clientX = clientObj.GetMyClient();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();
            //services.AddSingleton<WeatherForecastService>();
            services.AddSingleton<IMQTTViewModel, MQTTBasicViewModel>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }

        public IMqttClient GetMaClient()
        {
            return clientX;
        }
    }
}
