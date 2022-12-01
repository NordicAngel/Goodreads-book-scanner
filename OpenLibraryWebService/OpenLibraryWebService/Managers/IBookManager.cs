namespace OpenLibraryWebService.Managers
{
    public interface IBookManager<T>
    {
        List<T> GetAll();
        T Create(T book);
        T GetByID(int id);
    }
}
