using Autofac;
using MoviesSubscriptions.DataAccess;
using MoviesSubscriptions.UI.Data.Lookups;
using MoviesSubscriptions.UI.Data.Repositories;
using MoviesSubscriptions.UI.View;
using MoviesSubscriptions.UI.View.Services;
using MoviesSubscriptions.UI.ViewModel;
using MoviesSubscriptions.UI.ViewModel.Base;
using MoviesSubscriptions.UI.ViewModell;
using Prism.Events;


namespace MoviesSubscriptions.UI.Startup
{
     public class Bootstrapper
    {
        public IContainer Bootstrap()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<EventAggregator>().As<IEventAggregator>().SingleInstance();

            builder.RegisterType<MoviesSubscriptionsDbContext>().AsSelf();

            builder.RegisterType<LoginWindow>().AsSelf();
            builder.RegisterType<MainWindow>().AsSelf();

            builder.RegisterType<MessageDialogService>().As<IMessageDialogService>();

            builder.RegisterType<LoginsViewModel>().AsSelf();
            builder.RegisterType<MainViewModel>().AsSelf();

            builder.RegisterType<WelcomeViewModel>().As<TabViewModelBase>();
            builder.RegisterType<MoviesViewModel>().As<TabViewModelBase>();
            builder.RegisterType<MembersViewModel>().As<TabViewModelBase>();
            builder.RegisterType<UsersViewModel>().As<TabViewModelBase>(); ;

        
            builder.RegisterType<UserLoginRepository>().As<IUserLoginRepository>();
            builder.RegisterType<MovieRepository>().As<IMovieRepository>();
            builder.RegisterType<MemberRepository>().As<IMemberRepository>();
            builder.RegisterType<UserRepository>().As<IUserRepository>();
            builder.RegisterType<SubscriptionRepository>().As<ISubscriptionRepository>();


            builder.RegisterType<MovieNavigationViewModel>()
            .Keyed<INavigationViewModel>(nameof(MovieNavigationViewModel));
            builder.RegisterType<MemberNavigationViewModel>()
            .Keyed<INavigationViewModel>(nameof(MemberNavigationViewModel));
            builder.RegisterType<UserNavigationViewModel>()
            .Keyed<INavigationViewModel>(nameof(UserNavigationViewModel));


            builder.RegisterType<MovieDetailsViewModel>()
           .Keyed<IDetailViewModel>(nameof(MovieDetailsViewModel));
            builder.RegisterType<MemberDetailsViewModel>()
             .Keyed<IDetailViewModel>(nameof(MemberDetailsViewModel));
             builder.RegisterType<UserDetailsViewModel>()
            .Keyed<IDetailViewModel>(nameof(UserDetailsViewModel));


            builder.RegisterType<LookupDataService>().AsImplementedInterfaces();

            return builder.Build();
        }
    }
    }

