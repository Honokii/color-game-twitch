namespace WIS.TwitchComponent {
    using UnityEngine;
    
    [CreateAssetMenu(fileName = "New TwitchChannelConfig", menuName = "Twitch Component/Channel Config")]
    public class TwitchChannelConfig : ScriptableObject {
        /// <summary>
        /// Username of the bot. The name of the account with the twitch link
        /// </summary>
        [SerializeField] private string _UserName = null;

        /// <summary>
        /// ID that was generated from [twitchtokengenerator.com] using the bot account
        /// </summary>
        [SerializeField] private string _ClientId = null;

        /// <summary>
        /// Access token generated from [twitchtokengenerator.com] using the bot account
        /// </summary>
        [SerializeField] private string _AccessToken = null;

        /// <summary>
        /// Refresh token generated from [twitchtokengenerator.com] using the bot account.
        /// Used for updating the permissions of the generated ids and tokens.
        /// </summary>
        [SerializeField] private string _RefreshToken = null;

        public string UserName => _UserName;
        public string ClineId => _ClientId;
        public string AccessToken => _AccessToken;
        public string RefreshToken => _RefreshToken;
    }
}