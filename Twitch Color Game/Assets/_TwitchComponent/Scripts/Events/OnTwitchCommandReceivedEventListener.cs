namespace WIS.TwitchComponent.Events {
    using System;
    using UnityEngine;
    using UnityEngine.Events;
    using WIS.Utils.Events;
    public class OnTwitchCommandReceivedEventListener : BaseGameEventListener<OnTwitchCommandReceivedEventArgs, OnTwitchCommandReceivedEvent, OnTwitchCommandReceivedEventResponse> {
        [SerializeField]
        private string _CommandString = null;

        [SerializeField]
        private CommandUserLevel _UserLevel = 0;

        public new void OnEventRaised(OnTwitchCommandReceivedEventArgs item) {
            CommandUserLevel reqLevel = 0;
            if (item.UserIsModeretor) {
                reqLevel = CommandUserLevel.Moderator;
            }

            if (item.UserIsBroadcaster) {
                reqLevel = CommandUserLevel.Broadcaster;
            }

            if (_UserLevel < reqLevel) {
                return;
            }

            if (item.CommandString != _CommandString) {
                return;
            }

            base.OnEventRaised(item);
        }
    }

    [Serializable]
    public class OnTwitchCommandReceivedEventResponse : UnityEvent<OnTwitchCommandReceivedEventArgs> { }

    public enum CommandUserLevel
    {
        Default = 0,
        Vip = 1,
        Subscriber = 2,
        Moderator = 8,
        Broadcaster = 9
    }
}