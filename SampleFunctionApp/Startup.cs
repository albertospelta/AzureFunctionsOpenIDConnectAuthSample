using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Logging;
using OidcApiAuthorization;

[assembly: FunctionsStartup(typeof(SampleFunctionApp.Startup))]
namespace SampleFunctionApp
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            IdentityModelEventSource.ShowPII = true;

            builder.Services.AddOidcApiAuthorization();
        }
    }
}
