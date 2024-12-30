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
    public class UserServicesTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly UserServices _userServices;

        public UserServicesTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _mapperMock = new Mock<IMapper>();

            SecurityContext.SetUser(new Admin(1, "admin"));

            _userServices = new UserServices(_userRepositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task GetByIdAsync_WhenCalledByAdmin_ReturnsMappedUserDTO()
        {
            // Arrange
            var user = new User { Id = 1, Username = "Anna" };
            var userDTO = new UserDTO { Id = 1, Username = "Anna" };

            _userRepositoryMock.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(user);
            _mapperMock.Setup(m => m.Map<UserDTO>(user)).Returns(userDTO);

            // Act
            var result = await _userServices.GetByIdAsync(1);

            // Assert
            Assert.Equal(userDTO, result);
        }

        [Fact]
        public async Task GetAllAsync_WhenCalledByAdmin_ReturnsMappedUserDTOs()
        {
            // Arrange
            var user = new List<User>
            {
                new User { Id = 1, Username = "Alina" },
                new User { Id = 2, Username = "Ann" }
            };
            var userDTOs = new List<UserDTO>
            {
                new UserDTO { Id = 1, Username = "Alina" },
                new UserDTO { Id = 2, Username = "Ann" }
            };

            _userRepositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(user);
            _mapperMock.Setup(m => m.Map<IEnumerable<UserDTO>>(user)).Returns(userDTOs);

            // Act
            var result = await _userServices.GetAllAsync();

            // Assert
            Assert.Equal(userDTOs, result);
        }

        [Fact]
        public async Task AddAsync_WhenCalledByAdmin_MapsAndAddsUser()
        {
            // Arrange
            var userDTO = new UserDTO { Id = 1, Username = "Max" };
            var user = new User { Id = 1, Username = "Max" };

            _mapperMock.Setup(m => m.Map<User>(userDTO)).Returns(user);

            // Act
            await _userServices.AddAsync(userDTO);

            // Assert
            _userRepositoryMock.Verify(x => x.AddAsync(user), Times.Once);
        }


        [Fact]
        public async Task DeleteAsync_WhenCalledByAdmin_DeletesUser()
        {
            // Act & Arrange
            await _userServices.DeleteAsync(1);

            // Assert
            _userRepositoryMock.Verify(x => x.DeleteAsync(1), Times.Once);
        }
    }
}
