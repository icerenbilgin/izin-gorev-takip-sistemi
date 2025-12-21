namespace api.Core.Utilities.Results.Concrete
{
    public class ErrorDataResult<T> : DataResult<T>
    {
        public ErrorDataResult(string message = "")
            : base(default!, false, message)
        {
        }

        public ErrorDataResult(T data, string message = "")
            : base(data, false, message)
        {
        }
    }
}