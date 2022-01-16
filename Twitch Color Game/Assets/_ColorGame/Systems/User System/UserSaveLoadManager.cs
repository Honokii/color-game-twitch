using System;
using System.Collections.Generic;
using UnityEngine;

namespace ColorGame.User {
    public class UserSaveLoadManager {
        private string _saveFilePath = null;
        private bool _isEncrypted = false;

        private Action<List<User>> _UserLoadedAction = null;

        public UserSaveLoadManager() {
            _saveFilePath = Application.persistentDataPath + "/ColorGameUserData";
            Debug.Log("UserSaveLoadManager Initialized: saveFilePath - " + _saveFilePath);
        }

        public void LoadUserData(Action<List<User>> userLoadedComplete) {
            _UserLoadedAction = userLoadedComplete;
            SaveManager.Instance.Load<UserSaveLoadData>(_saveFilePath, DataLoaded, _isEncrypted);
        }

        public void SaveUserData(List<User> users) {
            UserSaveLoadData data = new UserSaveLoadData() {
                Users = users
            };

            SaveManager.Instance.Save(data, _saveFilePath, DataSaved, _isEncrypted);
        }

        private void DataLoaded(UserSaveLoadData data, SaveResult result, string message) {
            UserSaveLoadData resultData = null;
            if (result == SaveResult.EmptyData || result == SaveResult.Error) {
                resultData = new UserSaveLoadData();
            } else {
                resultData = data;
            }

            _UserLoadedAction(resultData.Users);
            _UserLoadedAction = null;
        }

        private void DataSaved(SaveResult result, string message) {
            Debug.Log(string.Format("UserSaveLoadManager: save result: {0}, message: {1}", result, message));
        }
    }

    [System.Serializable]
    public class UserSaveLoadData {
        public List<User> Users = new List<User>();
    }
}