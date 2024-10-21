using TruckDriversFunctionApp.Models;
using TruckDriversFunctionApp.Persistence;

namespace TruckDriversFunctionApp.Application
{
    public class TruckDriverApplicationQueryService(ITruckDriverRepository repository) : ITruckDriverApplicationQueryService
    {
        private readonly ITruckDriverRepository _repository = repository;

        public async Task<IReadOnlyCollection<TruckDriver>> GetAsync(TruckDriverFilter filter, CancellationToken cancellationToken = default)
        {
            return (await _repository.GetAsync(filter, cancellationToken)).AsReadOnly();
        }
    }
}
