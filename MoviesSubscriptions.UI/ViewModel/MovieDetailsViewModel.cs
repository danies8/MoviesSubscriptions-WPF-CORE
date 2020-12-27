
using MoviesSubscriptions.Model;
using MoviesSubscriptions.UI.Data.Repositories;
using MoviesSubscriptions.UI.View.Services;
using MoviesSubscriptions.UI.ViewModel.Base;
using MoviesSubscriptions.UI.Wrapper;
using Prism.Commands;
using Prism.Events;
using System;
using System.Threading.Tasks;

namespace MoviesSubscriptions.UI.ViewModel
{
    public class MovieDetailsViewModel : DetailViewModelBase
    {
        private readonly IMovieRepository _movieRepository;
        private MovieWrapper _movie;

        public MovieDetailsViewModel(IEventAggregator eventAggregator, IMessageDialogService messageDialogService,
            IMovieRepository movieRepository) : base(eventAggregator, messageDialogService)
        {
            _movieRepository = movieRepository;

        }

        public async override Task LoadAsync(int movieId)
        {
            var movie = movieId > 0
              ? await EditMovie(movieId)
              : CreateMovie();

            Id = movieId;

            InitializetMovie(movie);
        }

        public MovieWrapper Movie
        {
            get { return _movie; }
            private set
            {
                _movie = value;
                OnPropertyChanged();
            }
        }

        private Movie CreateMovie()
        {
            Movie movie = new Movie();
            _movieRepository.Add(movie);
            return movie;
        }

        private void InitializetMovie(Movie movie)
        {
            Movie = new MovieWrapper(movie);
            Movie.PropertyChanged += (s, e) =>
            {
                if (!HasChanges)
                {
                    HasChanges = _movieRepository.HasChanges();
                }

                if (e.PropertyName == nameof(Movie.HasErrors))
                {
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
                if (e.PropertyName == nameof(Movie.Name))
                {
                    SetTitle();
                }
            };
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();

            if (Movie.Id == 0)
            {
                // Little trick to trigger the validation
                Movie.Name = "";
                Movie.Genres = "";
                Movie.ImageUrl = "";
                Movie.Premired = DateTime.Now;
            }
            SetTitle();
        }

        private void SetTitle()
        {
            Title = $"{Movie.Name}";
        }

        private async Task<Movie> EditMovie(int movieId)
        {
            var movie = await _movieRepository.GetByIdAsync(movieId);
            return movie;
        }


        protected async override void OnDeleteExecute()
        {
            var result = await MessageDialogService.ShowOkCancelDialogAsync($"Do you really want to delete the movie {Movie.Name}?", "Question");
            if (result == MessageDialogResult.OK)
            {
                _movieRepository.Remove(Movie.Model);
                await _movieRepository.SaveAsync();
                RaiseDetailDeletedEvent(Movie.Id);
            }
        }


        protected override bool OnSaveCanExecute()
        {
            return Movie != null && !Movie.HasErrors && HasChanges;
        }

        protected async override void OnSaveExecute()
        {
            await _movieRepository.SaveAsync();
            HasChanges = _movieRepository.HasChanges();
            Id = Movie.Id;
            RaiseDetailSavedEvent(Movie.Id);
        }


    }
}
