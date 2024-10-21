using TruckDriversFunctionApp.Models;

namespace TruckDriversFunctionApp.Application
{
    public interface ITruckDriverApplicationCreateService
    {
        Task<TruckDriver> CreateAsync(NewTruckDriverRequest request, CancellationToken cancellationToken);
    }
}
