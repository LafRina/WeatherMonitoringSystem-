using AutoMapper;
using Catalog.DAL.Entities;
using DAL.Repositories.Interfaces;
using Catalog.BLL.DTO;
using Security.Identity;
using BLL.Services.Interfaces;
using Security;

namespace BLL.Services.Impl
{
    public class UserServices : IUserServices
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserServices(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserDTO> GetByIdAsync(int id)
        {
            var user = SecurityContext.GetUser();
            var userType = user.GetType();
            if (userType != typeof(Admin))
            {
                throw new MethodAccessException();
            }

            var dto = await _userRepository.GetByIdAsync(id);
            if (dto == null)
            {
                return null;
            }

            return _mapper.Map<UserDTO>(dto);
        }

        public async Task<IEnumerable<UserDTO>> GetAllAsync()
        {
            var user = SecurityContext.GetUser();
            var userType = user.GetType();
            if (userType != typeof(Admin))
            {
                throw new MethodAccessException();
            }

            var dto = await _userRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<UserDTO>>(dto);
        }

        public async Task AddAsync(UserDTO userDTO)
        {
            var user = SecurityContext.GetUser();
            var userType = user.GetType();
            if (userType != typeof(Admin))
            {
                throw new MethodAccessException();
            }

            var dto = _mapper.Map<User>(userDTO);
            await _userRepository.AddAsync(dto);
        }

        public async Task UpdateAsync(UserDTO userDTO)
        {
            var user = SecurityContext.GetUser();
            var userType = user.GetType();
            if (userType != typeof(Admin))
            {
                throw new MethodAccessException();
            }

            var dto = _mapper.Map<User>(userDTO);
            await _userRepository.UpdateAsync(dto);
        }

        public async Task DeleteAsync(int id)
        {
            var user = SecurityContext.GetUser();
            var userType = user.GetType();
            if (userType != typeof(Admin))
            {
                throw new MethodAccessException();
            }

            await _userRepository.DeleteAsync(id);
        }
    }
}
