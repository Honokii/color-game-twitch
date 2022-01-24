using System.Linq;
using System.Collections.Generic;

namespace ColorGame.User {
    public class UserCollection {
        private Dictionary<string, User> _users = new Dictionary<string, User>();

        public int Count {
            get {
                return _users.Count;
            }
        }

        public List<User> UserList {
            get {
                return _users.Values.ToList();
            }
        }

        public void SetUsers(List<User> users) {
            foreach(var user in users) {
                _users[user.UserName] = user;
            }
        }

        public void AddUser(string userName, string displayName, int point) {
            if (_users.ContainsKey(userName)) {
                return;
            }

            _users.Add(userName, new User() { 
                UserName = userName,
                DisplayName = displayName,
                UserPoints = point
            });
        }

        public void DeleteUser(string userName) {
            if (!_users.ContainsKey(userName)) {
                return;
            }

            _users.Remove(userName);
        }

        public void UpdateUserPoint(string userName, int point) {
            var user = GetUser(userName);
            if (user == null) {
                return;
            }

            user.UserPoints = point;
            _users[userName] = user;
        }

        public void UpdateUser(string userName, string displayName, int point) {
            var user = GetUser(userName);
            if (user == null) {
                return;
            }

            user.DisplayName = displayName;
            user.UserPoints = point;
            _users[userName] = user;
        }

        public User GetUser(string userName) {
            if (!IsValidUser(userName)) {
                return null;
            }

            return _users[userName];
        }

        public bool IsValidUser(string userName) {
            return _users.ContainsKey(userName);
        }

        public int GetUserPoint(string userName) {
            var user = GetUser(userName);
            if (user == null) {
                return -1; //return -1 when user is not registered.
            }

            return user.UserPoints;
        }
    }
}