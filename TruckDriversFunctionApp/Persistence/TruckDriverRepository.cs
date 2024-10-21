using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TruckDriversFunctionApp.Models;
using TruckDriversFunctionApp.Persistence.Configuration;

namespace TruckDriversFunctionApp.Persistence
{
    public class TruckDriverRepository : ITruckDriverRepository
    {
        private readonly IMongoCollection<TruckDriver> _truckDriversCollection;

        public TruckDriverRepository(IOptions<TruckDriverStoreDatabaseSettings> databaseSettings)
        {
            var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);

            _truckDriversCollection = mongoDatabase.GetCollection<TruckDriver>(
                databaseSettings.Value.TruckDriverCollectionName);
        }

        public async Task<string> CreateAsync(TruckDriver driver, CancellationToken cancellationToken)
        {
            await _truckDriversCollection.InsertOneAsync(driver, new InsertOneOptions { BypassDocumentValidation = true }, cancellationToken);
            return driver.Id;
        }

        public async Task<List<TruckDriver>> GetAsync(TruckDriverFilter filter, CancellationToken cancellationToken)
        {
            var locationFilter = string.IsNullOrEmpty(filter.Location)
                ? Builders<TruckDriver>.Filter.Empty
                : Builders<TruckDriver>.Filter.Eq(driver => driver.Location, filter.Location);

            return await _truckDriversCollection.Find(locationFilter).ToListAsync(cancellationToken);
        }
    }
}
