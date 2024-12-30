using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Catalog.DAL.Entities;
using Catalog.BLL.DTO;

namespace BLL.Services.Interfaces
{
    public interface IServices<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task AddAsync(T dto);
        Task UpdateAsync(T dto);
        Task DeleteAsync(int id);
    }
}