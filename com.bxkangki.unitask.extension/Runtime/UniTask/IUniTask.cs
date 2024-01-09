namespace Cysharp.Threading.Tasks {
    public interface IUniTask {
        void OnEnable();
        void OnDisable();
        void OnDestroy();
    }
}