using MoviesSubscriptions.UI.Data.Repositories;
using MoviesSubscriptions.UI.Event;
using MoviesSubscriptions.UI.ViewModel.Base;
using MoviesSubscriptions.UI.ViewModell;
using MoviesSubscriptions.UI.Wrapper;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MoviesSubscriptions.UI.ViewModel
{
    public class MovieNavigationViewModel : ViewModelBase, INavigationViewModel
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IEventAggregator _eventAggregator;
        private readonly IMemberRepository _memberRepository;
        private readonly ISubscriptionRepository _subscriptionRepository;
        private MovieWrapper _selectedMovie;
    
        public MovieNavigationViewModel(IMovieRepository movieRepository,IEventAggregator eventAggregator,
            IMemberRepository memberRepository, ISubscriptionRepository subscriptionRepository)
        {
            _movieRepository = movieRepository;
            _eventAggregator = eventAggregator;
            _memberRepository = memberRepository;
            _subscriptionRepository = subscriptionRepository;

            _eventAggregator.GetEvent<AfterDetailSavedEvent>().Subscribe(AfterDetailSaved);
            _eventAggregator.GetEvent<AfterDetailDeletedEvent>().Subscribe(AfterDetailDeleted);
            _eventAggregator.GetEvent<OpenNavigationViewEvent>().Subscribe(OpenNavigationView);
            _eventAggregator.GetEvent<OpenAndFilterNavigationViewEvent>().Subscribe(OpenAndFilterNavigationView);
       
            AllMovies = new ObservableCollection<MovieWrapper>();
            SubscriptionsViewModel = new ObservableCollection<SubscriptionViewModel>();
            
            SelectedMovieCommand = new DelegateCommand(OnSelectetedMovieExecute);
             MembersWatchedCommand = new DelegateCommand(OnMembersWatchedExecute);

        }
        public MovieWrapper SelectedMovie
        {
            get { return _selectedMovie; }
            set
            {
                _selectedMovie = value;
                OnPropertyChanged();
            }
        }

   
        public async Task LoadAsync()
        {
            AllMovies.Clear();
      
            var movies = await _movieRepository.GetAllAsync();
            foreach (var movie in movies)
            {
               var modelWrapper = new MovieWrapper(movie);
                AllMovies.Add(modelWrapper);

            }
  
        }

        public ObservableCollection<MovieWrapper> AllMovies { get; private set; }
        public ObservableCollection<SubscriptionWrapper> Subscriptions { get; }
        public ObservableCollection<SubscriptionViewModel> SubscriptionsViewModel { get; }

        public ICommand SelectedMovieCommand { get; }
        public ICommand SelectedMemberDetailsCommand { get; }
        public ICommand MembersWatchedCommand { get; }

        private async void AfterDetailDeleted(AfterDetailDeletedEventArgs args)
        {
            await LoadAsync();

        }

        private async void AfterDetailSaved(AfterDetailSavedEventArgs args)
        {
            await LoadAsync();
        }

        private void OnSelectetedMovieExecute()
        {
            if (SelectedMovie != null)
            {
                _eventAggregator.GetEvent<OpenDetailViewEvent>().Publish(new
                    OpenDetailViewEventArgs
                {
                    Id = SelectedMovie.Id,
                    ViewModelName = typeof(MovieDetailsViewModel).Name
                });
       
            }
        }

        private void OnSelectedMemberDetailsExecute(string subscriptionId)
        {
             _eventAggregator.GetEvent<OpenNavigationViewEvent>()
                      .Publish(new
                        OpenNavigationViewEventArgs
                      {
                          Id = int.Parse(subscriptionId),
                          ViewModelName = typeof(MembersViewModel).Name,
                        }); 
        }
      
        private async void OpenNavigationView(OpenNavigationViewEventArgs args)
        {
            await LoadAsync();

            var subscription = await _subscriptionRepository.GetByIdAsync(args.Id);
            var movie = await _movieRepository.GetByIdAsync(subscription.MovieId);
            var movieWrpper = AllMovies.Where(m => m.Id == movie.Id).FirstOrDefault();
            if (movieWrpper != null)
            {
                SelectedMovie = movieWrpper;
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
                var filteredMovies = AllMovies.Where(m => m.Name.ToLower().StartsWith(args.SelectedItem.ToLower()));
                List<MovieWrapper> tempList = new List<MovieWrapper>();
                foreach (var filteredMovie in filteredMovies)
                {
                    tempList.Add(filteredMovie);
                }
                AllMovies.Clear();
                foreach (var item in tempList)
                {
                    AllMovies.Add(item);
                }

            }
        }

        private async void OnMembersWatchedExecute()
        {
            if (SelectedMovie != null)
            {
                SubscriptionsViewModel.Clear();
                SubscriptionViewModel subscriptionViewModel;
                foreach (var subscription in SelectedMovie.Subscriptions)
                {
                    subscriptionViewModel = new SubscriptionViewModel();
                    var member = await _memberRepository.GetByIdAsync(subscription.MemberId);
                    subscriptionViewModel.Id = subscription.Id;
                    subscriptionViewModel.Movie = SelectedMovie.Model;
                    subscriptionViewModel.Member = member;
                    subscriptionViewModel.WatchedDate = subscription.WatchedDate;
                    subscriptionViewModel.SelectedMemberDetailsCommand = new DelegateCommand<string>(OnSelectedMemberDetailsExecute);
                    SubscriptionsViewModel.Add(subscriptionViewModel);
                }
            }

        }
    }
}
