using System.Threading.Tasks;

namespace FriendStorage.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel(INavigationViewModel navigationViewModel)
        {
            NavigationViewModel = navigationViewModel;

        }

        public INavigationViewModel NavigationViewModel { get; private set; }

        public async Task Load()
        {
            await NavigationViewModel.Load();
        }
    }
}
