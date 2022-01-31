using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnhancedUI.EnhancedScroller;

namespace ColorGame.UI {
    public class BetColorUI : MonoBehaviour, IEnhancedScrollerDelegate {
        [SerializeField]
        private EnhancedScroller _scroller;

        public ColorDataId ColorId;

        public List<Bet> Bets;

        public float CellSize = 30f;

        public EnhancedScrollerCellView CellPrefab;

        public void InitializeUI() {
            _scroller.Delegate = this;
        }

        public void ReloadUI() {
            _scroller.ReloadData();
        }

        public void ResetBets() {
            Bets = new List<Bet>();
        }

        #region Scroller Delegate

        public int GetNumberOfCells(EnhancedScroller scroller) {
            return Bets.Count;
        }

        public float GetCellViewSize(EnhancedScroller scroller, int dataIndex) {
            return CellSize;
        }

        public EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex) {
            BetUI cell = scroller.GetCellView(CellPrefab) as BetUI;
            var bet = Bets[dataIndex];
            //cell.SetBetUI(bet.UserName, bet.Amount);
            return cell;
        }

        #endregion
    }
}