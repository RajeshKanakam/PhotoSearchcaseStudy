using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhotoSearch.BLL.Interfaces
{
    public interface ISearchService<T>
    {
        string SearchString { get; set; }

        int MaxPerPage { get; set; }

        Task<List<T>> ExecuteSearch();
    }
}
