namespace WIS.TwitchComponent.Events {
    using System;
    using UnityEngine;
    using WIS.Utils.Events;
    [CreateAssetMenu(fileName = "New TwichChannelPointRedeemedEvent", menuName = "Twitch Component/Events/Channel Point Redeemed")]
    public class OnTwitchChannelPointRedeemedEvent : BaseGameEvent<OnTwitchChannelPointRedeemedEventArgs> { }

    public class OnTwitchChannelPointRedeemedEventArgs {
        public string RewardTitle = null;
        public string UserDisplayName = null;
        public string UserInput = null;
    }
}