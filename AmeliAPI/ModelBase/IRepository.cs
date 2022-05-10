using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ModelBase
{
    public interface IRepository : IDisposable
    {
        Task SaveAsync();
    }

    public interface IRepository<TEntity> : IRepository where TEntity : EntityBase
    {

        Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>>? filter = null, string includeProperties = "");

        Task<TEntity> GetByIDAsync( params object[] keyValues);

        Task<TEntity> GetByIDAsync(string includeProperties, params object[] keyValues);

        Task InsertAsync(TEntity entity);

        Task DeleteAsync(params object[] keyValues);

        void Delete(TEntity entityToDelete);

        void Delete(IEnumerable<TEntity> entitiesToDelete);

        Task DeleteAsync(Expression<Func<TEntity, bool>> filter);

        Task UpdateAsync(TEntity entityToUpdate);

        Task<int> CountAsync(Expression<Func<TEntity, bool>>? filter = null);
    }
}
