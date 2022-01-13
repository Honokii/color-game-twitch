using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ColorGame {
    public class GameManager : MonoBehaviour {
        public static GameManager Instance = null;

        private List<Cube> _cubes = new List<Cube>();

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

            _cubes.Add(cube);
        }

        public void UnregisterCube(Cube cube) {
            if (!_cubes.Contains(cube)) {
                return;
            }

            _cubes.Remove(cube);
        }

        private void TossCubes() {
            if (_cubes.Count == 0) {
                return;
            }

            foreach(var cube in _cubes) {
                cube.Toss();
            }
        }
    }
}