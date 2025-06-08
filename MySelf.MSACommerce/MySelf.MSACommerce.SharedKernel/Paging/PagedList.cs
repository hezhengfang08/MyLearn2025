using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.MSACommerce.SharedKernel.Paging
{
    public class PagedList<T> : List<T>
    {
        public PagedMetaData MetaData { get; set; }
        public PagedList(IEnumerable<T> items, long count, Pagination pagination)
        {
            MetaData = new PagedMetaData
            {
                TotalCount = count,
                PageSize = pagination.PageSize,
                CurrentPage = pagination.PageNumber,
                TotalPages = (int)Math.Ceiling(count / (double)pagination.PageSize)
            };
            AddRange(items);
        }
    }
}
