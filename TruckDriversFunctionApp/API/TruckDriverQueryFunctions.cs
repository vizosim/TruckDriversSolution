using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System.Net;
using TruckDriversFunctionApp.Application;
using TruckDriversFunctionApp.Models;
using TruckDriversFunctionApp.Persistence;

namespace TruckDriversFunctionApp.API
{
    public class TruckDriverQueryFunctions
    {
        private readonly ITruckDriverApplicationQueryService _driverQueryService;
        private readonly ILogger<TruckDriverQueryFunctions> _logger;

        public TruckDriverQueryFunctions(
            ILogger<TruckDriverQueryFunctions> logger,
            ITruckDriverApplicationQueryService driverQueryService)
        {
            _logger = logger;
            _driverQueryService = driverQueryService;
        }

        [Function("GetDrivers")]
        [OpenApiOperation(operationId: "GetDrivers")]
        [OpenApiParameter(name: "location", In = ParameterLocation.Query, Required = false, Type = typeof(string), Description = "Truck Driver's location")]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(TruckDriver[]),
            Description = "The OK response message containing a JSON result.")]
        public async Task<IActionResult> GetDrivers([HttpTrigger(
            AuthorizationLevel.Function,
            "get", Route = "drivers")]
        HttpRequest req, CancellationToken cancellationToken)
        {
            _logger.LogInformation("GetDrivers function processed a request.");

            var location = req.Query["location"];

            var result = await _driverQueryService.GetAsync(new TruckDriverFilter(location), cancellationToken);
            return new OkObjectResult(result);
        }
    }
}
