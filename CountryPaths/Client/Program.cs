using Grpc.Net.Client;
using Grpc.Net.Client.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CountryPaths.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
                
            var httpHandler = new GrpcWebHandler(new HttpClientHandler());
            var channel = GrpcChannel.ForAddress("https://localhost:5001", new GrpcChannelOptions { HttpHandler = httpHandler });
           
            builder.Services.AddSingleton(ss => 
            {
                return new Server.Greeter.GreeterClient(channel);
            });
            builder.Services.AddSingleton(ss =>
            {
                return new Server.Protos.CountriesMap.CountriesMapClient(channel);
            });

            await builder.Build().RunAsync();
        }
    }
}
