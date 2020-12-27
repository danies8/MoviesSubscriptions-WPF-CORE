
using Microsoft.EntityFrameworkCore;
using MoviesSubscriptions.DataAccess;
using MoviesSubscriptions.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoviesSubscriptions.UI.Data.Repositories
{
    public class SubscriptionRepository : GenericRepository<Subscription, MoviesSubscriptionsDbContext>,
                                     ISubscriptionRepository
    {
        public SubscriptionRepository(MoviesSubscriptionsDbContext context)
          : base(context) { }


        public override async Task<IEnumerable<Subscription>> GetAllAsync()
        {
            return await Context.Set<Subscription>().AsNoTracking()
                .Include(s => s.Member)
                .Include(s => s.Movie)
                .ToListAsync();

        }

    }
}
