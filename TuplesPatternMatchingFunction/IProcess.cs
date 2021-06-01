namespace TuplesPatternMatchingFunction
{
    public interface IProcess<T>
    {
        (bool success, T model, int status) Run();
    }
}