using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using OidcApiAuthorization.Abstractions;

namespace SampleFunctionApp
{
    public class HelloFunction
    {
        private readonly IApiAuthorization _apiAuthorization;

        public HelloFunction(IApiAuthorization apiAuthorization)
        {
            _apiAuthorization = apiAuthorization;
        }

        [FunctionName(nameof(HelloFunction))]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "GET", Route = null)] HttpRequest req, ILogger log)
        {
            log.LogWarning($"HTTP trigger function {nameof(HelloFunction)} received a request.");

            /*
             * _apiAuthorization.AuthorizeAsync returns an auth error:
             * 
             *      Authorization Failed. Microsoft.IdentityModel.Tokens.SecurityTokenDecryptionFailedException caught while validating JWT token.Message: 
             *      IDX10609: Decryption failed. No Keys tried: token: 'eyJhbGciOiJkaXIiLCJlbmM...mwL_4OA'.
             */

            var authorizationResult = await _apiAuthorization.AuthorizeAsync(req.Headers);
            if (authorizationResult.Failed)
            {
                log.LogWarning(authorizationResult.FailureReason);
                return new UnauthorizedResult();
            }

            log.LogWarning($"HTTP trigger function {nameof(HelloFunction)} request is authorized.");
            return new OkResult();
        }
    }
}
