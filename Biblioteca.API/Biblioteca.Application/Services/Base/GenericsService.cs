using AutoMapper;
using Biblioteca.Domain.Commands;
using System.Collections.Generic;

namespace Biblioteca.Application.Services.Base
{
    public class GenericsService : IGenericsService
    {
        protected IGenericsRepository repository;
        protected IMapper Mapper;

        public GenericsService(IGenericsRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.Mapper = mapper;
        }

    }
}
