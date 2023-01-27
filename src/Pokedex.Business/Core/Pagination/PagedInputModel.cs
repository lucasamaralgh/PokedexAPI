using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokedex.Business.Core.Pagination
{
    public class PagedInputModel
    {
        public int PageIndex { get; private set; } = 1;

        public int PageSize { get; private set; } = 10;

        public int Skip => (PageIndex - 1) * PageSize;

    }
}
