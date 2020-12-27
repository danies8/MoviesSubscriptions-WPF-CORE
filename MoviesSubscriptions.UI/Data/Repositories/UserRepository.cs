using Microsoft.EntityFrameworkCore;
using MoviesSubscriptions.DataAccess;
using MoviesSubscriptions.Model;
using MoviesSubscriptions.UI.Wrapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoviesSubscriptions.UI.Data.Repositories
{
    public class UserRepository : GenericRepository<User, MoviesSubscriptionsDbContext>,
                                     IUserRepository
    {
        public UserRepository(MoviesSubscriptionsDbContext context)
          : base(context) { }

        public override async Task<IEnumerable<User>> GetAllAsync()
        {
            return await Context.Set<User>().AsNoTracking().ToListAsync();

        }



    }
}
