using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using MovieApp.Client.Helpers;
using MovieApp.Client.Repositories;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace MovieApp.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddHttpClient("MovieApp.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
                .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

            // Supply HttpClient instances that include access tokens when making requests to the server project
            builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("MovieApp.ServerAPI"));

            builder.Services.AddApiAuthorization();

            builder.Services.AddScoped<MovieService, MovieService>();
            builder.Services.AddScoped<MovieRepository, MovieRepository>();

            await builder.Build().RunAsync();
        }
    }
}