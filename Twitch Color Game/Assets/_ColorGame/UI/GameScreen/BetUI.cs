using UnityEngine;
using UnityEngine.UI;
using EnhancedUI.EnhancedScroller;
using TMPro;

namespace ColorGame.UI {
    public class BetUI :  EnhancedScrollerCellView{
        [SerializeField]
        private TMP_Text _userName;

        [SerializeField]
        private TMP_Text _betAmount;

        [SerializeField]
        private Image _betColor;

        public void SetBetUI(string userName, int betAmount, Color betColor) {
            _userName.text = userName;
            _betAmount.text = betAmount.ToString();
            _betColor.color = betColor;
        }
    }
}