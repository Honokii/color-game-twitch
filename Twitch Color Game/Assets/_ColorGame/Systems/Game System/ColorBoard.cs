using System.Collections.Generic;
using UnityEngine;
using WIS.TwitchComponent;
using ColorGame.User;

namespace ColorGame {
    public class ColorBoard : MonoBehaviour {
        #region Twitch Messages
        private const string BetRewardMessage = "Congratulations {0}! You just won x{1} of your bet! Your current points : {2}";
        private const string BetLooseMessage = "LOL! {0} just lost {1} points. Your current points : {2}";
        private const string BetSuccessMessage = "{0} placed a bet: {1} {2}";
        private const string BetFailedMessage = "Sorry, {0} bet failed! make sure you have enough points before betting.";
        #endregion

        private List<Bet> _bets = new List<Bet>();

        public void HandleBetRewards(List<ColorDataId> colorDataIds) {
            if (_bets.Count == 0) return;

            Dictionary<ColorDataId, int> rewardMultiplier = GenerateRewardMultiplier(colorDataIds);
            foreach(var bet in _bets) {
                if (!rewardMultiplier.ContainsKey(bet.ColorFaceId)) {
                    TwitchComponent.SendTwitchMessage(string.Format(BetLooseMessage, bet.UserName, bet.Amount, GetUserPoint(bet.UserName)));
                    continue;
                }

                bet.RewardUser(rewardMultiplier[bet.ColorFaceId]);
                TwitchComponent.SendTwitchMessage(string.Format(BetRewardMessage, bet.UserName, rewardMultiplier[bet.ColorFaceId], GetUserPoint(bet.UserName)));
            }

            _bets.Clear();
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
            Bet bet = new Bet(userName, colorDataId, amount);
            if (bet.UseUserPoint()) {
                _bets.Add(bet);

                string betColorName = ColorCollection.Instance.GetColorData(colorDataId).ColorName;
                TwitchComponent.SendTwitchMessage(string.Format(BetSuccessMessage, userName, betColorName, amount));
            } else {
                TwitchComponent.SendTwitchMessage(string.Format(BetFailedMessage, userName));
            }
        }
    }
}