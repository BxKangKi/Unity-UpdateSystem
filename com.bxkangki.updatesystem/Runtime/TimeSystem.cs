using UnityEngine;

namespace UpdateSystem {
    public static class TimeSystem {
        public static float TimeScale = 1f;
        public static int Tick = 250;
        public static float DeltaTime {
            get { return Time.deltaTime * TimeScale; }
        }
    }
}