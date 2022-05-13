using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ModelBase
{


    public interface ICRUDServiceBase<TEntity> : IServiceBase where TEntity : EntityBase
    {
        Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>>? filter = null, string includeProperties = "");
        Task<TEntity> GetByIDAsync(string includeProperties = "", params object[] keyValues);
        Task<TEntity> InsertAsync( TEntity entity);
        Task DeleteAsync( params object[] keyValues);
        Task UpdateAsync( TEntity entity);

    }
}
