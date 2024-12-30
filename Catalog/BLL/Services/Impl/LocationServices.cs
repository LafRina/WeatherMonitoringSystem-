using AutoMapper;
using Catalog.DAL.Entities;
using DAL.Repositories.Interfaces;
using Catalog.BLL.DTO;
using Security.Identity;
using BLL.Services.Interfaces;
using Security;

namespace BLL.Services.Impl
{
    public class LocationServices : ILocationServices
    {
        private readonly ILocationRepository _locationRepository;
        private readonly IMapper _mapper;

        public LocationServices(ILocationRepository locationRepository, IMapper mapper)
        {
            _locationRepository = locationRepository;
            _mapper = mapper;
        }

        public async Task<LocationDTO> GetByIdAsync(int id)
        {
            var user = SecurityContext.GetUser();
            var userType = user.GetType();
            if (userType != typeof(Admin))
            {
                throw new MethodAccessException();
            }

            var dto = await _locationRepository.GetByIdAsync(id);
            if (dto == null)
            {
                return null;
            }

            return _mapper.Map<LocationDTO>(dto);
        }

        public async Task<IEnumerable<LocationDTO>> GetAllAsync()
        {
            var user = SecurityContext.GetUser();
            var userType = user.GetType();
            if (userType != typeof(Admin))
            {
                throw new MethodAccessException();
            }

            var dto = await _locationRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<LocationDTO>>(dto);
        }

        public async Task AddAsync(LocationDTO locationDTO)
        {
            var user = SecurityContext.GetUser();
            var userType = user.GetType();
            if (userType != typeof(Admin))
            {
                throw new MethodAccessException();
            }

            var dto = _mapper.Map<Location>(locationDTO);
            await _locationRepository.AddAsync(dto);
        }

        public async Task UpdateAsync(LocationDTO locationDTO)
        {
            var user = SecurityContext.GetUser();
            var userType = user.GetType();
            if (userType != typeof(Admin))
            {
                throw new MethodAccessException();
            }

            var dto = _mapper.Map<Location>(locationDTO);
            await _locationRepository.UpdateAsync(dto);
        }

        public async Task DeleteAsync(int id)
        {
            var user = SecurityContext.GetUser();
            var userType = user.GetType();
            if (userType != typeof(Admin))
            {
                throw new MethodAccessException();
            }

            await _locationRepository.DeleteAsync(id);
        }
    }
}
