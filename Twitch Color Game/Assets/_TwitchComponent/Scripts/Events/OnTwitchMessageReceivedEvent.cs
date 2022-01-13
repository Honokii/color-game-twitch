namespace WIS.TwitchComponent.Events {
    using UnityEngine;
    using WIS.Utils.Events;

    [CreateAssetMenu(fileName = "New TwichMessageReceivedEvent", menuName = "Twitch Component/Events/Message Received")]
    public class OnTwitchMessageReceivedEvent : BaseGameEvent<OnTwitchMessageReceivedEventArgs> { }

    public class OnTwitchMessageReceivedEventArgs {
        public string ColorHex;
        public string DisplayName;
        public string Message;
    }
}