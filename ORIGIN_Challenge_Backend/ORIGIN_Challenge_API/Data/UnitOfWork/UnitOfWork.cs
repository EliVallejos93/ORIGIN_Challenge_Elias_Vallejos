using ORIGIN_Challenge_Backend.Data.Repositories;
using ORIGIN_Challenge_Backend.Models;

namespace ORIGIN_Challenge_Backend.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private ITarjetasRepository<Tarjeta> _tarjetasRepository;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public ITarjetasRepository<Tarjeta> Tarjetas
        {
            get
            {
                return _tarjetasRepository ?? (_tarjetasRepository = new TarjetasRepository(_context));
            }
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
