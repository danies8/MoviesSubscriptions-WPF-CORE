using MoviesSubscriptions.UI.Event;
using MoviesSubscriptions.UI.Helpers;
using MoviesSubscriptions.UI.ViewModel.Base;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesSubscriptions.UI.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private TabViewModelBase _selectedTabViewModel;
        private readonly IEventAggregator EventAggregator;
        private string _loginUserName;

        public MainViewModel(IEnumerable<TabViewModelBase> tabViewModels, IEventAggregator eventAggregator)
        {
            TabViewModels = new ObservableCollection<TabViewModelBase>(tabViewModels);
            EventAggregator = eventAggregator;

            SelectedTabViewModel = TabViewModels.FirstOrDefault();

            EventAggregator.GetEvent<OpenNavigationViewEvent>().Subscribe(OpenNavigationView);
        }

        private void OpenNavigationView(OpenNavigationViewEventArgs args)
        {
            foreach (var tabViewModel in TabViewModels)
            {
                if (tabViewModel.Title ==  Constants.Subscriptions)
                {
                    if (args.ViewModelName == typeof(MembersViewModel).Name)
                    {
                        SelectedTabViewModel = tabViewModel;
                    }
                }
                else if (tabViewModel.Title == Constants.Movies)
                {
                    if (args.ViewModelName == typeof(MoviesViewModel).Name)
                    {
                        SelectedTabViewModel = tabViewModel;
                    }
                }

            }

         
        }


        public async Task LoadAsync()
        {
            var instance = LoginUserInfo.Instance;
            var viewSubscriptions = instance.User.ViewSubscriptions;
            var viewMovies = instance.User.ViewMovies;
           

            var viewModelsToAdd = new List<TabViewModelBase>();
            var viewModelsToRemove = new List<TabViewModelBase>();

            foreach (var tabViewModel in TabViewModels)
            {
                if (tabViewModel.Title == Constants.Welcome)
                {
                    viewModelsToAdd.Add(tabViewModel);
                }
                else if (tabViewModel.Title == Constants.UserManagements)
                {   if (instance.Login.IsAdmin) 
                    {
                        viewModelsToAdd.Add(tabViewModel);
                    }
                    else
                    {
                        viewModelsToRemove.Add(tabViewModel);
                    }
                }
                else if(tabViewModel.Title == Constants.Subscriptions)
                {
                    if (viewSubscriptions)
                    {
                        viewModelsToAdd.Add(tabViewModel);
                    }
                    else
                    {
                        viewModelsToRemove.Add(tabViewModel);
                    }
                }
                else if (tabViewModel.Title == Constants.Movies)
                {
                    if (viewMovies)
                    {
                        viewModelsToAdd.Add(tabViewModel);
                    }
                    else
                    {
                        viewModelsToRemove.Add(tabViewModel); 
                    }
                }
            }

            foreach (var viewModel in viewModelsToRemove)
            {
                TabViewModels.Remove(viewModel);
            }

            foreach (var viewModel in viewModelsToAdd)
            {
                await viewModel.LoadAsync();
            }

            LoginUserName = instance.User.UserName;
        }

        public ObservableCollection<TabViewModelBase> TabViewModels { get; private set; }

        public TabViewModelBase SelectedTabViewModel
        {
            get => _selectedTabViewModel;
            set
            {
                if (_selectedTabViewModel != value)
                {
                    _selectedTabViewModel = value;
                    OnPropertyChanged();

                    if (_selectedTabViewModel != null)
                    {
                        _selectedTabViewModel.LoadAsync();
                    }
                }
            }
        }
            
        public string  LoginUserName
        {
            get { return _loginUserName; }
            set
            { 
                _loginUserName = value;
                OnPropertyChanged();
            }
        }

    }
}
        



