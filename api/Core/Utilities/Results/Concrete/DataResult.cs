using api.Core.Utilities.Results.Abstract;

namespace api.Core.Utilities.Results.Concrete
{
    public class DataResult<T> : Result, IDataResult<T>
    {
        public T Data { get; }

        public DataResult(T data, bool success, string message = "")
            : base(success, message)
        {
            Data = data;
        }
    }
}