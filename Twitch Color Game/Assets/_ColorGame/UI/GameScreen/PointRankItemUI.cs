using UnityEngine;
using Sirenix.OdinInspector;
using TMPro;
using EnhancedUI.EnhancedScroller;

namespace ColorGame.UI {
    public class PointRankItemUI : EnhancedScrollerCellView {
        [SerializeField, Required]
        private TMP_Text _name;

        [SerializeField, Required]
        private TMP_Text _point;

        [SerializeField, Required]
        private TMP_Text _rank;

        public void SetItemTexts(string userName, int points, int rank = 0) {
            _name.text = userName;
            _point.text = points.ToString();
            _rank.text = rank.ToString();
        }
    }
}