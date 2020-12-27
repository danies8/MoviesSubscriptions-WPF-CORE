using Microsoft.EntityFrameworkCore;
using MoviesSubscriptions.DataAccess;
using MoviesSubscriptions.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesSubscriptions.UI.Data.Lookups
{
    public class LookupDataService : IMovieLookupDataService
    {
        private readonly Func<MoviesSubscriptionsDbContext> _contextCreator;

        public LookupDataService(Func<MoviesSubscriptionsDbContext> contextCreator)
        {
            _contextCreator = contextCreator;
        }

        public async Task<IEnumerable<LookupItem>> GetMovieLookupAsync()
        {
            using var ctx = _contextCreator();
            return await ctx.Movies.AsNoTracking()
              .Select(m =>
              new LookupItem
              {
                  Id = m.Id,
                  DisplayMember = m.Name
              })
              .ToListAsync();
        }

   
    }
}
