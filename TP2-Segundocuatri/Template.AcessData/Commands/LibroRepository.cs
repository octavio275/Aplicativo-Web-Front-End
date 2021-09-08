


using Template.AcessData;
using Template.Domain.Commands;
using Template.Domain.Entities;

namespace Template.AccessData.Commands
{
    public class LibroRepository : GenericRepository<Libros>, ILibroRepository
    {
        public LibroRepository(Context context) : base(context)
        {

        }
    }
}
