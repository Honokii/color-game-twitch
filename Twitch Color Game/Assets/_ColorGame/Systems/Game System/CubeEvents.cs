using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ColorGame {
    public class CubeEvents {
        public OnCubeTossCompleted onCubeTossCompleted;
    }

    public class OnCubeTossCompleted : UnityEvent<Cube> { }
}