using FleetManagement.Controllers;
using FleetManagement.Data;
using FleetManagement.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace FleetManagement.Tests
{
    public class VehiclesApiControllerTests
    {
        private readonly Mock<FleetDbContext> _mockContext;
        private readonly VehiclesApiController _controller;

        public VehiclesApiControllerTests()
        {
            // Mocking FleetDbContext
            var options = new DbContextOptionsBuilder<FleetDbContext>()
                        .UseInMemoryDatabase(databaseName: "Fleet_Management")
                        .Options;

            _mockContext = new Mock<FleetDbContext>(options);
            _controller = new VehiclesApiController(_mockContext.Object);
        }

        // valid data
        [Fact]
        public async Task PostLocation_ValidData_ReturnsOkResult()
        {
           
            var location = new VehicleLocation
            {
                VehicleId = "V123",
                Latitude = 40.7128m,
                Longitude = -74.0060m
            };

            // Act
            var result = await _controller.PostLocation(location);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<dynamic>(okResult.Value);
            Assert.Equal("Location data saved successfully", response.Message);
        }

        // Empty VehicleId
        [Fact]
        public async Task PostLocation_InvalidData_ReturnsBadRequest()
        {
           
            var location = new VehicleLocation
            {
                VehicleId = "",
                Latitude = 40.7128m,
                Longitude = -74.0060m
            };

            
            var result = await _controller.PostLocation(location);

            
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("VehicleId and location data are required.", badRequestResult.Value);
        }

        // invalid latitude
        [Fact]
        public async Task PostLocation_InvalidLatitude_ReturnsBadRequest()
        {
            
            var location = new VehicleLocation
            {
                VehicleId = "V123",
                Latitude = 100,  // Invalid latitude
                Longitude = -74.0060m
            };

            var result = await _controller.PostLocation(location);

          
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Invalid latitude value.", badRequestResult.Value);
        }

        // GetLatestLocations
        [Fact]
        public void GetLatestLocations_ReturnsOkResult()
        {
            
            var location1 = new VehicleLocation
            {
                VehicleId = "V123",
                Latitude = 40.7128m,
                Longitude = -74.0060m,
                Timestamp = DateTime.UtcNow.AddMinutes(-10)
            };

            var location2 = new VehicleLocation
            {
                VehicleId = "V123",
                Latitude = 40.7138m,
                Longitude = -74.0070m,
                Timestamp = DateTime.UtcNow
            };

            _mockContext.Object.VehicleLocations.Add(location1);
            _mockContext.Object.VehicleLocations.Add(location2);
            _mockContext.Object.SaveChanges();

           
            var result = _controller.GetLatestLocations();

          
            var okResult = Assert.IsType<OkObjectResult>(result);
            var locations = Assert.IsAssignableFrom<System.Collections.Generic.List<VehicleLocation>>(okResult.Value);
            Assert.Single(locations);  // Should only return the latest location
            Assert.Equal("V123", locations[0].VehicleId);
        }
    }
}
