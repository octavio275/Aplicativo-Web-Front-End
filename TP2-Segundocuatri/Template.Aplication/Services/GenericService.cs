
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using Template.Domain;
using Template.Domain.Base;

namespace Template.Aplication.Services
{ 
    public class GenericService<E> : IService<E> where E : class
    {
        protected IRepository<E> generics;
        protected IMapper Mapper;

        public GenericService(IMapper mapper)
        {
            Mapper = mapper;
        }

        public GenericService(IRepository<E> Repository, IMapper mapper)
        {

            generics = Repository;
            Mapper = mapper;
        }

        public void Add(E entity)
        {
            this.generics.Add(entity);
        }

        public void DeleteById(int id)
        {
            this.generics.DeleteById(id);
        }

        public E FindById(int id)
        {
            return this.generics.FindById(id);
        }

        public IEnumerable<E> GetAll()
        {
            return generics.GetAll().ToList();
        }
    }
}
