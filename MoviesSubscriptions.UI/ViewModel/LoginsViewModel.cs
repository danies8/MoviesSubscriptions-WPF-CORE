using MoviesSubscriptions.Model;
using MoviesSubscriptions.UI.Data.Repositories;
using MoviesSubscriptions.UI.Event;
using MoviesSubscriptions.UI.Helpers;
using MoviesSubscriptions.UI.ViewModel.Base;
using MoviesSubscriptions.UI.Wrapper;
using Prism.Commands;
using Prism.Events;
using System;
using System.Threading.Tasks;
using System.Windows.Input;


namespace MoviesSubscriptions.UI.ViewModel
{
    public class LoginsViewModel : ViewModelBase
    {
        private UserLoginWrapper _userLogin;
        private readonly IUserLoginRepository _userLoginRepository;
        private readonly IEventAggregator EventAggregator;

        public LoginsViewModel(IUserLoginRepository userLoginRepository, IEventAggregator eventAggregator)
        {
            _userLoginRepository = userLoginRepository;
            EventAggregator = eventAggregator;
            UserLogin = new UserLoginWrapper(new UserLogin());
            LoginCommand = new DelegateCommand(OnLoginExecute);
          }
        public ICommand LoginCommand { get; }

         public UserLoginWrapper UserLogin
        {
            get { return _userLogin; }
            private set
            {
                _userLogin = value;
                OnPropertyChanged();
            }
        }
   
        public async void OnLoginExecute()
        {
            var results = await _userLoginRepository.CheckUserCredentials(UserLogin.UserName, UserLogin.Password);
            if (results != null)
            {
                LoginUserInfo.Instance.Login = results.Login;
                LoginUserInfo.Instance.User = results.User;
                LoginUserInfo.CreateSubscriptions = results.User.CreateSubscriptions;
                LoginUserInfo.UpdateSubscriptions = results.User.UpdateSubscriptions;
                LoginUserInfo.DeleteSubscriptions = results.User.DeleteSubscriptions;
                LoginUserInfo.CreateMovies = results.User.CreateMovies;
                LoginUserInfo.UpdateMovies = results.User.UpdateMovies;
                LoginUserInfo.DeleteMovies = results.User.DeleteMovies;

                EventAggregator.GetEvent<AfterLoginExecuteEvent>()
                    .Publish(new AfterLoginExecuteEventArgs
                    {
                        IsUserExists = results.IsUserExists
                     });
         
            }
            else
            {
                EventAggregator.GetEvent<AfterLoginExecuteEvent>()
                    .Publish(new AfterLoginExecuteEventArgs
                    {
                        IsUserExists = false
                    }); ;
            }
        }

    }
}
