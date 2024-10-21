using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System.Net;
using TruckDriversFunctionApp.Application;
using TruckDriversFunctionApp.Models;

namespace TruckDriversFunctionApp.API
{

    public class TruckDriverCreateFunctions
    {
        private readonly ITruckDriverApplicationCreateService _driverCreateService;
        private readonly ILogger<TruckDriverCreateFunctions> _logger;

        public TruckDriverCreateFunctions(
            ILogger<TruckDriverCreateFunctions> logger,
            ITruckDriverApplicationCreateService driverCreateService)
        {
            _logger = logger;
            _driverCreateService = driverCreateService;
        }

        [Function("CreateDriver")]
        [OpenApiOperation(operationId: "CreateDriver")]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiRequestBody("application/json", typeof(NewTruckDriverRequest),
            Description = "JSON request body containing { name, location } of Driver")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(TruckDriver),
            Description = "The OK response message containing a new Driver object.")]
        public async Task<IActionResult> CreateDriver([HttpTrigger(
            AuthorizationLevel.Function,
            "post", Route = "driver")]
        HttpRequest req, CancellationToken cancellationToken)
        {
            _logger.LogInformation("CreateDriver function processed a request.");

            var request = await ReadRequestBody(req.Body, cancellationToken);
            if (request is null)
            {
                return new BadRequestObjectResult("Request is not valid.");
            }

            if (string.IsNullOrEmpty(request.Name) || string.IsNullOrEmpty(request.Location))
            {
                return new BadRequestObjectResult("No valid request parameter.");
            }

            var result = await _driverCreateService.CreateAsync(request, cancellationToken);

            return new OkObjectResult(result);
        }

        private async Task<NewTruckDriverRequest?> ReadRequestBody(Stream stream, CancellationToken cancellationToken)
        {
            var body = await new StreamReader(stream).ReadToEndAsync(cancellationToken);
            var driverRequest = JsonConvert.DeserializeObject<NewTruckDriverRequest>(body);
            return driverRequest;
        }
    }
}
