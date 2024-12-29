using System;
using System.Collections.Generic;
using System.Linq;
using Catalog.DAL.Entities;
using DAL.Repositories.Impl;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace DAL.Tests
{
    public abstract class RepositoryTestsBase<T> where T : class
    {
        protected readonly Mock<DbContext> _mockContext;
        protected readonly Mock<DbSet<T>> _mockDbSet;

        protected RepositoryTestsBase()
        {
            _mockContext = new Mock<DbContext>();
            _mockDbSet = new Mock<DbSet<T>>();

            _mockContext.Setup(m => m.Set<T>()).Returns(_mockDbSet.Object);
        }
    }
}