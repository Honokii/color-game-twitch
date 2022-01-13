namespace WIS.TwitchComponent.Commands {
    using System;
    using UnityEngine.Events;
    using Events;

    [Serializable]
    public class Command
    {
        public string CommandString = null;
        public CommandUserLevel UserLevel = 0;
        public CommandAction OnCommandAction = null;

        public void InvokeCommand(OnTwitchCommandReceivedEventArgs args) {
            CommandUserLevel reqLevel = 0;
            if (args.UserIsModeretor) {
                reqLevel = CommandUserLevel.Moderator;
            }

            if (args.UserIsBroadcaster) {
                reqLevel = CommandUserLevel.Broadcaster;
            }

            if (UserLevel < reqLevel) {
                return;
            }

            OnCommandAction?.Invoke(args);
        }
    }

    public enum CommandUserLevel
    {
        Default = 0,
        Vip = 1,
        Subscriber = 2,
        Moderator = 8,
        Broadcaster = 9
    }

    [Serializable]
    public class CommandAction : UnityEvent<OnTwitchCommandReceivedEventArgs> { }
}