using AutoMapper;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Catalog.DAL.Entities;
using DAL.Repositories.Interfaces;
using Catalog.BLL.DTO;
using Security;
using Security.Identity;
using BLL.Services.Impl;

namespace BLL.Tests
{
    public class LocationServicesTests
    {
        private readonly Mock<ILocationRepository> _locationRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly LocationServices _locationServices;

        public LocationServicesTests()
        {
            _locationRepositoryMock = new Mock<ILocationRepository>();
            _mapperMock = new Mock<IMapper>();

            SecurityContext.SetUser(new Admin(1, "admin"));

            _locationServices = new LocationServices(_locationRepositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task GetByIdAsync_WhenCalledByAdmin_ReturnsMappedLocationDTO()
        {
            // Arrange
            var location = new Location { Id = 1, Name = "Test Location" };
            var locationDTO = new LocationDTO { Id = 1, Name = "Test Location" };

            _locationRepositoryMock.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(location);
            _mapperMock.Setup(m => m.Map<LocationDTO>(location)).Returns(locationDTO);

            // Act
            var result = await _locationServices.GetByIdAsync(1);

            // Assert
            Assert.Equal(locationDTO, result);
        }

        [Fact]
        public async Task GetAllAsync_WhenCalledByAdmin_ReturnsMappedLocationDTOs()
        {
            // Arrange
            var locations = new List<Location>
            {
                new Location { Id = 1, Name = "Location 1" },
                new Location { Id = 2, Name = "Location 2" }
            };
            var locationDTOs = new List<LocationDTO>
            {
                new LocationDTO { Id = 1, Name = "Location 1" },
                new LocationDTO { Id = 2, Name = "Location 2" }
            };

            _locationRepositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(locations);
            _mapperMock.Setup(m => m.Map<IEnumerable<LocationDTO>>(locations)).Returns(locationDTOs);

            // Act
            var result = await _locationServices.GetAllAsync();

            // Assert
            Assert.Equal(locationDTOs, result);
        }

        [Fact]
        public async Task AddAsync_WhenCalledByAdmin_MapsAndAddsLocation()
        {
            // Arrange
            var locationDTO = new LocationDTO { Id = 1, Name = "New Location" };
            var location = new Location { Id = 1, Name = "New Location" };

            _mapperMock.Setup(m => m.Map<Location>(locationDTO)).Returns(location);

            // Act
            await _locationServices.AddAsync(locationDTO);

            // Assert
            _locationRepositoryMock.Verify(x => x.AddAsync(location), Times.Once);
        }


        [Fact]
        public async Task DeleteAsync_WhenCalledByAdmin_DeletesLocation()
        {
            // Act & Arrange
            await _locationServices.DeleteAsync(1);

            // Assert
            _locationRepositoryMock.Verify(x => x.DeleteAsync(1), Times.Once);
        }
    }
}
