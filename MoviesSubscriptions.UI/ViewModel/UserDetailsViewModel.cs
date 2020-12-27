using Microsoft.EntityFrameworkCore;
using MoviesSubscriptions.Model;
using MoviesSubscriptions.UI.Data.Repositories;
using MoviesSubscriptions.UI.View.Services;
using MoviesSubscriptions.UI.ViewModel.Base;
using MoviesSubscriptions.UI.Wrapper;
using Prism.Commands;
using Prism.Events;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesSubscriptions.UI.ViewModel
{
    public class UserDetailsViewModel : DetailViewModelBase
    {
       private readonly IUserRepository _userRepository;
       private UserWrapper _user;
    
        public UserDetailsViewModel(IEventAggregator eventAggregator, IMessageDialogService messageDialogService,
            IUserRepository userRepository):base(eventAggregator, messageDialogService)
        {
             _userRepository = userRepository;
        }

        public async override Task LoadAsync(int userId)
        {
            var user = userId > 0
              ? await EditUser(userId)
              : CreateUser();

            Id = userId;

            InitializetUser(user);
        }

        public UserWrapper User
        {
            get { return _user; }
            private set
            {
                 _user = value;
                 OnPropertyChanged();
            }
        }
                
        private User CreateUser()
        {
            User user = new User();
            _userRepository.Add(user);
             return user;
        }

        private void InitializetUser(User user)
        {
            User = new UserWrapper(user);
            User.PropertyChanged += (s, e) =>
            {
                if (!HasChanges)
                {
                    HasChanges = _userRepository.HasChanges();
                }

                if (e.PropertyName == nameof(User.HasErrors))
                {
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                 }
                if (e.PropertyName == nameof(User.FirstName)
                  || e.PropertyName == nameof(User.LastName))
                {
                    SetTitle();
                }
            };
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
  
            if (User.Id == 0)
            {
                // Little trick to trigger the validation
                User.FirstName = "";
                User.LastName = "";
                User.UserName = "";
                User.CreatedDate = DateTime.Now;
                User.SessionTimeOut = 1;
            }
            SetTitle();
        }

        private void SetTitle()
        {
            Title = $"{User.FirstName} {User.LastName}";
        }

        private async Task<User> EditUser(int userId)
        {
           var user = await _userRepository.GetByIdAsync(userId);
           return user;
        }

        
        protected async override void OnDeleteExecute()
        {
            var result = await MessageDialogService.ShowOkCancelDialogAsync($"Do you really want to delete the user {User.FullName}?", "Question");
            if (result == MessageDialogResult.OK)
            {
                _userRepository.Remove(User.Model);
                 await _userRepository.SaveAsync();
                 RaiseDetailDeletedEvent(User.Id);
            }
        }

      
        protected override bool OnSaveCanExecute()
        {
            return User != null && !User.HasErrors && HasChanges;
        }

        protected async override void OnSaveExecute()
        {
            if (User.Id == 0)
            {
                var allUsers = await _userRepository.GetAllAsync();
                var results = allUsers.Where(u => u.UserName == User.UserName).FirstOrDefault();
                if (results != null)
                {
                    ErrorMessage = "User name already exsist";
                }
            }
            else
            {
                await _userRepository.SaveAsync();
                HasChanges = _userRepository.HasChanges();
                Id = User.Id;
                RaiseDetailSavedEvent(User.Id);
            }
        }

      
    }
}
