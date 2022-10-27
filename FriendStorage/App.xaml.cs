using FriendStorage.Data;
using FriendStorage.Repository;
using FriendStorage.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace FriendStorage
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly ServiceProvider _serviceProvider;

        public App()
        {
            ServiceCollection services = new();
            ConfigureServices(services);
            _serviceProvider = services.BuildServiceProvider();
        }

        private void ConfigureServices(ServiceCollection services)
        {
            services.AddTransient<MainWindow>();

            services.AddTransient<MainViewModel>();
            services.AddTransient<INavigationViewModel, NavigationViewModel>();
            services.AddTransient<FriendEditViewModel>();

            services.AddTransient<INavigationDataProvider, NavigationDataProvider>();
            services.AddTransient<IFriendRepository, FriendRepository>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var mainWindow = _serviceProvider.GetService<MainWindow>();
            mainWindow?.Show();
        }
    }
}
