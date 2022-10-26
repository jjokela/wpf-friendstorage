using FriendStorage.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FriendStorage.Data
{
    public interface INavigationDataProvider
    {
        Task<IEnumerable<LookupItem>> GetAllFriends();
    }
}
