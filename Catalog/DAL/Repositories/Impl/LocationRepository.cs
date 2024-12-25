using Catalog.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using DAL.Repositories.Interfaces;

namespace DAL.Repositories.Impl
{
    public class LocationRepository : Repository<Location>, ILocationRepository
    {
        public LocationRepository(DbContext context) : base(context) { }
    }
}