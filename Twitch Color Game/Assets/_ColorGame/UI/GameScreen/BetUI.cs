using UnityEngine;
using UnityEngine.UI;
using EnhancedUI.EnhancedScroller;
using TMPro;

namespace ColorGame.UI {
    public class BetUI :  EnhancedScrollerCellView{
        [SerializeField]
        private Image _betUIBG;

        [SerializeField]
        private TMP_Text _userName;

        [SerializeField]
        private TMP_Text _betAmount;

        public void SetBetUI(string userName, int betAmount, Color betColor) {
            _userName.text = userName;
            _betAmount.text = betAmount.ToString();
            _betUIBG.color = betColor;
        }
    }
}