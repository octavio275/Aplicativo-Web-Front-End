using System.Collections.Generic;

namespace Biblioteca.Domain.Commands
{
    public interface IGenericsRepository
    {
        void SaveChanges();
        T Add<T>(T entity) where T : class;
        List<T> GetAll<T>() where T : class;
        void Update<T>(T entity) where T : class;
    }
}
