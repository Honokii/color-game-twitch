using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using WIS.TwitchComponent;
using ColorGame.User;

namespace ColorGame {
    public class ColorBoard : MonoBehaviour {
        #region Twitch Messages
        private const string BetRewardMessage = "Congratulations {0}! You just won x{1} of your bet! Your current points : {2}";
        private const string BetLooseMessage = "LOL! {0} just lost {1} points. Your current points : {2}";
        private const string BetReturnedMessage = "Nice! All bets on {0}, {1}, {2}, are all returned.";
        private const string BetDoubledMessage = "Great! All bets on {0} are doubled! And bets on {1} are returned.";
        private const string BetTrippledMessage = "Congratulations! Bets on {0} are trippled!";
        private const string BetAllLooseMessage = "LOL! No one got the colors! Get better next time guys.";
        private const string BetSuccessMessage = "{0} placed a bet: {1} {2}";
        private const string BetFailedMessage = "Sorry, {0} bet failed! make sure that your bet value is correct and you have enough points.";
        #endregion

        private List<Bet> _bets = new List<Bet>();

        [SerializeField]
        private OnBetListUpdatedEvent _betListUpdatedEvent = null;

        public void HandleBetRewards(List<ColorDataId> colorDataIds) {
            if (_bets.Count == 0) return;

            Dictionary<ColorDataId, int> rewardMultiplier = GenerateRewardMultiplier(colorDataIds);
            int rewardedBets = 0;
            foreach(var bet in _bets) {
                if (!rewardMultiplier.ContainsKey(bet.ColorFaceId)) {
                    //TwitchComponent.SendTwitchMessage(string.Format(BetLooseMessage, bet.UserName, bet.Amount, GetUserPoint(bet.UserName)));
                    continue;
                }

                rewardedBets++;
                bet.RewardUser(rewardMultiplier[bet.ColorFaceId]);
                //TwitchComponent.SendTwitchMessage(string.Format(BetRewardMessage, bet.UserName, rewardMultiplier[bet.ColorFaceId], GetUserPoint(bet.UserName)));
            }

            _bets.Clear();
            BetListUpdated();

            if (rewardedBets == 0) {
                TwitchComponent.SendTwitchMessage(BetAllLooseMessage);
                return;
            }

            string message = "";
            if (rewardMultiplier.Count == 3) {
                message = GenerateMessageStringForReturnedBets(rewardMultiplier.Keys.ToList());
            } else if (rewardMultiplier.Count == 2) {
                message = GenerateMessageStringForDoubleRewardBets(rewardMultiplier);
            } else if (rewardMultiplier.Count == 1) {
                var colorData = ColorCollection.Instance.GetColorData(rewardMultiplier.First().Key);
                message = string.Format(BetTrippledMessage, colorData.ColorName);
            }

            TwitchComponent.SendTwitchMessage(message);
        }

        private string GenerateMessageStringForDoubleRewardBets(Dictionary<ColorDataId, int> rewardMult) {
            ColorData doubleReward = null;
            ColorData returnedReward = null;

            foreach(var pair in rewardMult) {
                if (pair.Value == 2) {
                    doubleReward = ColorCollection.Instance.GetColorData(pair.Key);
                } else {
                    returnedReward = ColorCollection.Instance.GetColorData(pair.Key);
                }
            }

            return string.Format(BetDoubledMessage, doubleReward.ColorName, returnedReward.ColorName);
        }

        private string GenerateMessageStringForReturnedBets(List<ColorDataId> colorIds) {
            List<ColorData> colorDatas = new List<ColorData>();
            foreach(var colorId in colorIds) {
                colorDatas.Add(ColorCollection.Instance.GetColorData(colorId));
            }

            if (colorDatas.Count != 3) {
                return "All bets on the winning colors are returned!";
            }

            return string.Format(BetReturnedMessage, colorDatas[0].ColorName, colorDatas[1].ColorName, colorDatas[2].ColorName);
        }

        private Dictionary<ColorDataId, int> GenerateRewardMultiplier(List<ColorDataId> colorDataIds) {
            Dictionary<ColorDataId, int> rewardMultiplier = new Dictionary<ColorDataId, int>();
            foreach (var colorId in colorDataIds) {
                if (rewardMultiplier.ContainsKey(colorId)) {
                    rewardMultiplier[colorId]++;
                } else {
                    rewardMultiplier.Add(colorId, 1);
                }
            }
            return rewardMultiplier;
        }

        private int GetUserPoint(string userName) {
            return UserManager.Instance.GetUserPoint(userName);
        }

        public void AddBet(string userName, ColorDataId colorDataId, int amount) {
            if (amount <= 0) {
                TwitchComponent.SendTwitchMessage(string.Format(BetFailedMessage, userName));
                return;
            }

            Bet bet = new Bet(userName, colorDataId, amount);
            if (!bet.UseUserPoint()) {
                TwitchComponent.SendTwitchMessage(string.Format(BetFailedMessage, userName));
                return;
            }

            _bets.Add(bet);
            string betColorName = ColorCollection.Instance.GetColorData(colorDataId).ColorName;
            TwitchComponent.SendTwitchMessage(string.Format(BetSuccessMessage, userName, betColorName, amount));
            BetListUpdated();
        }

        private void BetListUpdated() {
            _betListUpdatedEvent?.Raise(new OnBetListUpdatedEventArgs() { 
                Bets = _bets
            });
        }
    }
}