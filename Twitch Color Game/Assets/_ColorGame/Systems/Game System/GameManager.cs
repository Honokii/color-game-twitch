using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using WIS.Utils.Helpers;

namespace ColorGame {
    public class GameManager : MonoBehaviour {
        public static GameManager Instance = null;

        private List<Cube> _cubes = new List<Cube>();
        private List<ColorDataId> _currentColorFaces = new List<ColorDataId>();
        private ColorBoard _colorBoard = null;
        private OnCheckColorFaces _checkColorFaces = new OnCheckColorFaces();

        #region Unity Methods

        private void Start() {
            _colorBoard = GetComponent<ColorBoard>();
            _checkColorFaces.AddListener(_colorBoard.HandleBetRewards);
        }

        private void OnDestroy() {
            _checkColorFaces.RemoveListener(_colorBoard.HandleBetRewards);
        }

        private void OnEnable() {
            if (Instance == null) Instance = this;
        }

        private void OnDisable() {
            if (Instance == this) Instance = null;
        }

        #endregion

        public void RegisterCube(Cube cube) {
            if (_cubes.Contains(cube)) {
                return;
            }

            cube.CubeMovementComplete.AddListener(CubeMovementComplete);
            _cubes.Add(cube);
        }

        public void UnregisterCube(Cube cube) {
            if (!_cubes.Contains(cube)) {
                return;
            }

            cube.CubeMovementComplete.RemoveListener(CubeMovementComplete);
            _cubes.Remove(cube);
        }

        public void TossCubes() {
            _currentColorFaces = new List<ColorDataId>();
            if (_cubes.Count == 0) {
                return;
            }

            foreach(var cube in _cubes) {
                cube.Toss();
            }
        }

        private void CubeMovementComplete(ColorDataId colorId) {
            _currentColorFaces.Add(colorId);

            if (_currentColorFaces.Count == _cubes.Count) {
                _checkColorFaces?.Invoke(_currentColorFaces);
            }
        }

        #region Twitch Callbacks

        public void OnTossCommand(string userName, string[] args) {
            TossCubes();
        }

        public void OnBetCommand(string userName, string[] args) {
            if (args.Length != 2) {
                return;
            }

            var colorData = ColorCollection.Instance.GetColorData(args[0]);
            if (colorData.ColorId == ColorDataId.Undefined) {
                return;
            }

            var colorDataId = colorData.ColorId;
            int amount = StringHelper.GetInt(args[1]);
            _colorBoard.AddBet(userName, colorDataId, amount);
        }

        #endregion
    }

    [System.Serializable]
    public class OnCheckColorFaces : UnityEvent<List<ColorDataId>> { }
}