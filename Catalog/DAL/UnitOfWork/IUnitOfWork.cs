using DAL.Repositories.Interfaces;

namespace Catalog.DAL.UnitOfWork {
    public interface IUnitOfWork : IDisposable {
        IUserRepository Users { get; }
        IWeatherRepository Weathers { get; }
        ILocationRepository Locations { get; }
        void Save();
    }
}