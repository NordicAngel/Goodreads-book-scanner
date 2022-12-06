namespace OpenLibraryWebService.Managers
{
    public interface IBookManager<T>
    {
        List<T> GetAll();
        T Create(T book);
        List<T> GetByID(int id);
    }
}
