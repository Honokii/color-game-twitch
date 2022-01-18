using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using WIS.TwitchComponent.Events;
using WIS.TwitchComponent.Commands;

namespace ColorGame {
    public class GameManager : MonoBehaviour {
        public static GameManager Instance = null;

        private List<Cube> _cubes = new List<Cube>();
        private List<Cube.CubeFace> _currentCubeFaces = new List<Cube.CubeFace>();

        [SerializeField]
        private ColorBoard _colorBoard = null;

        [SerializeField]
        private OnCheckCubeFaces _checkCubeFaces = null;

        #region Unity Methods

        private void OnEnable() {
            if (Instance == null) {
                Instance = this;
            }
        }

        private void OnDisable() {
            if (Instance == this) {
                Instance = null;
            }
        }

        private void Update() {
            if (Input.GetKeyDown(KeyCode.Space)) {
                TossCubes();
            }
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
            _currentCubeFaces = new List<Cube.CubeFace>();
            if (_cubes.Count == 0) {
                return;
            }

            foreach(var cube in _cubes) {
                cube.Toss();
            }
        }

        private void CubeMovementComplete(Cube.CubeFace cubeFace) {
            _currentCubeFaces.Add(cubeFace);

            if (_currentCubeFaces.Count == _cubes.Count) {
                _checkCubeFaces.Invoke(_currentCubeFaces);
            }
        }

        #region Twitch Callbacks

        public void OnTossCommand(string userName, string[] args) {
            TossCubes();
        }

        //bet red 50
        public void OnBetCommand(string userName, string[] args) {
            if (args.Length != 2) {
                return;
            }

            var face = ColorBoard.GetCubeFace(args[0]);
            int amount = ColorBoard.GetAmount(args[1]);
            _colorBoard.OnAddBetCommand(userName, face, amount);
        }

        #endregion
    }

    [System.Serializable]
    public class OnCheckCubeFaces : UnityEvent<List<Cube.CubeFace>> { }
}