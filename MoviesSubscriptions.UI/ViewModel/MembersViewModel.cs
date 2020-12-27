using Autofac.Features.Indexed;
using MoviesSubscriptions.UI.Event;
using MoviesSubscriptions.UI.Helpers;
using MoviesSubscriptions.UI.View.Services;
using MoviesSubscriptions.UI.ViewModel.Base;
using MoviesSubscriptions.UI.ViewModell;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MoviesSubscriptions.UI.ViewModel
{
    public class MembersViewModel : TabViewModelBase
    {
        private readonly IIndex<string, IDetailViewModel> _detailViewModelCreator;
        private readonly IEventAggregator _eventAggregator;
        private readonly IMessageDialogService _messageDialogService;
        private IDetailViewModel _selectedDetailViewModel;

        public MembersViewModel(IIndex<string, IDetailViewModel> detailViewModelCreator,
           IEventAggregator eventAggregator, IMessageDialogService messageDialogService,
           IIndex<string, INavigationViewModel> navigationViewModels)
        {
            _detailViewModelCreator = detailViewModelCreator;
            _eventAggregator = eventAggregator;
            _messageDialogService = messageDialogService;

            CreateNewDetailCommand = new DelegateCommand<Type>(OnCreateNewDetailExecute);
            SearchMemberCommand = new DelegateCommand<string>(OnSearchMemberExecute);

            _eventAggregator.GetEvent<OpenDetailViewEvent>().Subscribe(OpenDetailView);
            _eventAggregator.GetEvent<AfterDetailDeletedEvent>().Subscribe(AfterDetailDeleted);
            _eventAggregator.GetEvent<AfterDetailClosedEvent>().Subscribe(AfterDetailClosed);
        
            DetailViewModels = new ObservableCollection<IDetailViewModel>();

            NavigationViewModels = navigationViewModels;
        }

    
        public override async Task LoadAsync()
        {
            NavigationViewModel = NavigationViewModels[typeof(MemberNavigationViewModel).Name];
            await NavigationViewModel.LoadAsync();

            CloseOpenDetailsViews();
        }

        public override string Title => Constants.Subscriptions;

        public IIndex<string, INavigationViewModel> NavigationViewModels { get; }
        public INavigationViewModel NavigationViewModel { get; private set; }

        public IDetailViewModel SelectedDetailViewModel
        {
            get { return _selectedDetailViewModel; }
            set
            {
                _selectedDetailViewModel = value;
                OnPropertyChanged();
            }
        }

        public ICommand CreateNewDetailCommand { get; }

        public ICommand SearchMemberCommand { get; }
               

        public ObservableCollection<IDetailViewModel> DetailViewModels { get; private set; }
        private void AfterDetailDeleted(AfterDetailDeletedEventArgs args)
        {
            RemoveDetailViewModel(args.Id, args.ViewModelName);
        }

        private void AfterDetailClosed(AfterDetailClosedEventArgs args)
        {
            RemoveDetailViewModel(args.Id, args.ViewModelName);
        }

        private void RemoveDetailViewModel(int id, string viewModelName)
        {
            var detailViewModel = DetailViewModels
                   .SingleOrDefault(vm => vm.Id == id
                   && vm.GetType().Name == viewModelName);
            if (detailViewModel != null)
            {
                DetailViewModels.Remove(detailViewModel);
            }
        }

         private int nextNewItemId = 0;
        private void OnCreateNewDetailExecute(Type viewModelType)
        {
            OnOpenDetailView(
              new OpenDetailViewEventArgs
              {
                  Id = nextNewItemId--,
                  ViewModelName = viewModelType.Name
              }); 
        }


        private async void OnOpenDetailView(OpenDetailViewEventArgs args)
        {
            var detailViewModel = DetailViewModels
              .SingleOrDefault(vm => vm.Id == args.Id
              && vm.GetType().Name == args.ViewModelName);

             if (detailViewModel == null)
            {
                detailViewModel = _detailViewModelCreator[args.ViewModelName];
                try
                {
                    await detailViewModel.LoadAsync(args.Id);
                }
                catch
                {
                    await _messageDialogService.ShowInfoDialogAsync("Could not load the entity, " +
                        "maybe it was deleted in the meantime by another user. " +
                        "The navigation is refreshed for you.");
                    await NavigationViewModel.LoadAsync();
                    await LoadAsync();
                    return;
                }

                DetailViewModels.Add(detailViewModel);
            }
            SelectedDetailViewModel = detailViewModel;
        }


        private void OpenDetailView(OpenDetailViewEventArgs args)
        {
            OnOpenDetailView(args);
        }

        private void CloseOpenDetailsViews()
        {
            DetailViewModels.Clear();
            SelectedDetailViewModel = null;
        }

        private void OnSearchMemberExecute(string memberName)
        {
            _eventAggregator.GetEvent<OpenAndFilterNavigationViewEvent>()
                     .Publish(new
                       OpenAndFilterNavigationViewEventArgs
                     {
                         ViewModelName = typeof(MembersViewModel).Name,
                         SelectedItem = memberName
                     });
        }
    }
}

