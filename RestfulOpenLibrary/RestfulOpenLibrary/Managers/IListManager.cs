namespace RestfulOpenLibrary.Managers
{
    public interface IListManager<T>
    {
        List<T> GetAll();

        T AddList(string name);

        T DeleteList(int id);

        T GetById(int id);


    }
}
