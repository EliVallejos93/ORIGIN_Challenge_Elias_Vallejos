using ORIGIN_Challenge_API.Models;

namespace ORIGIN_Challenge_API.Data.Repositories
{
    public interface ITarjetasRepository<T> where T : class
    {
        T GetById(int id);
        T GetByNumero(string numeroTarjeta);
        T GetByNumeroConOperaciones(string numeroTarjeta);
        void Add(T entity);
        void InsertarDatosAleatorios();
        void Update(T entity);
        void Delete(T entity);
        IEnumerable<T> GetAll();
    }
}
