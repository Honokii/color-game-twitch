using ColorGame.User;

namespace ColorGame {
    public class Bet {
        public string UserName;
        public Cube.CubeFace ColorFace;
        public int Amount;

        public Bet(string userName, Cube.CubeFace colorFace, int amount) {
            UserName = userName;
            ColorFace = colorFace;
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