using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayStudioCodingChallenge.Application.AbstractRepository
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T> AddAsync(T entity);
        Task<IEnumerable<T>> AddManyAsync(IEnumerable<T> entities);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(string id);
        Task<bool> RemoveAsync(string id);
        Task<T> UpdateAsync(string id, T entity);
        Task<bool> CheckDocumentExists();
    }
}
