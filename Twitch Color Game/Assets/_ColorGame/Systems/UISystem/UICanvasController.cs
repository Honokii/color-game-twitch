using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ColorGame.UI {
    [RequireComponent(typeof(Canvas))]
    public class UICanvasController : MonoBehaviour {
        private Canvas _canvas = null;

        [SerializeField]
        private UIControllerId _controllerId = UIControllerId.Undefined;

        [SerializeField]
        private UnityEvent _onInitEvent = new UnityEvent();

        [SerializeField]
        private UnityEvent _onShowCanvasEvent = new UnityEvent();

        [SerializeField]
        private UnityEvent _onHideCanvasEvent = new UnityEvent();

        private void Awake() {
            _canvas = GetComponent<Canvas>();
        }

        public void InitializeController() {
            _onInitEvent?.Invoke();
        }

        public UIControllerId ControllerId {
            get {
                return _controllerId;
            }
        }

        public void ShowCanvas(bool show) {
            _canvas.enabled = show;

            if (show) {
                _onShowCanvasEvent?.Invoke();
            } else {
                _onHideCanvasEvent?.Invoke();
            }
        }
    }
}