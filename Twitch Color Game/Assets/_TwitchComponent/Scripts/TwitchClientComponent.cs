namespace WIS.TwitchComponent {
    
    using UnityEngine;

    using TwitchLib.Unity;
    using TwitchLib.Client.Models;
    using TwitchLib.Client.Events;
    using Events;

    public class TwitchClientComponent : MonoBehaviour {
        private Client _Client = null;
        private Client TwitchClient {
            get {
                if (_Client == null) {
                    _Client = new Client();
                }

                return _Client;
            }
        }

        private ConnectionCredentials _BotCredentials = null;
        private string _StreamerUserName = null;
        private bool _IsInitialized = false;

        #region Unity Methods

        void Start() {
            TwitchClient.OnConnected += Connected;
            TwitchClient.OnJoinedChannel += JoinedChannel;
            TwitchClient.OnMessageReceived += MessageReceived;
            TwitchClient.OnChatCommandReceived += ChatCommandReceived;
        }

        void OnDestroy() {
            TwitchClient.OnConnected -= Connected;
            TwitchClient.OnJoinedChannel -= JoinedChannel;
            TwitchClient.OnMessageReceived -= MessageReceived;
            TwitchClient.OnChatCommandReceived -= ChatCommandReceived;
        }
             
        #endregion

        #region Public Methods

        public void InitializeComponent(string streamerUserName, string botUserName, string botAccessToken) {
            _BotCredentials = new ConnectionCredentials(botUserName, botAccessToken);
            _StreamerUserName = streamerUserName;
            TwitchClient.Initialize(_BotCredentials, _StreamerUserName);
            _IsInitialized = true;
        }

        public void Connect() {
            if (_IsInitialized == false) {
                return;
            }

            TwitchClient.Connect();
        }

        public void SendTwitchMessage(string message) {
            if (TwitchClient.IsInitialized == false) {
                return;
            }

            if (TwitchClient.IsConnected == false) {
                return;
            }

            TwitchClient.SendMessage(_StreamerUserName, message);
        }

        #endregion

        #region Twitch Callbacks

        private void Connected(object sender, OnConnectedArgs e) {
            Debug.Log(e.BotUsername + " connected!");
        }

        private void JoinedChannel(object sender, OnJoinedChannelArgs e) {
            Debug.Log(e.BotUsername + " joined channel: " + e.Channel);
        }

        private void MessageReceived(object sender, OnMessageReceivedArgs e) {
            Debug.Log(e.ChatMessage.DisplayName + " : " + e.ChatMessage.Message);

            TwitchEventComponent.Instance.onTwitchMessageReceivedEvent.Raise(new OnTwitchMessageReceivedEventArgs() {
                ColorHex = e.ChatMessage.ColorHex,
                DisplayName = e.ChatMessage.DisplayName,
                Message = e.ChatMessage.Message
            });
        }

        private void ChatCommandReceived(object sender, OnChatCommandReceivedArgs e) {
            Debug.Log(e.Command.ChatMessage.DisplayName + "sent a command: " + e.Command.CommandText);

            TwitchEventComponent.Instance.onTwitchCommandReceivedEvent.Raise(new OnTwitchCommandReceivedEventArgs() {
                CommandString = e.Command.CommandText,
                CommandArguments = e.Command.ArgumentsAsList.ToArray(),
                UserDisplayName = e.Command.ChatMessage.DisplayName,
                UserIsBroadcaster = e.Command.ChatMessage.IsBroadcaster,
                UserIsModeretor = e.Command.ChatMessage.IsModerator,
                Username = e.Command.ChatMessage.Username
            });
        }

        #endregion
    }
}