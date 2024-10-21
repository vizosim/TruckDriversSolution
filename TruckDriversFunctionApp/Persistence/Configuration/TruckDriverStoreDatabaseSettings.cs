namespace TruckDriversFunctionApp.Persistence.Configuration
{
    public class TruckDriverStoreDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string TruckDriverCollectionName { get; set; } = null!;
    }
}
