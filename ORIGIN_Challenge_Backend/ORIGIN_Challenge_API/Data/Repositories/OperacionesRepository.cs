using ORIGIN_Challenge_API.Data;
using ORIGIN_Challenge_API.Models;

namespace ORIGIN_Challenge_API.Data.Repositories
{
    public class OperacionesRepository : IOperacionesRepository<Operaciones>
    {
        private readonly ApplicationDbContext _context;
        public OperacionesRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(Operaciones entity)
        {
            _context.Operaciones.Add(entity);
        }

        public void Delete(Operaciones entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Operaciones> GetAll()
        {
            throw new NotImplementedException();
        }

        public Operaciones GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Operaciones entity)
        {
            throw new NotImplementedException();
        }
    }
}
