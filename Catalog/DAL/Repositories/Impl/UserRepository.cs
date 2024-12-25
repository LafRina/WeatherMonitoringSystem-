using Microsoft.EntityFrameworkCore;
using System;
using DAL.Repositories.Interfaces;
using Catalog.DAL.Entities;

namespace DAL.Repositories.Impl
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DbContext context) : base(context) { }
    }
}