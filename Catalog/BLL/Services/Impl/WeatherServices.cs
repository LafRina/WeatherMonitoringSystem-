using AutoMapper;
using Catalog.DAL.Entities;
using DAL.Repositories.Interfaces;
using Catalog.BLL.DTO;
using Security.Identity;
using BLL.Services.Interfaces;
using Security;

namespace BLL.Services.Impl
{
    public class WeatherServices : IWeatherServices
    {
        private readonly IWeatherRepository _weatherRepository;
        private readonly IMapper _mapper;

        public WeatherServices(IWeatherRepository weatherRepository, IMapper mapper)
        {
            _weatherRepository = weatherRepository;
            _mapper = mapper;
        }

        public async Task<WeatherDTO> GetByIdAsync(int id)
        {
            var user = SecurityContext.GetUser();
            var userType = user.GetType();
            if (userType != typeof(Admin))
            {
                throw new MethodAccessException();
            }

            var dto = await _weatherRepository.GetByIdAsync(id);
            if (dto == null)
            {
                return null;
            }

            return _mapper.Map<WeatherDTO>(dto);
        }

        public async Task<IEnumerable<WeatherDTO>> GetAllAsync()
        {
            var user = SecurityContext.GetUser();
            var userType = user.GetType();
            if (userType != typeof(Admin))
            {
                throw new MethodAccessException();
            }

            var dto = await _weatherRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<WeatherDTO>>(dto);
        }

        public async Task AddAsync(WeatherDTO weatherDTO)
        {
            var user = SecurityContext.GetUser();
            var userType = user.GetType();
            if (userType != typeof(Admin))
            {
                throw new MethodAccessException();
            }

            var entity = _mapper.Map<Weather>(weatherDTO);
            await _weatherRepository.AddAsync(entity);
        }

        public async Task UpdateAsync(WeatherDTO weatherDTO)
        {
            var user = SecurityContext.GetUser();
            var userType = user.GetType();
            if (userType != typeof(Admin))
            {
                throw new MethodAccessException();
            }

            var entity = _mapper.Map<Weather>(weatherDTO);
            await _weatherRepository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var user = SecurityContext.GetUser();
            var userType = user.GetType();
            if (userType != typeof(Admin))
            {
                throw new MethodAccessException();
            }
            
            await _weatherRepository.DeleteAsync(id);
        }
    }
}
