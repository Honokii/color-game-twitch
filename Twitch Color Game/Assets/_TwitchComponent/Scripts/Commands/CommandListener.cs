namespace WIS.TwitchComponent.Commands {
    using System.Collections.Generic;
    using UnityEngine;

    using Events;

    public class CommandListener : MonoBehaviour
    {
        [SerializeField]
        private List<Command> _Commands = null;

        void OnEnable()
        {
            // TwitchEventComponent.Instance?.onTwitchCommandReceivedEvent.AddListener(this);
        }

        void OnDisable()
        {
            // TwitchEventComponent.Instance?.onTwitchCommandReceivedEvent.RemoveListener(this);
        }

        public void OnRaised(OnTwitchCommandReceivedEventArgs args) {
            if (_Commands == null) {
                return;
            }
            
            foreach (var command in _Commands) {
                //command.InvokeCommand(args);
            }
        }
    }
}