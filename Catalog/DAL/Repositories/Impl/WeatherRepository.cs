using Microsoft.EntityFrameworkCore;
using System;
using DAL.Repositories.Interfaces;
using Catalog.DAL.Entities;

namespace DAL.Repositories.Impl
{
    public class WeatherRepository : Repository<Weather>, IWeatherRepository
    {
        public WeatherRepository(DbContext context) : base(context) { }
    }
}