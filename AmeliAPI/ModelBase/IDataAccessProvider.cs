namespace ModelBase
{
    public interface IDataAccessProvider
    {

        IRepository GetRepository();

        IRepository<TEntity> GetRepository<TEntity>() where TEntity : EntityBase;

        IUnitOfWork NewUnitOfWork();
    }
}
