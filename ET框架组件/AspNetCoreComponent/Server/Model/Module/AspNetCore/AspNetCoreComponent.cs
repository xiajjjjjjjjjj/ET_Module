using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace ETModel.Module.AspNetCore
{
    public class AspNetCoreComponent : Component
    {
        public IServiceProvider ServiceProvider;

        public AspNetCoreComponent()
        {
            IWebHost webHost = CreateWebHostBuilder(Environment.GetCommandLineArgs()).Build();

            ServiceProvider = webHost.Services;

            webHost.StartAsync();
        }

        public IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            HttpConfig httpConfig = Game.Scene.GetComponent<StartConfigComponent>().StartConfig.GetComponent<HttpConfig>();

            string url = "http://*:5000";

            if (httpConfig != null)
            {
                url = httpConfig.Url;
            }

            return WebHost.CreateDefaultBuilder(args)
                    .UseKestrel()
                    .UseUrls(url)
                    .UseStartup<Startup>();
        }
    }
}