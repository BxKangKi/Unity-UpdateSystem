using System.Threading;

namespace Cysharp.Threading.Tasks {
    public class UniTaskCancellation : IUniTask
    {
        public static CancellationTokenSource GameExit = new CancellationTokenSource();
        public static void OnGameExit() {
            GameExit.Cancel();
            GameExit.Dispose();
        }

        public CancellationTokenSource Disable = new CancellationTokenSource();
        public CancellationTokenSource Destroy = new CancellationTokenSource();

        public void StopAllDisable() {
            OnDisable();
            OnEnable();
        }

        public void OnEnable()
        {
            if (Disable != null)
            {
                Disable.Dispose();
            }
            Disable = new CancellationTokenSource();
        }

        public void OnDisable()
        {
            Disable.Cancel();
        }

        public void OnDestroy()
        {
            Destroy.Cancel();
            Destroy.Dispose();
        }
    }
}