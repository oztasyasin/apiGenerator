using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Core.DataAccess.EntityFramework
{
    public class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
        where TEntity : class, IEntity, new()
        where TContext : DbContext, new()
    {
        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            using (var context = new TContext())
            {
                return context.Set<TEntity>().SingleOrDefault(filter);
            }
        }
        public TEntity Get(string[] includes, Expression<Func<TEntity, bool>> filter, string table)
        {
            using (var context = new TContext())
            {
                var m = context.Set<TEntity>().Include(includes[0]);
                if (filter == null)
                {

                    for (int i = 1; i < includes.Length; i++)
                    {
                        m = m.Include(includes[i]);
                    }
                    return m.SingleOrDefault();
                }
                else
                {
                    for (int i = 1; i < includes.Length; i++)
                    {
                        m = m.Include(includes[i]);
                    }
                    return m.Where(filter).SingleOrDefault();
                }
            }
        }
        public List<TEntity> GetList(Expression<Func<TEntity, bool>> filter = null)
        {
            using (var context = new TContext())
            {
                return filter == null
                    ? context.Set<TEntity>().ToList()
                    : context.Set<TEntity>().Where(filter).ToList();
            }
        }
        public List<TEntity> GetList(string[] includes, Expression<Func<TEntity, bool>> filter = null)
        {

            using (var context = new TContext())
            {
                var m = context.Set<TEntity>().Include(includes[0]);
                if (filter == null)
                {

                    for (int i = 1; i < includes.Length; i++)
                    {
                        m = m.Include(includes[i]);
                    }
                    return m.ToList();
                }
                else
                {
                    for (int i = 1; i < includes.Length; i++)
                    {
                        m = m.Include(includes[i]);
                    }
                    return m.Where(filter).ToList();
                }
            }
        }
        public List<TEntity> GetList(string[] includes, string[] thenIncludes, Expression<Func<TEntity, bool>> filter = null)
        {
            using (var context = new TContext())
            {
                var m = context.Set<TEntity>().Include(includes[0]);
                if (filter == null)
                {
                    for (int i = 1; i < includes.Length; i++)
                    {
                        m = m.Include(includes[i]);
                    }
                    return m.ToList();
                }
                else
                {
                    for (int i = 1; i < includes.Length; i++)
                    {
                        m = m.Include(includes[i]);
                    }
                    return m.Where(filter).ToList();
                }
            }
        }
        public void Add(TEntity entity)
        {
            using (var context = new TContext())
            {
                var addedEntity = context.Entry(entity);
                addedEntity.State = EntityState.Added;
                context.SaveChanges();
            }
        }

        public void Update(TEntity entity)
        {
            using (var context = new TContext())
            {
                var updatedEntity = context.Entry(entity);
                updatedEntity.State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void Delete(TEntity entity)
        {
            using (var context = new TContext())
            {
                var deleteEntity = context.Entry(entity);
                deleteEntity.State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public TEntity Get(string[] includes, Expression<Func<TEntity, bool>> filter = null)
        {
            using (var context = new TContext())
            {
                var m = context.Set<TEntity>().Include(includes[0]);
                if (filter == null)
                {

                    for (int i = 1; i < includes.Length; i++)
                    {
                        m = m.Include(includes[i]);
                    }
                    return m.SingleOrDefault();
                }
                else
                {
                    for (int i = 1; i < includes.Length; i++)
                    {
                        m = m.Include(includes[i]);
                    }
                    return m.Where(filter).SingleOrDefault();
                }
            }
        }
    }
}
