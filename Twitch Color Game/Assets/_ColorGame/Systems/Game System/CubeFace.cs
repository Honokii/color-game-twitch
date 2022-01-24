using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ColorGame {
    [CreateAssetMenu(fileName = "CubeFace_", menuName = "Color Game/Cube/Face")]
    public class CubeFace : ScriptableObject {
        public Vector3 FaceDirection;
        public ColorData FaceColorData;
    }
}