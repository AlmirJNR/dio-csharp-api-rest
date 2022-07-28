namespace CsharpApiRest.Interfaces;

interface IRepository<T>
{
    public void Create(T genericObject);
    public List<T> ReadAll();
    public bool ReadById(int id, out T? genericObject);
    public bool Update(int it, T genericObject);
    public void DeleteAll();
    public bool DeleteById(int id);
}
