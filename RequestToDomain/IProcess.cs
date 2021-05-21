namespace RequestToDomain
{
    public interface IProcess<T>
    {
        (bool success, T model, int status) Run();
    }
}