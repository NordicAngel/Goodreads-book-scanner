namespace OpenLibraryWebService.Managers
{
    public interface IListManager<T>
    {
        List<T> GetAll();
        T Create(T book);
        T GetByID(int id);
    }
}
