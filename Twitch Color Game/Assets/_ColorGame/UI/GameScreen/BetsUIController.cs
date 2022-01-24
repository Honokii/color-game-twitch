using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnhancedUI.EnhancedScroller;

namespace ColorGame.UI {
    public class BetsUIController : MonoBehaviour, IEnhancedScrollerDelegate {

        [SerializeField]
        private EnhancedScroller _scroller;

        [SerializeField]
        private EnhancedScrollerCellView _cellPrefab;

        [SerializeField]
        private float _cellSize = 200;

        private List<Bet> _bets = new List<Bet>();

        #region Unity Methods
        private void Start() {
            _scroller.Delegate = this;
            _scroller.ReloadData();
        }
        #endregion

        #region Scroller Delegate
        public EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex) {
            BetUI cell = scroller.GetCellView(_cellPrefab) as BetUI;

            return cell;
        }

        public float GetCellViewSize(EnhancedScroller scroller, int dataIndex) {
            return _cellSize;
        }

        public int GetNumberOfCells(EnhancedScroller scroller) {
            return _bets == null ? 0 : _bets.Count;
        }
        #endregion

    }
}