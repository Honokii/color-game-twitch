using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ColorGame.UI {

    [System.Serializable]
    public class UICollection {
        public List<UICanvasController> Collection = new List<UICanvasController>();
        private Dictionary<UIControllerId, UICanvasController> _initializedCollection = new Dictionary<UIControllerId, UICanvasController>();

        public void InitializeCollection() {
            if (Collection.Count < 0) {
                return;
            }

            _initializedCollection.Clear();

            foreach(var collection in Collection) {
                collection.InitializeController();
                _initializedCollection.Add(collection.ControllerId, collection);
            }
        }
    }
}