using FriendStorage.Model;
using FriendStorage.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FriendStorage.Data
{
    public class NavigationDataProvider : INavigationDataProvider
    {
        private readonly IFriendRepository _friendRepository;

        public NavigationDataProvider(IFriendRepository friendRepository)
        {
            _friendRepository = friendRepository;
        }
        
        public async Task<IEnumerable<LookupItem>> GetAllFriends()
        {
            return await _friendRepository.GetAllFriends();
        }
    }
}
