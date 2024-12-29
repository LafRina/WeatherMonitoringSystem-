using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog.DAL.Entities;
using Xunit;
using Moq;
using DAL.Repositories.Impl;

namespace DAL.Tests
{
    public class UserRepositoryTests : RepositoryTestsBase<User>
    {
        private readonly UserRepository _repository;

        public UserRepositoryTests()
        {
            _repository = new UserRepository(_mockContext.Object);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsAllUsers()
        {
            // Arrange
            var users = new List<User>
            {
                new User { Id = 1, Username = "User1" },
                new User { Id = 2, Username = "User2" }
            }.AsQueryable();

            _mockDbSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(users.Provider);
            _mockDbSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(users.Expression);
            _mockDbSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(users.ElementType);
            _mockDbSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(users.GetEnumerator());

            // Act
            var result = await _repository.GetAllAsync();

            // Assert
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsCorrectUser()
        {
            // Arrange
            var user = new User { Id = 1, Username = "User1" };
            _mockDbSet.Setup(m => m.FindAsync(1)).ReturnsAsync(user);

            // Act
            var result = await _repository.GetByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("User1", result.Username);
        }

        [Fact]
        public async Task AddAsync_AddsUserToDbSet()
        {
            // Arrange
            var user = new User { Id = 1, Username = "User1" };

            // Act
            await _repository.AddAsync(user);

            // Assert
            _mockDbSet.Verify(m => m.AddAsync(user, default), Times.Once);
            _mockContext.Verify(m => m.SaveChangesAsync(default), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_UpdatesUserInDbSet()
        {
            // Arrange
            var user = new User { Id = 1, Username = "User1" };

            // Act
            await _repository.UpdateAsync(user);

            // Assert
            _mockDbSet.Verify(m => m.Update(user), Times.Once);
            _mockContext.Verify(m => m.SaveChangesAsync(default), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_RemovesUserFromDbSet()
        {
            // Arrange
            var user = new User { Id = 1, Username = "User1" };
            _mockDbSet.Setup(m => m.FindAsync(1)).ReturnsAsync(user);

            // Act
            await _repository.DeleteAsync(1);

            // Assert
            _mockDbSet.Verify(m => m.Remove(user), Times.Once);
            _mockContext.Verify(m => m.SaveChangesAsync(default), Times.Once);
        }
    }
}