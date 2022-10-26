using FriendStorage.Data;
using FriendStorage.Model;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace FriendStorage.ViewModel
{
    public interface INavigationViewModel
    {
        Task Load();
    }
    public class NavigationViewModel : ViewModelBase,
      INavigationViewModel
    {
        private INavigationDataProvider _dataProvider;

        public NavigationViewModel(INavigationDataProvider dataProvider)
        {
            Friends = new ObservableCollection<LookupItem>();
            _dataProvider = dataProvider;
        }

        public async Task Load()
        {
            Friends.Clear();
            foreach (var friend in await _dataProvider.GetAllFriends())
            {
                Friends.Add(friend);
            }
        }

        public ObservableCollection<LookupItem> Friends { get; private set; }
    }
}
