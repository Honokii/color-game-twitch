namespace WIS.TwitchComponent.Commands {
    using System;
    using UnityEngine.Events;

    [Serializable]
    public class Command
    {
        public string CommandString = null;
        public CommandUserLevel UserLevel = 0;
        public CommandAction OnCommandAction = null;
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
    public class CommandAction : UnityEvent<string, string[]> { }
}