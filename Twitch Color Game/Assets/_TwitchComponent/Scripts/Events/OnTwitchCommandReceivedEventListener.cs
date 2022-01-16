namespace WIS.TwitchComponent.Events {
    using System;
    using UnityEngine.Events;
    using WIS.Utils.Events;
    public class OnTwitchCommandReceivedEventListener : BaseGameEventListener<OnTwitchCommandReceivedEventArgs, OnTwitchCommandReceivedEvent, OnTwitchCommandReceivedEventResponse> { }

    [Serializable]
    public class OnTwitchCommandReceivedEventResponse : UnityEvent<OnTwitchCommandReceivedEventArgs> { }
}