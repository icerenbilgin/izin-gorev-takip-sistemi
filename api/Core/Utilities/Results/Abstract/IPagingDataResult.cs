using System.Collections.Generic;

namespace api.Core.Utilities.Results.Abstract
{
    public interface IPagingDataResult<T> : IDataResult<List<T>>
    {
        int Page { get; }
        int PageSize { get; }
        int TotalCount { get; }
        int TotalPages { get; }
        bool HasNext { get; }
        bool HasPrevious { get; }
    }
}