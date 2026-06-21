using UnityEngine;

namespace MyDefenseGame
{
    [System.Serializable]
    public struct SpawnGroup
    {
        [SerializeField] private EnemyController _enemyPrefab;
        [SerializeField] private int _spawnCount;
        [SerializeField] private float _spawnInterval;
        [SerializeField] private float _delayNextSpawnGroup;

        public readonly EnemyController EnemyPrefab => _enemyPrefab;
        public readonly int SpawnCount => _spawnCount;
        public readonly float SpawnInterval => _spawnInterval;
        public readonly float DelayNextSpawnGroup => _delayNextSpawnGroup;
    }

    [CreateAssetMenu(fileName = "New Wave Data", menuName = "TowerDefense/Wave Data")]
    public class Wave : ScriptableObject
    {
        #region Variables
        [SerializeField] private SpawnGroup[] _spawnGroups;

        // 배열 역시 외부에서는 읽기만 가능하도록 프로퍼티로 제공합니다.
        public SpawnGroup[] SpawnGroups => _spawnGroups;
        #endregion
    }
}