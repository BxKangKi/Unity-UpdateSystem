using Cysharp.Threading.Tasks;
using System.Collections.Generic;

namespace UpdateSystem {
    public static class UpdateManager {
        private static readonly List<IUpdate> updates = new List<IUpdate>(1);
        private static readonly List<IFixedUpdate> fixedUpdates = new List<IFixedUpdate>(1);
        private static readonly List<ILateUpdate> lateUpdates = new List<ILateUpdate>(1);

        private static UniTaskCancellation cancel = new UniTaskCancellation();

        public static void Update() {
            for (int i = 0; i < updates.Count; i++) {
                updates[i].OnUpdate();
            }
        }

        public static void FixedUpdate() {
            for (int i = 0; i < fixedUpdates.Count; i++) {
                fixedUpdates[i].OnFixedUpdate();
            }
        }

        public static void LateUpdate() {
            for (int i = 0; i < lateUpdates.Count; i++) {
                lateUpdates[i].OnLateUpdate();
            }
        }

        public static void OnEnable() {
            cancel.OnEnable();
        }

        public static void OnDisable() {
            cancel.OnDisable();
        }

        public static void OnDestroy() {
            cancel.OnDestroy();
        }


        public static class Add {
            public static void Update(IUpdate value) {
                AddUpdate(value).Forget();
            }

            public static void FixedUpdate(IFixedUpdate value) {
                AddFixedUpdate(value).Forget();
            }

            public static void LateUpdate(ILateUpdate value) {
                AddLateUpdate(value).Forget();
            }

            private static async UniTaskVoid AddUpdate(IUpdate value) {
                await UniTask.NextFrame(cancellationToken: cancel.Disable.Token);
                if (!updates.Contains(value)) {
                    updates.Add(value);
                }
            }

            private static async UniTaskVoid AddFixedUpdate(IFixedUpdate value) {
                await UniTask.NextFrame(cancellationToken: cancel.Disable.Token);
                if (!fixedUpdates.Contains(value)) {
                    fixedUpdates.Add(value);
                }
            }

            private static async UniTaskVoid AddLateUpdate(ILateUpdate value) {
                await UniTask.NextFrame(cancellationToken: cancel.Disable.Token);
                if (!lateUpdates.Contains(value)) {
                    lateUpdates.Add(value);
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
        }
    }
}