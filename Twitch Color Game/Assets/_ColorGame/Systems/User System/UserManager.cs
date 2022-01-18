using System.Collections.Generic;
using UnityEngine;
using WIS.TwitchComponent;
using WIS.TwitchComponent.Events;

namespace ColorGame.User {
    public class UserManager : MonoBehaviour {

        public static UserManager Instance = null;

        private UserCollection _users;
        private UserSaveLoadManager _saveLoadManager;
        private UserPointRedeemDictionary _pointRedeemDictionary;

        [SerializeField]
        private UserCommandDictionary _commandDictionary = null;
        
        private void Awake() {
            Instance = this;

            _users = new UserCollection();
            _saveLoadManager = new UserSaveLoadManager();
            _pointRedeemDictionary = new UserPointRedeemDictionary();

            _saveLoadManager.LoadUserData(SetUsers);
        }

        private void OnDestroy() {
            Instance = null;
        }

        private void SetUsers(List<User> users) {
            if (users == null || users.Count == 0) {
                return;
            }

            _users.SetUsers(users);
        }

        public void CreateUser(string userName, string displayName, int points) {
            if (_users.IsValidUser(userName)) {
                return;
            }

            _users.AddUser(userName, displayName, points);
            SaveCurrentUserData();
        }

        public bool UsePoints(string userName, int points) {
            if (!_users.IsValidUser(userName)) {
                return false;
            }

            var user = _users.GetUser(userName);
            if (user.UserPoints < points) {
                return false;
            }

            int newPointValue = user.UserPoints - points;
            _users.UpdateUserPoint(userName, newPointValue);
            SaveCurrentUserData();
            return true;
        }

        public void AddPoints(string userName, int points) {
            if (!_users.IsValidUser(userName)) {
                return;
            }

            int currentPoints = _users.GetUserPoint(userName);
            _users.UpdateUserPoint(userName, currentPoints + points);
            SaveCurrentUserData();
        }

        private void HandlePointRedeem(string redeemKey, string userName, string displayName) {
            int points = _pointRedeemDictionary.GetRedeemValue(redeemKey);

            if (!_users.IsValidUser(userName)) {
                CreateUser(userName, displayName, points);
            } else {
                AddPoints(userName, points);
            }
        }

        private void SaveCurrentUserData() {
            _saveLoadManager.SaveUserData(_users.UserList);
        }

        #region Twitch Redeems
        public void TwitchPointRedeemed(OnTwitchChannelPointRedeemedEventArgs args) {
            if (_pointRedeemDictionary.IsValidRedeemKey(args.RewardTitle)) {
                HandlePointRedeem(args.RewardTitle, args.UserName, args.UserDisplayName);
                Debug.Log(args.UserName + " redeemed " + args.RewardTitle);
            }
        }

        #endregion

        #region Twitch Commands

        private const string UnregisteredUserMessage = "Action Failed! User:{0} was not registered. Register by redeeming points first.";
        private const string InquirePointMessage = "{0} currently have {1} points!";

        public void InquirePointsCommand(string userName, string[] args) {
            var user = _users.GetUser(userName);
            string message;

            if (user == null) {
                message = string.Format(UnregisteredUserMessage, userName);
                TwitchComponent.SendTwitchMessage(message);
                return;
            }

            message = string.Format(InquirePointMessage, user.DisplayName, user.UserPoints.ToString());
            TwitchComponent.SendTwitchMessage(message);
        }

        #endregion
    }
}