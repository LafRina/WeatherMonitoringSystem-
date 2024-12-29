using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog.DAL.Entities;
using Xunit;
using Moq;
using DAL.Repositories.Impl;

namespace DAL.Tests
{
    public class WeatherRepositoryTests : RepositoryTestsBase<Weather>
    {
        private readonly WeatherRepository _repository;

        public WeatherRepositoryTests()
        {
            _repository = new WeatherRepository(_mockContext.Object);
        }

        [Fact]
        public async Task GetWeatherByLocationAsync_ReturnsCorrectWeather()
        {
            // Arrange
            var weatherData = new List<Weather>
            {
                new Weather { Id = 1, LocationId = 1, Temperature = 20 },
                new Weather { Id = 2, LocationId = 1, Temperature = 22 }
            }.AsQueryable();

            _mockDbSet.As<IQueryable<Weather>>().Setup(m => m.Provider).Returns(weatherData.Provider);
            _mockDbSet.As<IQueryable<Weather>>().Setup(m => m.Expression).Returns(weatherData.Expression);
            _mockDbSet.As<IQueryable<Weather>>().Setup(m => m.ElementType).Returns(weatherData.ElementType);
            _mockDbSet.As<IQueryable<Weather>>().Setup(m => m.GetEnumerator()).Returns(weatherData.GetEnumerator());

            // Act
            var result = await _repository.GetWeatherByLocationAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(22, result.Temperature);
        }
    }
}