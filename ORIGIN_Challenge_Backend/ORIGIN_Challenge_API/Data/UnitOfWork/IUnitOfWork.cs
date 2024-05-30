using ORIGIN_Challenge_Backend.Data.Repositories;
using ORIGIN_Challenge_Backend.Models;

namespace ORIGIN_Challenge_Backend.Data.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        ITarjetasRepository<Tarjeta> Tarjetas { get; }
        void Save();
    }

}
