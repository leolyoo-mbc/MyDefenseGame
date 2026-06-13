using UnityEngine;

namespace MyDefenseGame
{
    [System.Serializable]
    public class TowerBlueprint
    {

        #region Variables
        [Header("기본 타워 정보")]
        public GameObject prefab;
        public int cost;

        [Header("업그레이드 타워 정보")]
        public GameObject upgradedPrefab;

        public int upgradeCost;
        #endregion
    }
}
