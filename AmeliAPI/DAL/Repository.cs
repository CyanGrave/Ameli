using ModelBase;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DAL
{

    public class Repository : IRepository
    {
        protected Context _Context;

        private bool _disposed = false;


        public Repository(Context context)
        {
            _Context = context;
        }

        public async Task SaveAsync()
        {
            await _Context.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
                if (disposing)
                    _Context?.Dispose();

            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }

    public class Repository<TEntity> : Repository, IRepository<TEntity> where TEntity : EntityBase
    {
        internal DbSet<TEntity> _DbSet;

        public Repository(Context context) : base(context)
        {
            this._DbSet = _Context.Set<TEntity>();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>>? filter = null, string includeProperties = "")
        {
            IQueryable<TEntity> query = _DbSet;

            if (filter != null)
                query = query.Where(filter);

            if (!string.IsNullOrEmpty(includeProperties))
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    query = query.Include(includeProperty);

            return await query.ToListAsync();
        }


        public virtual async Task<TEntity> GetByIDAsync(params object[] keyValues)
        {
            return await GetByIDAsync(keyValues: keyValues, includeProperties: "");
        }

        public virtual async Task<TEntity> GetByIDAsync(string includeProperties, params object[] keyValues)
        {
            var result = await _DbSet.FindAsync(keyValues);

            if (!string.IsNullOrEmpty(includeProperties))

                await Task.WhenAll(includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(async include =>
                {
                    await LoadNavigationPropertyRecursiveAsync(result, include.Trim().Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries));
                }));


            return result;
        }



        public virtual async Task InsertAsync(TEntity entity)
        {
            await _DbSet.AddAsync(entity);
        }

        public virtual async Task DeleteAsync(params object[] keyValues)
        {
            TEntity entityToDelete = await _DbSet.FindAsync(keyValues);
            if (entityToDelete != null)
                Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (entityToDelete == null)
                return;

            if (_Context.Entry(entityToDelete).State == EntityState.Detached)
                this._DbSet.Attach(entityToDelete);

            this._DbSet.Remove(entityToDelete);
        }

        public virtual void Delete(IEnumerable<TEntity> entitiesToDelete)
        {
            if (entitiesToDelete == null)
                return;

            _DbSet.AttachRange(entitiesToDelete.Where(entity => entity != null && _Context.Entry(entity).State == EntityState.Detached));


            this._DbSet.RemoveRange(entitiesToDelete.Where(e => e != null));
        }

        public virtual async Task DeleteAsync(Expression<Func<TEntity, bool>> filter)
        {
            Delete( await GetAsync(filter));
        }

        public virtual async Task UpdateAsync(TEntity entityToUpdate)
        {
            var entityInDB = await this._DbSet.FindAsync(entityToUpdate.PrimaryKey);
            if (entityInDB != null)
                this._Context.Entry(entityInDB).State = EntityState.Detached;

            if (this._Context.Entry(entityToUpdate).State == EntityState.Detached)
            {
                this._DbSet.Attach(entityToUpdate);
            }

            _Context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public virtual async Task<int> CountAsync(Expression<Func<TEntity, bool>> filter) => filter != null
                                                                            ? await this._DbSet.CountAsync(filter)
                                                                            : await this._DbSet.CountAsync();


        #region internal

        private async Task LoadNavigationPropertyAsync(object entity, string include)
        {
            var member = _Context.Entry(entity).Member(include);

            if (member is ReferenceEntry)
                await _Context.Entry(entity).Reference(include)?.LoadAsync();
            else if (member is CollectionEntry)
                await _Context.Entry(entity).Collection(include)?.LoadAsync();
        }

        private async Task LoadNavigationPropertyRecursiveAsync(object entity, string[] includes, int index = 0)
        {
            await LoadNavigationPropertyAsync(entity, includes[index]);
            object navProp = _Context.Entry(entity).Member(includes[index]).CurrentValue;

            if(includes.Length > index + 1)
            {
                if (IsHashSet(navProp))
                {
                    var tasks = new List<Task>();
                    foreach (var childEntity in navProp as IEnumerable)
                    {
                        tasks.Add(LoadNavigationPropertyRecursiveAsync(childEntity, includes, index + 1));
                    };

                    await Task.WhenAll(tasks);
                }
                else
                {
                    await LoadNavigationPropertyRecursiveAsync(navProp, includes, index + 1);
                }
            }
        }


        private bool IsHashSet(object obj)
        {
            if (obj != null)
            {
                var t = obj.GetType();
                if (t.IsGenericType)
                {
                    return t.GetGenericTypeDefinition() == typeof(HashSet<>);
                }
            }
            return false;
        }


        #endregion
    }

}
