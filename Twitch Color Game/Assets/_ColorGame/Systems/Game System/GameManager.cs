using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ColorGame {
    public class GameManager : MonoBehaviour {
        public static GameManager Instance = null;

        private List<Cube> _cubes = new List<Cube>();
        private List<Cube.CubeFace> _currentCubeFaces = new List<Cube.CubeFace>();

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
    }

    [System.Serializable]
    public class OnCheckCubeFaces : UnityEvent<List<Cube.CubeFace>> { }
}