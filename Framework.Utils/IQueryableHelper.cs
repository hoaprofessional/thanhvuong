using PagedList.Core;
using System.Linq;

namespace Framework.Utils
{
    public static class IQueryableHelper
    {
        public static IPagedList<T> Paged<T>(this IQueryable<T> query, int pageIndex, int pageSize = 10)
        {
            if(pageIndex<1)
            {
                pageIndex = 1;
            }
            var result = query.ToPagedList(pageIndex, pageSize);
            return result;
        }
    }
}
