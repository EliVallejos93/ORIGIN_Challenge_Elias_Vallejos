using Microsoft.EntityFrameworkCore;
using ORIGIN_Challenge_API.Models;

namespace ORIGIN_Challenge_API.Data.Repositories
{
    public class TarjetasRepository : ITarjetasRepository<Tarjeta>
    {
        private readonly ApplicationDbContext _context;

        public TarjetasRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Tarjeta GetById(int id)
        {
            return _context.Tarjetas.Find(id);
        }

        public Tarjeta GetByNumero(string numeroTarjeta)
        {
            return _context.Tarjetas.SingleOrDefault(t => t.NumeroTarjeta == numeroTarjeta);
        }

        public Tarjeta GetByNumeroConOperaciones(string numeroTarjeta)
        {
            return _context.Tarjetas
                .Include(t => t.Operaciones)
                .SingleOrDefault(t => t.NumeroTarjeta == numeroTarjeta);
        }

        public void Add(Tarjeta entity)
        {
            _context.Tarjetas.Add(entity);
        }

        public void InsertarDatosAleatorios()
        {
            _context.Database.ExecuteSqlRaw("EXEC sp_InsertarTarjetasAleatorias");
        }

        public void Update(Tarjeta entity)
        {
            _context.Tarjetas.Update(entity);
        }

        public void Delete(Tarjeta entity)
        {
            _context.Tarjetas.Remove(entity);
        }

        public IEnumerable<Tarjeta> GetAll()
        {
            return _context.Tarjetas.ToList();
        }
    }
}
