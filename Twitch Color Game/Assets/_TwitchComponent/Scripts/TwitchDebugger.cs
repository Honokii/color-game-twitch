namespace WIS.TwitchComponent.Debugger {
    using UnityEngine;
    using WIS.TwitchComponent.Events;
    public class TwitchDebugger : MonoBehaviour
    {
        public void MessageReceived(OnTwitchMessageReceivedEventArgs args) {
            Debug.Log("Message Received: " + args.Message);
        }

        public void CommandReceived(OnTwitchCommandReceivedEventArgs args) {
            Debug.Log("Command Received: " + args.CommandString);
        }

        public void ChannelPointRedeemed(OnTwitchChannelPointRedeemedEventArgs args) {
            Debug.Log("Channel Point Redeemed: " + args.RewardTitle);
        }
    }
}