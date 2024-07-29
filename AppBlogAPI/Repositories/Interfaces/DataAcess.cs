namespace AppBlogAPI.Repositories.Interfaces
{
    public interface DataAcess<T> where T : class
    {
        bool Add(T Entity);
        bool Update(T Entity);
        bool Delete(T Entity);
        T GetById(Guid Id);

    }
}
