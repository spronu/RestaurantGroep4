public interface ILogic<T>
{
    void UpdateList(T acc);
    List<T> GetAll();
    void ReloadData();
}
