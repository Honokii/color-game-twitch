namespace WIS.TwitchComponent {
    using UnityEngine;

    [CreateAssetMenu(fileName = "New TwitchAppConfig", menuName = "Twitch Component/App Config")]
    public class TwitchAppConfig : ScriptableObject
    {
        [SerializeField] private string _ClientId = null;
        [SerializeField] private string _ClientSecret = null;

        public string ClientId => _ClientId;
        public string ClientSecret => _ClientSecret;
    }
}