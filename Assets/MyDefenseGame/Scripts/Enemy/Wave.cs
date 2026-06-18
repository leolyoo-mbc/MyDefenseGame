using UnityEngine;

namespace MyDefenseGame
{
    [System.Serializable]
    public struct Spawn
    {
        public GameObject enemyPrefab;
        public float delayNextSpawn;
    }

    [System.Serializable]
    [CreateAssetMenu(fileName = "New Wave Data", menuName = "TowerDefense/Wave Data")]
    public class Wave : ScriptableObject
    {
        #region Variables
        public Spawn[] spawns;
        #endregion
    }
}
