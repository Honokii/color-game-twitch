using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using WIS.TwitchComponent.Events;

namespace WIS.TwitchComponent.Commands {
    [RequireComponent(typeof(OnTwitchCommandReceivedEventListener))]
    public class CommandDictionary : SerializedMonoBehaviour {
        public string DictionaryId = null;

        [SerializeField]
        private Dictionary<string, Command> _commands = new Dictionary<string, Command>();
        private OnTwitchCommandReceivedEventListener _listener = null;

        #region Unity Methods

        private void Start() {
            _listener = GetComponent<OnTwitchCommandReceivedEventListener>();
            _listener.Response.AddListener(TwitchCommandReceived);
        }

        private void OnDestroy() {
            _listener.Response.RemoveListener(TwitchCommandReceived);
        }

        private void OnEnable() {
            CommandsManager.RegisterDictionary(this);
        }

        private void OnDisable() {
            CommandsManager.UnregisterDictionary(this);
        }
        #endregion

        public void HandleCommandInvocation(string userName, CommandUserLevel userLevel, string commandString, string[] commandArgs) {
            if (!_commands.ContainsKey(commandString)) {
                return;
            }

            var command = _commands[commandString];
            if (userLevel < command.UserLevel) {
                return;
            }

            command.OnCommandAction?.Invoke(userName, commandArgs);
        }

        #region Twitch Methods

        private void TwitchCommandReceived(OnTwitchCommandReceivedEventArgs args) {
            CommandUserLevel userLevel = CommandUserLevel.Default;
            if (args.UserIsModeretor) {
                userLevel = CommandUserLevel.Moderator;
            } else if (args.UserIsBroadcaster) {
                userLevel = CommandUserLevel.Broadcaster;
            }

            HandleCommandInvocation(args.Username, userLevel, args.CommandString, args.CommandArguments);
        }

        #endregion
    }
}