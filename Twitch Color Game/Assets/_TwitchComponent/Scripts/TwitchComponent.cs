namespace WIS.TwitchComponent {
    using System;
    using UnityEngine;

    using Events;
    public class TwitchComponent : MonoBehaviour {
        private static  TwitchComponent _Instance = null;
        public static TwitchComponent Instance => _Instance;

        [SerializeField]
        private TwitchChannelConfig _BotConfig = null;

        [SerializeField]
        private TwitchChannelConfig _StreamerConfig = null;

        [SerializeField]
        private TwitchApiComponent _ApiComponent = null;

        [SerializeField]
        private TwitchPubsubComponent _PubsubComponent = null;

        [SerializeField]
        private TwitchClientComponent _ClientComponent = null;

        private string _StreamerChannelId = null;        

        void Start()
        {
            if (_Instance != this) {
                _Instance = this;
            }

            //api
            _ApiComponent.InitializeManager(_StreamerConfig.ClineId, _StreamerConfig.AccessToken);
            StartCoroutine(_ApiComponent.GetTwitchChannelId(_StreamerConfig.UserName, (response) => {
                _StreamerChannelId = response;
                // Debug.Log("Streamer Channel Id: " + _StreamerChannelId);

                //pubsub
                _PubsubComponent.InitializeComponent(_StreamerConfig.UserName, _StreamerConfig.AccessToken, _StreamerChannelId);
            }));

            //client
            _ClientComponent.InitializeComponent(_StreamerConfig.UserName, _BotConfig.UserName, _BotConfig.AccessToken);
            _ClientComponent.Connect();
        }

        void OnDestroy()
        {
            if (_Instance == this) {
                _Instance = null;
            }
        }

        public void StartFetchingClipForUser(string userName, Action<string[]> onComplete) {
            StartCoroutine(_ApiComponent.GetTwitchChannelClipsByUserName(userName, onComplete));
        }

        public void OnTwitchCommandReceived(OnTwitchCommandReceivedEventArgs args) {
            if (args.CommandString == "so2") {
                // Debug.Log("Command Received");
                StartFetchingClipForUser(args.CommandArguments[0], null);
            }
        }
    }
}