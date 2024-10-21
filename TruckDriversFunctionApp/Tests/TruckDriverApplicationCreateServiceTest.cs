using TruckDriversFunctionApp.Application;
using TruckDriversFunctionApp.Models;
using TruckDriversFunctionApp.Persistence;

namespace TruckDriversFunctionApp.Tests
{
    public class TruckDriverApplicationCreateServiceTest
    {
        private readonly TruckDriverApplicationCreateService _service;
        private readonly Mock<ITruckDriverRepository> _repository;

        public TruckDriverApplicationCreateServiceTest()
        {
            _repository = new Mock<ITruckDriverRepository>();
            _service = new TruckDriverApplicationCreateService(_repository.Object);
        }

        [Fact]
        public async Task CreateAsync_ShouldReturnNewDriver()
        {
            // Arange
            var request = new NewTruckDriverRequest("Name", "Location");

            var expectedDriver = new TruckDriver { Id = "driverId", Name = "Name", Location = "Location" };

            _repository.Setup(_ => _.CreateAsync(It.IsAny<TruckDriver>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync("driverId");

            // Act
            var result = await _service.CreateAsync(request, It.IsAny<CancellationToken>());

            // Assert
            result.Should().BeEquivalentTo(expectedDriver);
        }
    }
}
