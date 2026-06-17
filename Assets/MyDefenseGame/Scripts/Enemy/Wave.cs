using UnityEngine;

namespace MyDefenseGame
{
    [System.Serializable]
    public struct Spawn
    {
        public EnemyController enemyPrefab;
        public float delayNextSpawn;
    }

    [System.Serializable]
    public class Wave
    {
        #region Variables
        public Spawn[] spawns;
        #endregion
    }
}
