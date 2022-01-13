namespace WIS.TwitchComponent {
    using UnityEngine;
    using Events;

    public class TwitchEventComponent : MonoBehaviour
    {
        private static TwitchEventComponent _Instance = null;
        public static TwitchEventComponent Instance => _Instance;

        public OnTwitchMessageReceivedEvent onTwitchMessageReceivedEvent = null;
        public OnTwitchCommandReceivedEvent onTwitchCommandReceivedEvent  = null;
        public OnTwitchChannelPointRedeemedEvent onTwitchChannelPointRedeemedEvent = null;
        
        #region Unity Methods
        void OnEnable()
        {
            _Instance = this;
        }

        void OnDisable()
        {
            _Instance = null;
        }
        #endregion
    }

    
}