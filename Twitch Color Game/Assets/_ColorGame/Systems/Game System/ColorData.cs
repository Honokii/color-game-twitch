using UnityEngine;
using Sirenix.OdinInspector;

namespace ColorGame {
    [CreateAssetMenu(fileName = "new ColorData", menuName = "Color Game/Color Data")]
    public class ColorData : ScriptableObject{

        public ColorDataId ColorId;

        public string ColorName;

        [ColorPalette("CG Colors")]
        public Color ColorValue;
    }
}