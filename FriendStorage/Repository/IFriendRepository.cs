using FriendStorage.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FriendStorage.Repository
{
    public interface IFriendRepository
    {
        Task<Friend> GetFriendById(int friendId);

        Task SaveFriend(Friend friend);
        
        Task DeleteFriend(int friendId);

        Task<IEnumerable<LookupItem>> GetAllFriends();
    }
}
