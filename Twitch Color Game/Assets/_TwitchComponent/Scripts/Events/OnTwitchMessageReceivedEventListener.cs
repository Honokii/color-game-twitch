namespace WIS.TwitchComponent.Events {
    using System;
    using UnityEngine.Events;
    using WIS.Utils.Events;

    public class OnTwitchMessageReceivedEventListener : BaseGameEventListener<OnTwitchMessageReceivedEventArgs, OnTwitchMessageReceivedEvent, OnTwitchMessageReceivedEventResponse> { }

    [Serializable]
    public class OnTwitchMessageReceivedEventResponse : UnityEvent<OnTwitchMessageReceivedEventArgs> { }
}