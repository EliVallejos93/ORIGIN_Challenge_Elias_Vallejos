using ORIGIN_Challenge_API.Data.Repositories;
using ORIGIN_Challenge_API.Models;

namespace ORIGIN_Challenge_API.Data.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        ITarjetasRepository<Tarjeta> Tarjetas { get; }
        IOperacionesRepository<Operaciones> Operaciones { get; }
        void Save();
    }

}
