namespace WIS.TwitchComponent {
    using System;
    using UnityEngine;
    using TwitchLib.Unity;
    using TwitchLib.PubSub.Events;

    using Events;

    public class TwitchPubsubComponent : MonoBehaviour
    {
        private PubsubProperties _Properties = null;
        
        private PubSub _TwitchPubsub = null;
        private PubSub TwitchPubsub {
            get {
                if (_TwitchPubsub == null) {
                    _TwitchPubsub = new PubSub();
                }

                return _TwitchPubsub;
            }
        }

        #region Unity Methods

        void Start()
        {
            TwitchPubsub.OnPubSubServiceConnected += PubSubServiceConnected;
            TwitchPubsub.OnChannelPointsRewardRedeemed += ChannelPointsRewardRedeemed;
        }

        void OnDestroy()
        {
            TwitchPubsub.OnPubSubServiceConnected -= PubSubServiceConnected;
            TwitchPubsub.OnChannelPointsRewardRedeemed -= ChannelPointsRewardRedeemed;
        }

        #endregion

        #region Public Methods

        public void InitializeComponent(string streamerUserName, string streamerAccessToken, string streamerTwitchId) {
            if (_Properties == null) {
                _Properties = new PubsubProperties();
            }

            _Properties.StreamerUserName = streamerUserName;
            _Properties.StreamerAccessToken = streamerAccessToken;
            _Properties.StreamerTwitchId = streamerTwitchId;

            TwitchPubsub.Connect();
        }

        #endregion

        #region Private Methods

        #endregion

        #region Twitch Callbacks

        private void PubSubServiceConnected(object sender, EventArgs e) {
            // Debug.Log("PubsubComponent: Service Connected.");

            TwitchPubsub.ListenToChannelPoints(_Properties.StreamerTwitchId);
            TwitchPubsub.SendTopics(_Properties.StreamerAccessToken);
        }

        private void ChannelPointsRewardRedeemed(object sender, OnChannelPointsRewardRedeemedArgs e) {
            TwitchEventComponent.Instance.onTwitchChannelPointRedeemedEvent?.Raise(new OnTwitchChannelPointRedeemedEventArgs() {
                RewardTitle = e.RewardRedeemed.Redemption.Reward.Title,
                UserName = e.RewardRedeemed.Redemption.User.Login,
                UserDisplayName = e.RewardRedeemed.Redemption.User.DisplayName,
                UserInput = e.RewardRedeemed.Redemption.UserInput
            }) ;
        }

        #endregion

        public class PubsubProperties {
            public string StreamerUserName;
            public string StreamerAccessToken;
            public string StreamerTwitchId;
        }

    }
}