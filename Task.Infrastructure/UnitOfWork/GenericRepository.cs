using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Task.Infrastructure.DataContext;

namespace Task.Infrastructure.UnitOfWork
{
    public class GenericRepository<TEntity> where TEntity : class
    {
        internal TaskContext Context;
        internal DbSet<TEntity> DbSet;

        public GenericRepository(TaskContext context)
        {
            Context = context;
            DbSet = context.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = DbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            query = includeProperties.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries)
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            return orderBy != null ? orderBy(query).ToList() : query.ToList();
        }
        public virtual IEnumerable<TEntity> GetPerPage(int index=0, int count=0,
           Expression<Func<TEntity, bool>> filter = null,
           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
           string includeProperties = "")
        {
            IQueryable<TEntity> query = DbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }
            if(index != 0)
            { 
            query=query.Skip(index);
            }

            if (count != 0)
            {
                query = query.Take(count);
            }

            query = includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            return orderBy != null ? orderBy(query).ToList() : query.ToList();
        }



        public virtual TEntity GetById(object id)
        {
            return DbSet.Find(id);
        }
        //public virtual TEntity GetById(int id,
        //    string includeProperties = "")
        //{
        //  // IQueryable<TEntity> query = DbSet;
        //    IQueryable<TEntity> query = Context.Set<TEntity>();
        //    foreach (var includeProperty in includeProperties.Split
        //        (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
        //    {
        //        query = query.Include(includeProperty);
        //    }
            
        //    var result = query.ToList();
        //    return result.FirstOrDefault(p=>p.id=id);
        //    //var rt = ((DbSet<TEntity>)query).Find(id);
        //    // return ((DbSet<TEntity>)query).Find(id);
        //}
        public virtual void Insert(TEntity entity)
        {
            DbSet.Add(entity);
        }

        public virtual void Delete(object id)
        {
            var entityToDelete = DbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (Context.Entry(entityToDelete).State == EntityState.Detached)
            {
                DbSet.Attach(entityToDelete);
            }
            DbSet.Remove(entityToDelete);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            DbSet.Attach(entityToUpdate);
            Context.Entry(entityToUpdate).State = EntityState.Modified;
        }
    }
}
