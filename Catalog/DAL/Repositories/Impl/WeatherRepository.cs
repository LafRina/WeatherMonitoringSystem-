using Microsoft.EntityFrameworkCore;
using System;
using DAL.Repositories.Interfaces;
using Catalog.DAL.Entities;

namespace DAL.Repositories.Impl
{
    public class WeatherRepository : Repository<Weather>, IWeatherRepository
    {
        public WeatherRepository(DbContext context) : base(context) { }
        
        public async Task<Weather> GetWeatherByLocationAsync(int locationId)
        {
            return await _context.Set<Weather>() 
                .Where(w => w.LocationId == locationId)
                .OrderByDescending(w => w.Id) // Отримати останнє оновлення погоди
                .FirstOrDefaultAsync();
        }
    }
}