namespace TruckDriversFunctionApp.Models
{
    public class NewTruckDriverRequest(string Name, string Location)
    {
        public string Name { get; } = Name;
        public string Location { get; } = Location;
    }
}
