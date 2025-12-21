using System;
using System.Collections.Generic;
using api.Core.Utilities.Results.Abstract;

namespace api.Core.Utilities.Results.Concrete
{
    public class PagingDataResult<T> : DataResult<List<T>>, IPagingDataResult<T>
    {
        public int Page { get; }
        public int PageSize { get; }
        public int TotalCount { get; }

        public int TotalPages =>
            PageSize <= 0 ? 0 : (int)Math.Ceiling((double)TotalCount / PageSize);

        public bool HasNext => Page < TotalPages;
        public bool HasPrevious => Page > 1;

        public PagingDataResult(
            List<T> data,
            int page,
            int pageSize,
            int totalCount,
            string message = "")
            : base(data, true, message)
        {
            Page = page;
            PageSize = pageSize;
            TotalCount = totalCount;
        }
    }
}