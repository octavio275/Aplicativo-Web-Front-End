
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using Template.AcessData;
using Template.Domain;

namespace Template.AccessData.Commands
{
    public class GenericRepository<E> : IRepository<E> where E : class
    {

        public Context context;

        public GenericRepository(Context dbContext)
        {
            this.context = dbContext;
        }

        public void Add(E entity)
        {
            context.Add(entity);
            context.SaveChanges();

        }
        public void SaveChanges()
        {
            context.SaveChanges();
        }
        public void DeleteById(int id)
        {
            var entity = FindById(id);
            context.Remove(entity);
            context.SaveChanges();
        }

        public E FindById(int id)
        {
            var entity = context.Set<E>().Find(id);

            return entity;
        }

        public System.Linq.IQueryable<E> GetAll()
        {
            IQueryable<E> query = context.Set<E>();
            return query;
        }

        public void Update(E entity)
        {
            context.Set<E>().Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
        }

        public IQueryable<E> FindBy(Expression<Func<E, bool>> predicate, string[] includeProperties = null)
        {
            IQueryable<E> query = context.Set<E>();

            if (includeProperties != null)
                foreach (string includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }

            return query.Where(predicate);
        }

        public void Agregar<T>(T entity)
        {
            throw new NotImplementedException();
        }


    }
}
