
using Microsoft.EntityFrameworkCore;
using MoviesSubscriptions.DataAccess;
using MoviesSubscriptions.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoviesSubscriptions.UI.Data.Repositories
{
    public class MovieRepository : GenericRepository<Movie, MoviesSubscriptionsDbContext>,
                                     IMovieRepository
    {
        public MovieRepository(MoviesSubscriptionsDbContext context)
          : base(context) { }


        public override async Task<IEnumerable<Movie>> GetAllAsync()
        {
            return await Context.Set<Movie>()
                .Include(m => m.Subscriptions)
                .AsNoTracking().ToListAsync();

        }
     

    }
}
