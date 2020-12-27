
using Microsoft.EntityFrameworkCore;
using MoviesSubscriptions.DataAccess;
using MoviesSubscriptions.Model;
using System.Threading.Tasks;

namespace MoviesSubscriptions.UI.Data.Repositories
{
    public class UserLoginRepository : GenericRepository<UserLogin, MoviesSubscriptionsDbContext>,
                                     IUserLoginRepository
    {
        public UserLoginRepository(MoviesSubscriptionsDbContext context)
          : base(context) { }

        public async Task<UserCredentialsResults> CheckUserCredentials(string userName, string password)
        {
            var userLogin = await Context.Set<UserLogin>().SingleOrDefaultAsync(userLogin => userLogin.UserName == userName && userLogin.Password == password);
            if (userLogin != null)
            {
                UserCredentialsResults userCredentialsResults = new UserCredentialsResults();
                var user = await Context.Set<User>().SingleOrDefaultAsync(usr => usr.UserName == userName);
            
                if (userLogin !=null && user != null)
                {
                    userCredentialsResults.IsUserExists = true;
                    userCredentialsResults.Login = userLogin;
                    userCredentialsResults.User = user;
                }
                return userCredentialsResults;

            }
            else
            {
                return null;
            }

        }

    

    }
}
