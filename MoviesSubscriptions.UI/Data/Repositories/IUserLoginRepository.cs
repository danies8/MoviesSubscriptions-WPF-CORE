using MoviesSubscriptions.Model;
using System.Threading.Tasks;

namespace MoviesSubscriptions.UI.Data.Repositories
{
  public interface IUserLoginRepository : IGenericRepository<UserLogin>
  {
        Task<UserCredentialsResults> CheckUserCredentials(string userName, string password);
    }

    public class UserCredentialsResults
    {
        public bool IsUserExists { get; internal set; }
        public UserLogin Login { get; internal set; }
        public User User { get; internal set; }
      
    }
}
  
