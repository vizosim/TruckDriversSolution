using TruckDriversFunctionApp.Models;
using TruckDriversFunctionApp.Persistence;

namespace TruckDriversFunctionApp.Application
{
    public interface ITruckDriverApplicationQueryService
    {
        Task<IReadOnlyCollection<TruckDriver>> GetAsync(TruckDriverFilter filter, CancellationToken cancellationToken);
    }
}
