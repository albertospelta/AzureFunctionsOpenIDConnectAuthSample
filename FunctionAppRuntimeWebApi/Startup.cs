using Microsoft.Azure.Functions.Extensions.DependencyInjection;
//using Microsoft.IdentityModel.Logging;

[assembly: FunctionsStartup(typeof(FunctionAppRuntimeWebApi.Startup))]
namespace FunctionAppRuntimeWebApi
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            //IdentityModelEventSource.ShowPII = true;

            //builder.Services.AddOidcApiAuthorization();
        }
    }
}
