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
using System.Linq;
using System;
using MoviesSubscriptions.Model;
using System.Collections.Generic;
using MoviesSubscriptions.UI.Data.Lookups;

namespace MoviesSubscriptions.UI.ViewModell
{
    public class MemberNavigationViewModel : ViewModelBase, INavigationViewModel
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IEventAggregator _eventAggregator;
        private readonly IMovieRepository _movieRepository;
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly IMovieLookupDataService _movieLookupService;
        private MemberWrapper _selectedMember;
        private MovieWatchedViewModel _selectedMovieWatched;
      

        public MemberNavigationViewModel(IMemberRepository memberRepository,IEventAggregator eventAggregator,
            IMovieRepository movieRepository, ISubscriptionRepository subscriptionRepository, IMovieLookupDataService movieLookupService)
        {
            _memberRepository = memberRepository;
            _eventAggregator = eventAggregator;
            _movieRepository = movieRepository;
            _subscriptionRepository = subscriptionRepository;
            _movieLookupService = movieLookupService;

            _eventAggregator.GetEvent<AfterDetailSavedEvent>().Subscribe(AfterDetailSaved);
           _eventAggregator.GetEvent<AfterDetailDeletedEvent>().Subscribe(AfterDetailDeleted);
           _eventAggregator.GetEvent<OpenNavigationViewEvent>().Subscribe(OpenNavigationView);
            _eventAggregator.GetEvent<OpenAndFilterNavigationViewEvent>().Subscribe(OpenAndFilterNavigationView);


            AllMembers = new ObservableCollection<MemberWrapper>();
            MoviesWatched = new ObservableCollection<MovieWatchedViewModel>();
            SubscriptionsViewModel = new ObservableCollection<SubscriptionViewModel>();

           SelectedMemberCommand = new DelegateCommand(OnSelectetedMemberExecute);
           SubscribedMovieCommand = new DelegateCommand(OnSubscribeMovieExecute);
           MoviesWatchedCommand = new DelegateCommand(OnMoviesWatchedExecute);

        }

        public MemberWrapper SelectedMember
        {
            get { return _selectedMember; }
            set
            {
                _selectedMember = value;
                OnPropertyChanged();
            }
        }

          
        public MovieWatchedViewModel SelectedMovieWatched
        {
            get { return _selectedMovieWatched; }
            set
            {
                _selectedMovieWatched = value;
                OnPropertyChanged();
            }
        }

        public async Task LoadAsync()
        {
            AllMembers.Clear();
     
            var members = await _memberRepository.GetAllAsync();
            foreach (var member in members)
            {
               
               var modelWrapper = new MemberWrapper(member);
               AllMembers.Add(modelWrapper);
            }

            MoviesWatched.Clear();
            var movies = await _movieLookupService.GetMovieLookupAsync();
            foreach (var movie in movies)
            {
                MoviesWatched.Add(new MovieWatchedViewModel 
                { Id = movie.Id,  Name = movie.DisplayMember, MovieWatchedDate= DateTime.Now });
            }
            SelectedMovieWatched = MoviesWatched.FirstOrDefault();
             
        }
        
        
        public ObservableCollection<MemberWrapper> AllMembers { get; }
        public ObservableCollection<MovieWatchedViewModel> MoviesWatched { get; }
        public ObservableCollection<SubscriptionViewModel> SubscriptionsViewModel { get; }
        
        public ICommand SelectedMemberCommand { get; }
        public ICommand SubscribedMovieCommand { get; }
        public ICommand MoviesWatchedCommand { get; }


        private async void AfterDetailDeleted(AfterDetailDeletedEventArgs args)
        {
            await LoadAsync();
           
        }

        private async void AfterDetailSaved(AfterDetailSavedEventArgs args)
        {
            await LoadAsync();
        }

        private void OnSelectetedMemberExecute()
        {
            if (SelectedMember != null)
            {
                _eventAggregator.GetEvent<OpenDetailViewEvent>().Publish(new
                    OpenDetailViewEventArgs
                {
                    Id = SelectedMember.Id,
                    ViewModelName = typeof(MemberDetailsViewModel).Name
                });
            }
        }

        private async void OnSubscribeMovieExecute()
        {
            if (SelectedMember != null)
            {
                var movie = MoviesWatched.Where(m => m.Id == SelectedMovieWatched.Id).FirstOrDefault();
                if (movie != null)
                {
                    _subscriptionRepository.Add(new Subscription
                    {
                        MemberId = SelectedMember.Id,
                        MovieId = movie.Id,
                        WatchedDate = SelectedMovieWatched.MovieWatchedDate
                    });
                    await _subscriptionRepository.SaveAsync();

                }
            }

            var keptMemberId = SelectedMember.Id;
            await LoadAsync();
            SelectedMember = AllMembers.Where(m => m.Id == keptMemberId).FirstOrDefault();
        }

     
        private async void OpenNavigationView(OpenNavigationViewEventArgs args)
        {
            await LoadAsync();

            var subscription = await _subscriptionRepository.GetByIdAsync(args.Id);
            var member = await _memberRepository.GetByIdAsync(subscription.MemberId);
            var memberWrpper = AllMembers.Where(m => m.Id == member.Id).FirstOrDefault();
            if (memberWrpper != null)
            {
                SelectedMember = memberWrpper;
            }
        }

        private void OnSelectedMovieDetailsExecute(string subscriptionId)
        {
             _eventAggregator.GetEvent<OpenNavigationViewEvent>()
                       .Publish(new
                         OpenNavigationViewEventArgs
                       {
                           Id = int.Parse(subscriptionId),
                           ViewModelName = typeof(MoviesViewModel).Name,
                         }); ; 
        }

        private async void OpenAndFilterNavigationView(OpenAndFilterNavigationViewEventArgs args)
        {
            if (String.IsNullOrEmpty(args.SelectedItem))
            {
                await LoadAsync();
            }
            else
            {
                var filteredMembers = AllMembers.Where(m => m.Name.ToLower().StartsWith(args.SelectedItem.ToLower()));
                List<MemberWrapper> tempList = new List<MemberWrapper>();
                foreach (var filteredMember in filteredMembers)
                {
                    tempList.Add(filteredMember);
                }
                AllMembers.Clear();
                foreach (var item in tempList)
                {
                    AllMembers.Add(item);
                }

            }

        }


        private async void OnMoviesWatchedExecute()
        {
          
            if (SelectedMember != null)
            {
                SubscriptionsViewModel.Clear();

                SubscriptionViewModel subscriptionViewModel;
                foreach (var subscription in SelectedMember.Subscriptions)
                {
                    subscriptionViewModel = new SubscriptionViewModel();
                    Movie movie = null;
                    try
                    {
                        movie = await _movieRepository.GetByIdAsync(subscription.MovieId);
                    }
                    //TODO: A second operation started on this context before a previous operation completed.
                    // This is usually caused by different threads using the same instance of DbContext. 
                    catch (Exception )
                    {
                        
                    }
                    // TODO:Bug - add double watch date withou movie

                    if(movie == null)
                    {
                        continue;
                    }
                    subscriptionViewModel.Id = subscription.Id;
                    subscriptionViewModel.Movie = movie;
                    subscriptionViewModel.Member = SelectedMember.Model;
                    subscriptionViewModel.WatchedDate = subscription.WatchedDate;
                    subscriptionViewModel.SelectedMovieDetailsCommand = new DelegateCommand<string>(OnSelectedMovieDetailsExecute);
                    SubscriptionsViewModel.Add(subscriptionViewModel);
                }
            }

        }
    }

    public class MovieWatchedViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime MovieWatchedDate { get; set; }
    }
}
