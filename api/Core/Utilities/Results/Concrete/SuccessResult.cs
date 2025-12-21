namespace api.Core.Utilities.Results.Concrete
{
    public class SuccessResult : Result
    {
        public SuccessResult(string message = "")
            : base(true, message)
        {
        }
    }
}