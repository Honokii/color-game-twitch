using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace ColorGame.UI {
    [System.Serializable]
    public enum UIControllerId {
        Undefined = 0,
        MainGameScreen = 1
    }

    public class UIManager : MonoBehaviour {

        [Required]
        public UICollection Collection;

        private void Start() {
            Collection.InitializeCollection();
        }
    }
}