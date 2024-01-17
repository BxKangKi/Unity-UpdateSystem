using UnityEngine;

namespace UpdateSystem {
    public static class DeltaTime {
        public static float Value {
            get { return Time.deltaTime * TimeScale.Value; }
        }
    }
}