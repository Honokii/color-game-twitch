using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using EnhancedUI.EnhancedScroller;

namespace ColorGame.UI {
    public class BetsUIController : MonoBehaviour, IEnhancedScrollerDelegate {

        [SerializeField]
        private EnhancedScroller _scroller;

        [SerializeField]
        private float _cellSize = 30f;

        [SerializeField]
        private EnhancedScrollerCellView _cellPrefab;

        private List<Bet> _bets = new List<Bet>();

        #region Unity Methods
        private void Awake() {
            _scroller.Delegate = this;
            _scroller.ReloadData();
        }

        #endregion

        public void UpdateBets(List<Bet> bets) {
            _bets = bets;
            _scroller.ReloadData();
        }

        #region Bet Events

        public void BetListUpdated(OnBetListUpdatedEventArgs args) {
            if (args.Bets == null) {
                UpdateBets(new List<Bet>());
                return;
            }

            UpdateBets(args.Bets);
        }

        #endregion

        #region Enhanced Scroller Delegate
        public EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex) {
            BetUI cell = scroller.GetCellView(_cellPrefab) as BetUI;
            var bet = _bets[dataIndex];
            var betColor = ColorCollection.Instance.GetColorData(bet.ColorFaceId);
            cell.SetBetUI(bet.UserName, bet.Amount, betColor.ColorValue);
            return cell;
        }

        public float GetCellViewSize(EnhancedScroller scroller, int dataIndex) {
            return _cellSize;
        }

        public int GetNumberOfCells(EnhancedScroller scroller) {
            return _bets.Count;
        }

        #endregion
    }
}