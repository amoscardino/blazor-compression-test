using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorCompressionTest
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddDeflateCompressor("deflate");
            builder.Services.AddGZipCompressor("gzip");
            builder.Services.AddLZMACompressor("lzma");
            builder.Services.AddLZ4Compressor("lz4");

            await builder.Build().RunAsync();
        }
    }
}
