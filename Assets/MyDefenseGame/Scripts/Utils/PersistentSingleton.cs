using Unity.VisualScripting;
using UnityEngine;

namespace MyDefenseGame
{
    public class PersistentSingleton<T> : Singleton<T> where T : Singleton<T>
    {
        #region Unity Event Method
        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(gameObject);
        }
        #endregion
    }
}