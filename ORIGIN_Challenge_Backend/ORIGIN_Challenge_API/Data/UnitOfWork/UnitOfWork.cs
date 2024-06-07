using ORIGIN_Challenge_API.Data.Repositories;
using ORIGIN_Challenge_API.Models;

namespace ORIGIN_Challenge_API.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private ITarjetasRepository<Tarjeta> _tarjetasRepository;
        private IOperacionesRepository<Operaciones> _operacionesRepository;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public ITarjetasRepository<Tarjeta> Tarjetas
        {
            get { return _tarjetasRepository ?? (_tarjetasRepository = new TarjetasRepository(_context)); }
        }

        public IOperacionesRepository<Operaciones> Operaciones
        {
            get { return _operacionesRepository ?? (_operacionesRepository = new OperacionesRepository(_context)); }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }

}
