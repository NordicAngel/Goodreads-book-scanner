namespace OpenLibraryWebService.Managers
{
    public interface IBookManager<T>
    {
        T Create(T book);
        T GetByID(int id);
    }
}
