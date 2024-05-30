using ORIGIN_Challenge_Backend.Models;

namespace ORIGIN_Challenge_Backend.Data.Repositories
{
    public interface ITarjetasRepository<T> where T : class
    {
        T GetById(int id);
        T GetByNumero(string numeroTarjeta);
        void Add(T entity);
        void InsertarDatosAleatorios();
        void Update(T entity);
        void Delete(T entity);
        IEnumerable<T> GetAll();
    }
}
