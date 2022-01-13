using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ColorGame.User {
    public class UserSaveLoadManager {
        private string _saveFilePath = null;

        public UserSaveLoadManager() {
            _saveFilePath = Application.persistentDataPath + "/ColorGameUserData";
        }

        public void LoadUserData() {

        }
    }
}