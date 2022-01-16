using System.Collections.Generic;
using UnityEngine;
using ColorGame.User;
using WIS.TwitchComponent;
using WIS.TwitchComponent.Events;

namespace ColorGame {
    public class ColorBoard : MonoBehaviour {
        #region Bet Command Keys
        private const string BetCommandAddKey = "add";
        private const string BetCommandRemoveKey = "remove";
        #endregion

        #region Bet Color Keys
        private const string BetColorBlueKey = "blue";
        private const string BetColorGreenKey = "green";
        private const string BetColorOrangeKey = "orange";
        private const string BetColorRedKey = "red";
        private const string BetColorVioletKey = "violet";
        private const string BetColorYellowKey = "yellow";
        #endregion

        #region Twitch Messages
        private const string BetRewardMessage = "Congratulations {0}! you just won x{1} of your bet!";
        private const string BetLooseMessage = "LOL! {0} just lost {1} points.";
        private const string BetSuccessMessage = "{0} bet registered!";
        private const string BetFailedMessage = "Sorry, {0} bet failed! make sure you have enough points before betting.";
        private const string ColorGameTossStartedMessage = "{0} have started the toss!";
        #endregion

        private List<Bet> _bets = new List<Bet>();

        [SerializeField]
        private UserCommandDictionary _commandDictionary = null;

        public void HandleBetRewards(List<Cube.CubeFace> cubeFaces) {
            if (_bets.Count == 0) return;

            Dictionary<Cube.CubeFace, int> rewardMult = new Dictionary<Cube.CubeFace, int>();
            foreach(var face in cubeFaces) {
                if (rewardMult.ContainsKey(face)) {
                    rewardMult[face]++;
                } else {
                    rewardMult.Add(face, 1);
                }
            }

            foreach(var bet in _bets) {
                if (!rewardMult.ContainsKey(bet.ColorFace)) {
                    TwitchComponent.SendTwitchMessage(string.Format(BetLooseMessage, bet.UserName, bet.Amount.ToString()));
                    continue;
                }

                bet.RewardUser(rewardMult[bet.ColorFace]);
                TwitchComponent.SendTwitchMessage(string.Format(BetRewardMessage, bet.UserName, rewardMult[bet.ColorFace]));
            }

            _bets.Clear();
        }

        #region Twitch Command Receiver

        public void TwitchCommandReceived(OnTwitchCommandReceivedEventArgs args) {
            _commandDictionary.HandleCommandInvocation(args.CommandString, args.UserIsBroadcaster, args.UserIsModeretor, args.Username, args.CommandArguments);
        }

        #endregion

        public void OnColorGameToss(string userName, string[] args) {
            GameManager.Instance.TossCubes();
            TwitchComponent.SendTwitchMessage(string.Format(ColorGameTossStartedMessage, userName));
        }

        public void OnBetCommandReceived(string userName, string[] args) {
            if (args.Length != 3) {
                return;
            }

            string betAction = args[0];
            switch(betAction) {
                case BetCommandAddKey: {
                        OnAddBetCommand(userName, GetCubeFace(args[1]), GetAmount(args[2]));
                    } break;
                case BetCommandRemoveKey: {
                        OnRemoveBetCommand(userName, GetCubeFace(args[1]), GetAmount(args[2]));
                    }
                    break;
                default: {
                        Debug.Log("Invalid bet action");
                    } break;
            }
        }

        public void OnAddBetCommand(string userName, Cube.CubeFace cubeFace, int amount) {
            Bet bet = new Bet(userName, cubeFace, amount);
            if (bet.UseUserPoint()) {
                _bets.Add(bet);
                TwitchComponent.SendTwitchMessage(string.Format(BetSuccessMessage, userName));
            } else {
                TwitchComponent.SendTwitchMessage(string.Format(BetFailedMessage, userName));
            }
        }

        public void OnRemoveBetCommand(string userName, Cube.CubeFace cubeFace, int amount) {
            int betIndex = -1;
            for(int i = 0; i < _bets.Count; i++) {
                if (_bets[i].UserName == userName) {
                    betIndex = i;
                    break;
                }
            }

            if (betIndex < 0) {
                return;
            }

            _bets.RemoveAt(betIndex);
        }

        private Cube.CubeFace GetCubeFace(string colorString) {
            switch (colorString) {
                case BetColorBlueKey: return Cube.CubeFace.Blue;
                case BetColorGreenKey: return Cube.CubeFace.Green;
                case BetColorOrangeKey: return Cube.CubeFace.Orange;
                case BetColorRedKey: return Cube.CubeFace.Red;
                case BetColorVioletKey: return Cube.CubeFace.Violet;
                case BetColorYellowKey: return Cube.CubeFace.Yellow;
                default: return Cube.CubeFace.Undefined;
            }
        }

        private int GetAmount(string amountString) {
            int result = 0;
            if (int.TryParse(amountString, out result)) {
                return result;
            }

            return 0;
        } 
    }
}