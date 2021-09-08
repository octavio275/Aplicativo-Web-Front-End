using Biblioteca.AccessData.BibliotecaDBContext;
using Biblioteca.Domain.Commands;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Biblioteca.AccessData.Commands
{
    public class GenericsRepository : IGenericsRepository
    {
        private readonly BibliotecaContext context;
        public GenericsRepository(BibliotecaContext dbContext)
        {
            context = dbContext;
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }

        public T Add<T>(T entity) where T : class
        {
            context.Add(entity);
            return entity;
        }

        public List<T> GetAll<T>() where T : class
        {
            List<T> query = context.Set<T>().ToList();
            return query;
        }

        public void Update<T>(T entity) where T : class
        {
            context.Set<T>().Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
        }
    }
}
