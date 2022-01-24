using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

namespace ColorGame {
    public class Cube : MonoBehaviour {

        [SerializeField]
        private float _tossForce = 20f;

        [SerializeField]
        private float _rotateForce = 20f;

        [SerializeField, TableList]
        private CubeFace[] _faces;

        [SerializeField]
        private CubeFace _defaultFace;

        private Rigidbody _rigidbody;

        private Vector3 _lastPosition = Vector3.zero;

        private float _movementThreshold = 0.01f;

        private bool _isMoving = false;

        private RaycastHit _hit;
        private int _floorLayerMask = 1 << 10;
        private float _checkDistance = 1f;

        public CubeEvents GetCubeEvents = new CubeEvents();

        public OnCubeMovementComplete CubeMovementComplete = new OnCubeMovementComplete();

        #region Unity Methods

        private void Start() {
            GameManager.Instance?.RegisterCube(this);
            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.maxAngularVelocity = 100f;
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
                CubeMovementComplete?.Invoke(GetTopFaceColorData().ColorId);
            }
        }

        public void Toss() {
            float rForce = Random.Range(_tossForce, _tossForce * 1.5f);
            _rigidbody.AddForce(Vector3.up * rForce);

            float x = Random.Range(_rotateForce, _rotateForce * 3f);
            float y = Random.Range(_rotateForce, _rotateForce * 3f);
            float z = Random.Range(_rotateForce, _rotateForce * 3f);
            Vector3 torq = new Vector3(x, y, z);
            _rigidbody.AddTorque(torq);
        }

        public ColorData GetTopFaceColorData() {
            var result = _defaultFace.FaceColorData;

            if (_faces.Length == 0) {
                return result;
            }

            foreach(var face in _faces) {
                if (IsDirectionSelected(face.FaceDirection, _hit, _checkDistance, _floorLayerMask)) {
                    result = face.FaceColorData;
                    break;
                }
            }

            return result;
        }

        private bool IsDirectionSelected(Vector3 direction, RaycastHit hit, float checkDistance, int layerMask) {
            return Physics.Raycast(transform.position, transform.TransformDirection(direction), out hit, checkDistance, layerMask);
        }
    }

    public class OnCubeMovementComplete : UnityEvent<ColorDataId> { }
}