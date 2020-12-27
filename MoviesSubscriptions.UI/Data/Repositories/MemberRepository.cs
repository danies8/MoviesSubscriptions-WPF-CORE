
using Microsoft.EntityFrameworkCore;
using MoviesSubscriptions.DataAccess;
using MoviesSubscriptions.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoviesSubscriptions.UI.Data.Repositories
{
    public class MemberRepository : GenericRepository<Member, MoviesSubscriptionsDbContext>,
                                     IMemberRepository
    {
        public MemberRepository(MoviesSubscriptionsDbContext context)
          : base(context) { }


        public override async Task<IEnumerable<Member>> GetAllAsync()
        {
            return await Context.Set<Member>()
                .Include(m => m.Subscriptions)
                .AsNoTracking().ToListAsync();

        }

    }
}
