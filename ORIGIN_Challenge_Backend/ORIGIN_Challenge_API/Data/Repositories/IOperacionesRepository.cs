namespace ORIGIN_Challenge_API.Data.Repositories
{
    public interface IOperacionesRepository<O> where O : class
    {
        O GetById(int id);
        void Add(O entity);
        void Update(O entity);
        void Delete(O entity);
        IEnumerable<O> GetAll();
    }
}
