using Autofac;
using MoviesSubscriptions.UI.Data.Repositories;
using MoviesSubscriptions.UI.Event;
using MoviesSubscriptions.UI.Startup;
using Prism.Events;
using System;
using System.Windows;

namespace MoviesSubscriptions.UI
{
      public partial class App : Application
    {
         public static readonly Bootstrapper bootstrapper = new Bootstrapper();
         private LoginWindow _loginWindow;

        private void Application_Startup(object sender, StartupEventArgs e)
        {
             var container = bootstrapper.Bootstrap();
            _loginWindow = container.Resolve<LoginWindow>();
            var eventAggregator = container.Resolve<IEventAggregator>();
            eventAggregator.GetEvent<AfterLoginExecuteEvent>()
                .Subscribe(AfterLoginExecute);
            _loginWindow.ShowDialog();
        
        }

        private void AfterLoginExecute(AfterLoginExecuteEventArgs args)
        {
            if (args.IsUserExists )
            {
           
                var container = bootstrapper.Bootstrap();
                var mainWindow = container.Resolve<MainWindow>();
                mainWindow.Show();

                _loginWindow.Close();
         }
            else
            {
                _loginWindow.ViewModel.ErrorMessage = "User name or password are not valid";
            }

        }

        private void Application_DispatcherUnhandledException(object sender,
          System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show("Unexpected error occured. Please inform the admin."
              + Environment.NewLine + e.Exception.Message, "Unexpected error");

            e.Handled = true;
        }
    }
}
