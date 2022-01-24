using ColorGame.User;

namespace ColorGame {
    public class Bet {
        public string UserName;
        public ColorDataId ColorFaceId;
        public int Amount;

        public Bet(string userName, ColorDataId colorFaceId, int amount) {
            UserName = userName;
            ColorFaceId = colorFaceId;
            Amount = amount;
        }

        public bool UseUserPoint() {
            return UserManager.Instance.UsePoints(UserName, Amount);
        }

        public void RewardUser(int rewardMultiplier) {
            UserManager.Instance.AddPoints(UserName, Amount * rewardMultiplier);
        }
    }
}