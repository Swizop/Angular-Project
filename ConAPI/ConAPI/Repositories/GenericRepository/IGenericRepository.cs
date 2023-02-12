using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConAPI.Repositories
{
    public interface IGenericRepository<TEntity>
    {
        // Get

        IQueryable<TEntity> GetAll();
        Task<TEntity> GetByIdAsync(int id);

        // Create - Insert
        void Create(TEntity entity);
        void CreateRange(IEnumerable<TEntity> entities);

        // Update
        void Update(TEntity entity);

        // Delete
        void Delete(TEntity entity);
        void DeleteRange(IEnumerable<TEntity> entities);

        // Save
        Task<bool> SaveAsync();
    }
}
