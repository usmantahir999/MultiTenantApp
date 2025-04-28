namespace Application.Wrappers
{
    public interface IResponseWrapper
    {
        List<string> Messages { get; set; } 
        bool IsSucessful { get; set; }
    }

    public interface IResponseWrapper<out T> : IResponseWrapper
    {
        T Data { get; }
    }
}
