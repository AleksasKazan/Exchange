namespace Persistence
{
    public interface IFileClient
    {
        IEnumerable<T> ReadFile<T>();
    }
}