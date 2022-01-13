namespace WIS.TwitchComponent.Events {
    using UnityEngine;
    using WIS.Utils.Events;
    [CreateAssetMenu(fileName = "New TwitchCommandReceivedEvent", menuName = "Twitch Component/Events/Command Received")]
    public class OnTwitchCommandReceivedEvent : BaseGameEvent<OnTwitchCommandReceivedEventArgs> { }

    public class OnTwitchCommandReceivedEventArgs {
        public string CommandString = null;
        public string[] CommandArguments = null;
        public string UserDisplayName = null;
        public string UserLoginName = null;
        public bool UserIsBroadcaster = false;
        public bool UserIsModeretor = false;
    }
}