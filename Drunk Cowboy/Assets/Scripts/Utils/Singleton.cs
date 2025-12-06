using UnityEngine;

namespace cowboy.utils
{
    public class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        public static T Instance;
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = (T)this;
                DontDestroyOnLoad(gameObject);
            }
            else if (Instance != this)
            {
                Destroy(this.gameObject); // Prevent duplicate instances
            }
        }
    }
}
