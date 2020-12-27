using Prism.Events;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using MoviesSubscriptions.UI.ViewModel.Base;
using MoviesSubscriptions.UI.Data.Repositories;
using MoviesSubscriptions.UI.Event;
using MoviesSubscriptions.UI.Wrapper;
using MoviesSubscriptions.UI.ViewModel;
using System.Windows.Input;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MoviesSubscriptions.UI.ViewModell
{
    public class UserNavigationViewModel : ViewModelBase, INavigationViewModel
    {
        private readonly IUserRepository _userRepository;
        private readonly IEventAggregator _eventAggregator;
        private UserWrapper _selectedUser;


        public UserNavigationViewModel(IUserRepository userRepository,
                IEventAggregator eventAggregator)
        {
           _userRepository = userRepository;
          _eventAggregator = eventAggregator;

          _eventAggregator.GetEvent<AfterDetailSavedEvent>().Subscribe(AfterDetailSaved);
          _eventAggregator.GetEvent<AfterDetailDeletedEvent>().Subscribe(AfterDetailDeleted);
         _eventAggregator.GetEvent<OpenAndFilterNavigationViewEvent>().Subscribe(OpenAndFilterNavigationView);

            AllUsers = new ObservableCollection<UserWrapper>();

           SelectedUserCommand = new DelegateCommand(OnSelectetedUserExecute);

       }

        public UserWrapper SelectedUser
    {
        get { return _selectedUser; }
        set
        {
            _selectedUser = value;
            OnPropertyChanged();
        }
    }

        public async Task LoadAsync()
        {
            AllUsers.Clear();
            var users = await _userRepository.GetAllAsync();
            foreach (var user in users)
            {   
                if(user.UserName == "admin@yahoo.com")
                {
                    continue;
                }
                var modelWrapper = new UserWrapper(user);
                AllUsers.Add(modelWrapper);
            }
        }

         public ObservableCollection<UserWrapper> AllUsers { get; }

        public ICommand SelectedUserCommand { get; }


        private async void AfterDetailDeleted(AfterDetailDeletedEventArgs args)
        {
            await LoadAsync();
           
        }

        private async void AfterDetailSaved(AfterDetailSavedEventArgs args)
        {
            await LoadAsync();
        }

        private void OnSelectetedUserExecute()
        {
            if (SelectedUser != null)
            {
                _eventAggregator.GetEvent<OpenDetailViewEvent>().Publish(new
                    OpenDetailViewEventArgs
                {
                    Id = SelectedUser.Id,
                    ViewModelName = typeof(UserDetailsViewModel).Name
                });
            }
        }

        private async void OpenAndFilterNavigationView(OpenAndFilterNavigationViewEventArgs args)
        {
            if (String.IsNullOrEmpty(args.SelectedItem))
            {
                await LoadAsync();
            }
            else
            {
                var filteredUsers = AllUsers.Where(u => u.FullName.ToLower().StartsWith(args.SelectedItem.ToLower()));
                List<UserWrapper> tempList = new List<UserWrapper>();
                foreach (var filteredUser in filteredUsers)
                {
                    tempList.Add(filteredUser);
                }
                AllUsers.Clear();
                foreach (var item in tempList)
                {
                    AllUsers.Add(item);
                }

            }
        }

    }
}
