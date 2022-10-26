using FriendStorage.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Formatting = Newtonsoft.Json.Formatting;

namespace FriendStorage.Repository
{
    public class FriendRepository : IFriendRepository
    {
        private const string StorageFile = "Friends.json";
        
        public async Task DeleteFriend(int friendId)
        {
            var friends = await ReadFromFile();
            var existing = friends.Single(f => f.Id == friendId);
            friends.Remove(existing);
            
            await SaveToFile(friends);
        }

        public async Task<IEnumerable<LookupItem>> GetAllFriends()
        {
            var result = await ReadFromFile();
            
            return result.Select(f => new LookupItem
              {
                  Id = f.Id,
                  DisplayMember = $"{f.FirstName} {f.LastName}"
              });
        }

        public async Task<Friend> GetFriendById(int friendId)
        {
            var friends = await ReadFromFile();
            return friends.Single(f => f.Id == friendId);
        }

        public async Task SaveFriend(Friend friend)
        {
            if (friend.Id <= 0)
            {
                await InsertFriend(friend);
            }
            else
            {
                await UpdateFriend(friend);
            }
        }

        private async Task UpdateFriend(Friend friend)
        {
            var friends = await ReadFromFile();
            var existing = friends.Single(f => f.Id == friend.Id);
            var indexOfExisting = friends.IndexOf(existing);
            friends.Insert(indexOfExisting, friend);
            friends.Remove(existing);
            await SaveToFile(friends);
        }

        private async Task InsertFriend(Friend friend)
        {
            var friends = await ReadFromFile();
            var maxFriendId = friends.Count == 0 ? 0 : friends.Max(f => f.Id);
            friend.Id = maxFriendId + 1;
            friends.Add(friend);
            await SaveToFile(friends);
        }

        private async Task SaveToFile(List<Friend> friendList)
        {
            string json = JsonConvert.SerializeObject(friendList, Formatting.Indented);
            await File.WriteAllTextAsync(StorageFile, json);
        }

        private async Task<List<Friend>> ReadFromFile()
        {
            if (!File.Exists(StorageFile))
            {
                return new List<Friend>
                {
                    new Friend{Id=1,FirstName = "Thomas",LastName="Huber",
                        Birthday = new DateTime(1980,10,28), IsDeveloper = true},
                    new Friend{Id=2,FirstName = "Julia",LastName="Huber",
                        Birthday = new DateTime(1982,10,10)},
                    new Friend{Id=3,FirstName="Anna",LastName="Huber",
                        Birthday = new DateTime(2011,05,13)},
                    new Friend{Id=4,FirstName="Sara",LastName="Huber",
                        Birthday = new DateTime(2013,02,25)},
                    new Friend{Id=5,FirstName = "Andreas",LastName="Böhler",
                        Birthday = new DateTime(1981,01,10), IsDeveloper = true},
                    new Friend{Id=6,FirstName="Urs",LastName="Meier",
                        Birthday = new DateTime(1970,03,5), IsDeveloper = true},
                     new Friend{Id=7,FirstName="Chrissi",LastName="Heuberger",
                        Birthday = new DateTime(1987,07,16)},
                     new Friend{Id=8,FirstName="Erkan",LastName="Egin",
                        Birthday = new DateTime(1983,05,23)},
                };
            }

            string json = await File.ReadAllTextAsync(StorageFile);
            
            var result = JsonConvert.DeserializeObject<List<Friend>>(json);

            return result ?? new List<Friend>();
        }
    }
}
