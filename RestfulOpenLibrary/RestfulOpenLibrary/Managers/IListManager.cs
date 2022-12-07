namespace RestfulOpenLibrary.Managers
{
    public interface IListManager<T>
    {
        List<T> GetAll();

        T Addlist(T name);

        T Delete(int id);

        T GetBýId(int id);


    }
}
