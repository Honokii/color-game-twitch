namespace WIS.TwitchComponent.Events {
    using System;
    using UnityEngine.Events;
    using WIS.Utils.Events;

    public class OnTwitchChannelPointRedeemedEventListener : BaseGameEventListener<OnTwitchChannelPointRedeemedEventArgs, OnTwitchChannelPointRedeemedEvent, OnTwitchChannelPointRedeemedEventResponse> { }

    [Serializable]
    public class OnTwitchChannelPointRedeemedEventResponse : UnityEvent<OnTwitchChannelPointRedeemedEventArgs> {}
}