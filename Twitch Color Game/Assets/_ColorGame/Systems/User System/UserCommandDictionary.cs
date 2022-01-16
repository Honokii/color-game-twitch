using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

namespace ColorGame.User {
    [Serializable]
    public class UserCommandDictionary : SerializedMonoBehaviour {

        [SerializeField]
        private Dictionary<string, UserCommandData> CommandDatas = new Dictionary<string, UserCommandData>();

        public void HandleCommandInvocation(string commandString, bool userIsBroadcaster, bool userIsModerator, string userName, string[] commandArgs) {
            if (!CommandDatas.ContainsKey(commandString)) {
                return;
            }

            var command = CommandDatas[commandString];
            if (command.IsBroadcasterCommand && !userIsBroadcaster) {
                return;
            }

            if (command.IsModeratorCommand && !userIsModerator) {
                return;
            }

            command.CommandAction?.Invoke(userName, commandArgs);
        }
    }

    [Serializable]
    public class UserCommandData {
        public string CommandString;
        public bool IsBroadcasterCommand;
        public bool IsModeratorCommand;
        public UserCommandAction CommandAction;
    }

    [Serializable]
    public class UserCommandAction : UnityEvent<string, string[]> { }
}