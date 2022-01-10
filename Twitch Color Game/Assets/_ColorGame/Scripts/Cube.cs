using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ColorGame {
    public class Cube : MonoBehaviour {

        public enum CubeFace {
            Undefined = 0,
            Blue = 1,
            Green = 2,
            Orange = 3,
            Red = 4,
            Violet = 5,
            Yellow = 6
        }

        [SerializeField]
        private float _tossForce = 20f;

        [SerializeField]
        private CubeFace[] _faceColor = new CubeFace[6];

        private Rigidbody _rigidbody;

        private Vector3 _lastPosition = Vector3.zero;

        private float _movementThreshold = 0.01f;

        private bool _isMoving = false;

        public CubeEvents GetCubeEvents = new CubeEvents();

        #region Unity Methods

        private void Start() {
            GameManager.Instance?.RegisterCube(this);
            _rigidbody = GetComponent<Rigidbody>();
            _lastPosition = transform.position;
        }

        private void OnDestroy() {
            GameManager.Instance?.UnregisterCube(this);
        }

        private void LateUpdate() {
            HandleCubeMovement();
        }

        #endregion

        private void HandleCubeMovement() {
            var currentPos = transform.position;
            if (Vector3.Distance(currentPos, _lastPosition) >= _movementThreshold) {
                _lastPosition = currentPos;
                _isMoving = true;
            }

            if (_isMoving && _rigidbody.velocity.magnitude == 0) {
                GetCubeEvents.onCubeTossCompleted?.Invoke(this);
                _isMoving = false;
                CurrentTopFace();
            }
        }

        public void Toss() {
            float rForce = Random.Range(_tossForce, _tossForce * 1.5f);
            _rigidbody.AddForce(Vector3.up * rForce);

            float x = Random.Range(_tossForce, _tossForce * 1.5f);
            float y = Random.Range(_tossForce, _tossForce * 1.5f);
            float z = Random.Range(_tossForce, _tossForce * 1.5f);
            Vector3 torq = new Vector3(x, y, z);
            _rigidbody.AddTorque(torq);
        }

        public CubeFace CurrentTopFace() {
            CubeFace result = CubeFace.Undefined;

            int layerMask = 1 << 10;
            RaycastHit hit;

            //up
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.up), out hit, 2f, layerMask)) {
                result = CubeFace.Red;
                Debug.Log("Red");
            }
            //down
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 2f, layerMask)) {
                result = CubeFace.Orange;
                Debug.Log("Orange");
            }
            //forward
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 2f, layerMask)) {
                Debug.Log("Blue");
            }
            //backward
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.back), out hit, 2f, layerMask)) {
                Debug.Log("Green");
                result = CubeFace.Green;
            }
            //left
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.left), out hit, 2f, layerMask)) {
                Debug.Log("Yellow");
            }
            //right
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), out hit, 2f, layerMask)) {
                result = CubeFace.Violet;
                Debug.Log("Violet");
            }

            return result;
        }
    }
}