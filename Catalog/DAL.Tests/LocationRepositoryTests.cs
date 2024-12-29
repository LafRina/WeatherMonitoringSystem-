using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog.DAL.Entities;
using Xunit;
using Moq;
using DAL.Repositories.Impl;

namespace DAL.Tests
{
    public class LocationRepositoryTests : RepositoryTestsBase<Location>
    {
        private readonly LocationRepository _repository;

        public LocationRepositoryTests()
        {
            _repository = new LocationRepository(_mockContext.Object);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsAllLocations()
        {
            // Arrange
            var locations = new List<Location>
            {
                new Location { Id = 1, Name = "Location1" },
                new Location { Id = 2, Name = "Location2" }
            }.AsQueryable();

            _mockDbSet.As<IQueryable<Location>>().Setup(m => m.Provider).Returns(locations.Provider);
            _mockDbSet.As<IQueryable<Location>>().Setup(m => m.Expression).Returns(locations.Expression);
            _mockDbSet.As<IQueryable<Location>>().Setup(m => m.ElementType).Returns(locations.ElementType);
            _mockDbSet.As<IQueryable<Location>>().Setup(m => m.GetEnumerator()).Returns(locations.GetEnumerator());

            // Act
            var result = await _repository.GetAllAsync();

            // Assert
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsCorrectLocation()
        {
            // Arrange
            var location = new Location { Id = 1, Name = "Location1" };
            _mockDbSet.Setup(m => m.FindAsync(1)).ReturnsAsync(location);

            // Act
            var result = await _repository.GetByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Location1", result.Name);
        }

        [Fact]
        public async Task AddAsync_AddsLocationToDbSet()
        {
            // Arrange
            var location = new Location { Id = 1, Name = "Location1" };

            // Act
            await _repository.AddAsync(location);

            // Assert
            _mockDbSet.Verify(m => m.AddAsync(location, default), Times.Once);
            _mockContext.Verify(m => m.SaveChangesAsync(default), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_UpdatesLocationInDbSet()
        {
            // Arrange
            var location = new Location { Id = 1, Name = "Location1" };

            // Act
            await _repository.UpdateAsync(location);

            // Assert
            _mockDbSet.Verify(m => m.Update(location), Times.Once);
            _mockContext.Verify(m => m.SaveChangesAsync(default), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_RemovesLocationFromDbSet()
        {
            // Arrange
            var location = new Location { Id = 1, Name = "Location1" };
            _mockDbSet.Setup(m => m.FindAsync(1)).ReturnsAsync(location);

            // Act
            await _repository.DeleteAsync(1);

            // Assert
            _mockDbSet.Verify(m => m.Remove(location), Times.Once);
            _mockContext.Verify(m => m.SaveChangesAsync(default), Times.Once);
        }
    }
}
