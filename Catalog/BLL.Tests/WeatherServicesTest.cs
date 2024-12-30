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
    public class WeatherServicesTests
    {
        private readonly Mock<IWeatherRepository> _weatherRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly WeatherServices _weatherServices;

        public WeatherServicesTests()
        {
            _weatherRepositoryMock = new Mock<IWeatherRepository>();
            _mapperMock = new Mock<IMapper>();

            SecurityContext.SetUser(new Admin(1, "admin"));

            _weatherServices = new WeatherServices(_weatherRepositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task GetByIdAsync_WhenCalledByAdmin_ReturnsMappedWeatherDTO()
        {
            // Arrange
            var weather = new Weather { Id = 1, Temperature = 20 };
            var weatherDTO = new WeatherDTO { Id = 1, Temperature = 20 };

            _weatherRepositoryMock.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(weather);
            _mapperMock.Setup(m => m.Map<WeatherDTO>(weather)).Returns(weatherDTO);

            // Act
            var result = await _weatherServices.GetByIdAsync(1);

            // Assert
            Assert.Equal(weatherDTO, result);
        }

        [Fact]
        public async Task GetAllAsync_WhenCalledByAdmin_ReturnsMappedWeatherDTOs()
        {
            // Arrange
            var weather = new List<Weather>
            {
                new Weather { Id = 1, Humidity = 73 },
                new Weather { Id = 2, Humidity = 72 }
            };
            var weatherDTOs = new List<WeatherDTO>
            {
                new WeatherDTO { Id = 1, Humidity = 73 },
                new WeatherDTO { Id = 2, Humidity = 72 }
            };

            _weatherRepositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(weather);
            _mapperMock.Setup(m => m.Map<IEnumerable<WeatherDTO>>(weather)).Returns(weatherDTOs);

            // Act
            var result = await _weatherServices.GetAllAsync();

            // Assert
            Assert.Equal(weatherDTOs, result);
        }

        [Fact]
        public async Task AddAsync_WhenCalledByAdmin_MapsAndAddsWeather()
        {
            // Arrange
            var weatherDTO = new WeatherDTO { Id = 1, Humidity = 30 };
            var weather = new Weather { Id = 1, Humidity = 30 };

            _mapperMock.Setup(m => m.Map<Weather>(weatherDTO)).Returns(weather);

            // Act
            await _weatherServices.AddAsync(weatherDTO);

            // Assert
            _weatherRepositoryMock.Verify(x => x.AddAsync(weather), Times.Once);
        }


        [Fact]
        public async Task DeleteAsync_WhenCalledByAdmin_DeletesWeather()
        {
            // Act & Arrange
            await _weatherServices.DeleteAsync(1);

            // Assert
            _weatherRepositoryMock.Verify(x => x.DeleteAsync(1), Times.Once);
        }
    }
}
