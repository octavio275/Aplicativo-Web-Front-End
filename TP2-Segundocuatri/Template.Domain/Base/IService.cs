using System.Collections.Generic;

namespace Template.Domain.Base
{
    public interface IService<E> where E : class
    {
        IEnumerable<E> GetAll();
        void Add(E entity);
        void DeleteById(int id);
        E FindById(int id);
    }
}
