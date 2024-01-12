using Cysharp.Threading.Tasks;
using System.Collections.Generic;

namespace UpdateSystem {
    public static class UpdateManager {
        private static readonly List<IUpdate> updates = new List<IUpdate>(1);
        private static readonly List<IFixedUpdate> fixedUpdates = new List<IFixedUpdate>(1);
        private static readonly List<ILateUpdate> lateUpdates = new List<ILateUpdate>(1);
        private static readonly List<IUnscaleUpdate> unscaleUpdates = new List<IUnscaleUpdate>(1);
        private static readonly List<IUnscaleLateUpdate> unscaleLateUpdates = new List<IUnscaleLateUpdate>(1);
        private static readonly List<ITickUpdate> tickUpdates = new List<ITickUpdate>(1);

        private static UniTaskCancellation cancel = new UniTaskCancellation();

        public static void Update() {
            if (TimeSystem.TimeScale > 0f) {
                for (int i = 0; i < updates.Count; i++) {
                    updates[i].OnUpdate();
                }
            }
            for (int i = 0; i < unscaleUpdates.Count; i++) {
                unscaleUpdates[i].OnUnscaleUpdate();
            }
        }

        public static void FixedUpdate() {
            for (int i = 0; i < fixedUpdates.Count; i++) {
                fixedUpdates[i].OnFixedUpdate();
            }
        }

        public static void LateUpdate() {
            if (TimeSystem.TimeScale > 0f) {
                for (int i = 0; i < lateUpdates.Count; i++) {
                    lateUpdates[i].OnLateUpdate();
                }
            }
            for (int i = 0; i < unscaleLateUpdates.Count; i++) {
                unscaleLateUpdates[i].OnUnscaleLateUpdate();
            }
        }


        private static async UniTaskVoid TickUpdate() {
            for (;;) {
                for (int i = 0; i < tickUpdates.Count; i++) {
                    tickUpdates[i].OnTickUpdate();
                }
                await UniTask.Delay(TimeSystem.Tick, cancellationToken: cancel.Disable.Token);
            }
        }


        public static void OnEnable() {
            cancel.OnEnable();
            TickUpdate().Forget();
        }

        public static void OnDisable() {
            cancel.OnDisable();
        }

        public static void OnDestroy() {
            cancel.OnDestroy();
        }


        public static class Add {
            public static void Update(IUpdate value) {
                AddUpdate<IUpdate>(updates, value).Forget();
            }

            public static void FixedUpdate(IFixedUpdate value) {
                AddUpdate<IFixedUpdate>(fixedUpdates, value).Forget();
            }

            public static void LateUpdate(ILateUpdate value) {
                AddUpdate<ILateUpdate>(lateUpdates, value).Forget();
            }

            public static void UnscaleUpdate(IUnscaleUpdate value) {
                AddUpdate<IUnscaleUpdate>(unscaleUpdates, value).Forget();
            }

            public static void UnscaleLateUpdate(IUnscaleLateUpdate value) {
                AddUpdate<IUnscaleLateUpdate>(unscaleLateUpdates, value).Forget();
            }

            public static void TickUpdate(ITickUpdate value) {
                AddUpdate<ITickUpdate>(tickUpdates, value).Forget();
            }

            private static async UniTaskVoid AddUpdate<T>(List<T> list, T value) {
                await UniTask.NextFrame(cancellationToken: cancel.Disable.Token);
                if (!list.Contains(value)) {
                    list.Add(value);
                }
            }
        }


        public static class Remove {
            public static void Update(IUpdate value) {
                if (updates.Contains(value)) {
                    updates.Remove(value);
                }
            }

            public static void FixedUpdate(IFixedUpdate value) {
                if (fixedUpdates.Contains(value)) {
                    fixedUpdates.Remove(value);
                }
            }

            public static void LateUpdate(ILateUpdate value) {
                if (lateUpdates.Contains(value)) {
                    lateUpdates.Remove(value);
                }
            }

            public static void UnscaleUpdate(IUnscaleUpdate value) {
                if (unscaleUpdates.Contains(value)) {
                    unscaleUpdates.Remove(value);
                }
            }

            public static void UnscaleLateUpdate(IUnscaleLateUpdate value) {
                if (unscaleLateUpdates.Contains(value)) {
                    unscaleLateUpdates.Remove(value);
                }
            }

            public static void TickUpdate(ITickUpdate value) {
                if (tickUpdates.Contains(value)) {
                    tickUpdates.Remove(value);
                }
            }
        }
    }
}