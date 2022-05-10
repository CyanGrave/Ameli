using ModelBase;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace DAL
{
    public class Context : DbContext
    {
        public virtual string SchemaName { get; } = "Base";

        public Context() : base()
        {
            this.ChangeTracker.LazyLoadingEnabled = false;
        }

        public Context(DbContextOptions options) : base (options)
        {
            this.ChangeTracker.LazyLoadingEnabled = false;
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(SchemaName);

            // equivalent of modelBuilder.Conventions.AddFromAssembly(Assembly.GetExecutingAssembly());
            // look at this answer: https://stackoverflow.com/a/43075152/3419825


            // for the other conventions, we do a metadata model loop
            foreach (var entityType in modelBuilder.Model.GetEntityTypes().Where(x => x.ClrType.BaseType == typeof(EntityBase)))
            {
                // equivalent of modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
                //entityType.Relational().TableName = entityType.DisplayName();

                // equivalent of modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
                // and modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
                entityType.GetForeignKeys()
                    .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade)
                    .ToList()
                    .ForEach(fk => fk.DeleteBehavior = DeleteBehavior.Restrict);
            }

            //modelBuilder.Entity<PropertyValue>().MapToStoredProcedures();



            // get all composite keys (entity decorated by more than 1 [Key] attribute
            foreach (var entity in modelBuilder.Model.GetEntityTypes()
                .Where(t =>
                    t.ClrType.GetProperties()
                        .Count(p => p.CustomAttributes.Any(a => a.AttributeType == typeof(KeyAttribute))) > 1))
            {
                // get the keys in the appropriate order
                var orderedKeys = entity.ClrType
                    .GetProperties()
                    .Where(p => p.CustomAttributes.Any(a => a.AttributeType == typeof(KeyAttribute)))
                    .OrderBy(p =>
                        p.CustomAttributes.Single(x => x.AttributeType == typeof(ColumnAttribute))?
                            .NamedArguments?.Single(y => y.MemberName == nameof(ColumnAttribute.Order))
                            .TypedValue.Value ?? 0)
                    .Select(x => x.Name)
                    .ToArray();

                // apply the keys to the model builder
                modelBuilder.Entity(entity.ClrType).HasKey(orderedKeys);
            }


            base.OnModelCreating(modelBuilder);

        }


        public new DbSet<TEntity> Set<TEntity>() where TEntity : EntityBase
        {
            return base.Set<TEntity>();
        }
    }

}
