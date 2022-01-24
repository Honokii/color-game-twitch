using System.Collections.Generic;
using UnityEngine;
using EnhancedUI.EnhancedScroller;
using Sirenix.OdinInspector;
using ColorGame.User;

namespace ColorGame.UI {
    public class PointRankUIController : MonoBehaviour, IEnhancedScrollerDelegate {

        [SerializeField, Required]
        private EnhancedScrollerCellView _cellPrefab;

        [SerializeField]
        private float _cellSize = 80;

        [SerializeField, Required]
        private EnhancedScroller _scroller;

        private List<User.User> _users;

        #region Unity Methods

        private void Start() {
            _scroller.Delegate = this;
            _scroller.ReloadData();
        }

        #endregion

        public void UpdateRanksUIWithUsers(List<User.User> users) {
            _users = users;
            _scroller.ReloadData();
        }

        #region EnhancedScrollerDelegate
        public EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex) {
            PointRankItemUI cell = scroller.GetCellView(_cellPrefab) as PointRankItemUI;
            var user = _users[dataIndex];
            cell.SetItemTexts(user.UserName, user.UserPoints, dataIndex + 1);
            return cell;
        }

        public float GetCellViewSize(EnhancedScroller scroller, int dataIndex) {
            return _cellSize;
        }

        public int GetNumberOfCells(EnhancedScroller scroller) {
            return _users == null ? 0 : _users.Count;
        }
        #endregion

        #region User Manager Events

        public void OnUserSaveDataUpdatedEvent(UserSaveDataUpdatedEventArgs args) {
            UpdateRanksUIWithUsers(args.Users);
        }

        #endregion
    }
}