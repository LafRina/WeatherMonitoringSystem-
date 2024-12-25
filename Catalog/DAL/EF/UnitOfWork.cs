using System;
using Microsoft.EntityFrameworkCore;
using DAL.Repositories.Interfaces;
using Catalog.DAL.UnitOfWork;
using DAL.Repositories.Impl;

namespace Catalog.DAL.EF
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;
        private IUserRepository _userRepository;
        private IWeatherRepository _weatherRepository;
        private ILocationRepository _locationRepository;

        public UnitOfWork(DbContext context)
        {
            _context = context;
        }

        public IUserRepository Users
        {
            get
            {
                if (_userRepository == null)
                {
                    _userRepository = new UserRepository(_context);
                }
                return _userRepository;
            }
        }

        public IWeatherRepository Weathers
        {
            get
            {
                if (_weatherRepository == null)
                {
                    _weatherRepository = new WeatherRepository(_context);
                }
                return _weatherRepository;
            }
        }

        public ILocationRepository Locations
        {
            get
            {
                if (_locationRepository == null)
                {
                    _locationRepository = new LocationRepository(_context);
                }
                return _locationRepository;
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
