using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace ColorGame {
    public class ColorCollection : MonoBehaviour {
        [SerializeField, TableList]
        private ColorData[] _collection;

        public int Count {
            get {
                return _collection.Length;
            }
        }

        public ColorData GetColorData(ColorDataId dataId) {
            if (_collection == null || Count == 0) {
                return null;
            }

            ColorData result = null;
            foreach (var item in _collection) {
                if (item.ColorId == dataId) {
                    result = item;
                    break;
                }
            }

            return result;
        }

        public ColorData GetColorData(string colorName) {
            if (_collection == null || Count == 0) {
                return null;
            }

            ColorData result = null;
            foreach (var item in _collection) {
                if (item.ColorName == colorName) {
                    result = item;
                    break;
                }
            }

            return result;
        }
    }

    [System.Serializable]
    public enum ColorDataId {
        Undefined = 0,
        Blue = 1,
        Green = 2,
        Orange = 3,
        Red = 4,
        Violet = 5,
        Yellow = 6
    }
}