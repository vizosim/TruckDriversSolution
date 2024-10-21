using TruckDriversFunctionApp.Models;

namespace TruckDriversFunctionApp.Persistence
{
    public interface ITruckDriverRepository
    {
        Task<List<TruckDriver>> GetAsync(TruckDriverFilter filter, CancellationToken cancellationToken);

        Task<string> CreateAsync(TruckDriver driver, CancellationToken cancellationToken);
    }
}
