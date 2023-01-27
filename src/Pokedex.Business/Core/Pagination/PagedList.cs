using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokedex.Business.Core.Pagination
{
    public class PagedList<TData>
    {

        public int PageIndex { get; private set; }

        public int PageSize { get; private set; }

        public int TotalCount { get; private set;}

        public List<TData> Data { get; private set; }

        public PagedList(int pageIndex, int pageSize, int totalCount, List<TData> data)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalCount = totalCount;
            Data = data;
        }


    }
}
