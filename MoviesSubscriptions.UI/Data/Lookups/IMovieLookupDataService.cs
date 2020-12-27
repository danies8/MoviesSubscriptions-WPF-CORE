using MoviesSubscriptions.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoviesSubscriptions.UI.Data.Lookups
{
    public interface IMovieLookupDataService
    {
        Task<IEnumerable<LookupItem>> GetMovieLookupAsync();
    }
}
