using api.Core.Utilities.Results.Abstract;

namespace api.Core.Utilities.Results.Concrete
{
    public class Result : Abstract.IResult
    {
        public bool Success { get; }
        public string Message { get; }

        public Result(bool success, string message = "")
        {
            Success = success;
            Message = message;
        }
    }
}