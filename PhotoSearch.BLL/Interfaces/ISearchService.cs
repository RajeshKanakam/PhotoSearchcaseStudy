using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhotoSearch.BLL.Interfaces
{
    /// <summary>
    /// Interface to implement contract for Search
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISearchService<T>
    {
        string SearchString { get; set; }

        int MaxPerPage { get; set; }

        Task<List<T>> ExecuteSearch();
    }
}
