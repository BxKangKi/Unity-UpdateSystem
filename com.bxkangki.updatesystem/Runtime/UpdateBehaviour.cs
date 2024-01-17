using UnityEngine;

namespace UpdateSystem
{
    public class UpdateBehaviour : MonoBehaviour, IUpdateSystem
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        public void OnEnable()
        {
            UpdateManager.OnEnable();
        }

        public void OnDisable()
        {
            UpdateManager.OnDisable();
        }

        private void OnDestroy()
        {
            UpdateManager.OnDestroy();
        }

        // Update is called once per frame
        private void Update()
        {
            UpdateManager.Update();
        }

        private void FixedUpdate()
        {
            UpdateManager.FixedUpdate();
        }

        private void LateUpdate()
        {
            UpdateManager.LateUpdate();
        }
    }
}