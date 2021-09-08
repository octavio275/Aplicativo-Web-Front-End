using System;
using System.Linq;
using System.Linq.Expressions;


namespace Template.Domain
{
   public interface IRepository<E> where E : class
    {
        void Add(E entity);
        void SaveChanges();

        void DeleteById(int id);
        IQueryable<E> GetAll();
        E FindById(int id);
        void Update(E entity);
        public IQueryable<E> FindBy(Expression<Func<E, bool>> predicate, string[] includeProperties = null);
        void Agregar<T>(T entity);
    }
}
