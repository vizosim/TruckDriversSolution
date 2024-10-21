using TruckDriversFunctionApp.Models;
using TruckDriversFunctionApp.Persistence;

namespace TruckDriversFunctionApp.Application
{
    public class TruckDriverApplicationCreateService(ITruckDriverRepository repository) : ITruckDriverApplicationCreateService
    {
        private readonly ITruckDriverRepository _repository = repository;

        public async Task<TruckDriver> CreateAsync(NewTruckDriverRequest request, CancellationToken cancellationToken)
        {
            TruckDriver newDriver = new() { Name = request.Name, Location = request.Location };
            var driverId = await _repository.CreateAsync(newDriver, cancellationToken);
            newDriver.Id = driverId;

            return newDriver;
        }
    }
}
