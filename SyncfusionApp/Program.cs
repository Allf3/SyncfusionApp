using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Syncfusion.Blazor;
using ServiceLayer.Services;
using RepoLayer.Repositories.Measurements;

namespace SyncfusionApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NDM5ODE3QDMxMzkyZTMxMmUzMFdCVnhrQjUwUVNoYXRyQmNHRjNHZURlY2gzSXUvVkVLSzN1MzE0ZzByVnc9");


            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddScoped<IGenericRepository, GenericRepository>();
            builder.Services.AddScoped<IMeasurementService, MeasurementService>();
            builder.Services.AddSyncfusionBlazor();


            await builder.Build().RunAsync();
        }
    }
}
