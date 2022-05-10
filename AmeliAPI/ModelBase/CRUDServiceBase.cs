using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ModelBase
{
    public class ServiceBase : IServiceBase
    {
        public readonly IDataAccessProvider _DataAccessProvider;

        public ServiceBase(IDataAccessProvider dataAccessProvider)
        {
            _DataAccessProvider = dataAccessProvider;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // free managed resources
            }
            // free native resources if there are any.
        }
    }

    public class CRUDServiceBase<TEntity> : ServiceBase, ICRUDServiceBase<TEntity> where TEntity : EntityBase, new()
    {
        public CRUDServiceBase(IDataAccessProvider dataAccessProvider) : base(dataAccessProvider)
        {

        }


        public virtual async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>>? filter = null, string includeProperties = "")
        {
            using (var rep = _DataAccessProvider.GetRepository<TEntity>())
            {
                return await rep.GetAsync(filter, includeProperties);
            }
        }

        public virtual async Task<TEntity> GetByIDAsync(string includeProperties = "", params object[] keyValues)
        {
            using (var rep = _DataAccessProvider.GetRepository<TEntity>())
            {
                return await rep.GetByIDAsync(keyValues: keyValues, includeProperties: includeProperties);
            }
        }



        public async Task DeleteAsync(params object[] keyValues)
        {
            using (var unitOfWork = _DataAccessProvider.NewUnitOfWork())
            {
                await DeleteAsync(unitOfWork: unitOfWork, keyValues: keyValues);
                await unitOfWork.SaveAsync();
            }
        }

        public async Task DeleteAsync(TEntity entity)
        {
            using (var unitOfWork = _DataAccessProvider.NewUnitOfWork())
            {
                await DeleteAsync(unitOfWork: unitOfWork, keyValues: entity.PrimaryKey);
                await unitOfWork.SaveAsync();
            }
        }

        public async Task Delete(IEnumerable<TEntity> entities)
        {
            using (var unitOfWork = _DataAccessProvider.NewUnitOfWork())
            {
                await Task.WhenAll(entities.Select(entity => DeleteAsync(unitOfWork: unitOfWork, keyValues: entity.PrimaryKey)));
                await unitOfWork.SaveAsync();
            }
        }

        protected internal virtual async Task DeleteAsync(IUnitOfWork unitOfWork, params object[] keyValues)
        {
            if(unitOfWork is null)
                await DeleteAsync(keyValues: keyValues);
            else
                await unitOfWork.GetRepository<TEntity>().DeleteAsync(keyValues);
        }

        protected internal async Task DeleteAsync(TEntity entity, IUnitOfWork unitOfWork)
        {
            if(unitOfWork is null)
                await DeleteAsync(keyValues: entity.PrimaryKey);
            else
                await DeleteAsync(unitOfWork: unitOfWork, keyValues: entity.PrimaryKey);
        }

        protected internal async Task DeleteAsync(IEnumerable<TEntity> entities, IUnitOfWork unitOfWork)
        {
            if(unitOfWork is null)
                await Task.WhenAll(entities.Select(entity => DeleteAsync(keyValues: entity.PrimaryKey)));
            else
                await Task.WhenAll(entities.Select(entity => DeleteAsync(unitOfWork: unitOfWork, keyValues: entity.PrimaryKey)));
        }



        public virtual async Task<TEntity> InsertAsync(TEntity entity)
        {
            using (var unitOfWork = _DataAccessProvider.NewUnitOfWork())
            {
                await InsertAsync(unitOfWork, entity);
                await unitOfWork.SaveAsync();
                return entity;
            }
        }

        protected internal virtual async Task<TEntity> InsertAsync(IUnitOfWork unitOfWork, TEntity entity)
        {
            if (unitOfWork is null)
                await InsertAsync(entity);
            else
                await unitOfWork.GetRepository<TEntity>().InsertAsync(entity);
            return entity;
        }



        public async Task UpdateAsync(TEntity entity)
        {
            using (var unitOfWork = _DataAccessProvider.NewUnitOfWork())
            {
                await UpdateAsync(unitOfWork, entity);
                await unitOfWork.SaveAsync();
            }
        }

        protected internal virtual async Task UpdateAsync(IUnitOfWork unitOfWork, TEntity entity)
        {
            if (unitOfWork is null)
                await UpdateAsync(entity);
            else
                await unitOfWork.GetRepository<TEntity>().UpdateAsync(entity);
        }


        public virtual async Task<int> CountAsync(Expression<Func<TEntity, bool>>? filter = null)
        {
            using (var rep = _DataAccessProvider.GetRepository<TEntity>())
            {
                return await rep.CountAsync(filter);
            }
        }
    }
}
