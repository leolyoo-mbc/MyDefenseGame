using UnityEngine;

namespace MyDefenseGame
{
    [CreateAssetMenu(fileName = "New Tower Blueprint", menuName = "TowerDefense/Tower Blueprint")]
    public class TowerBlueprint : ScriptableObject
    {

        #region Variables
        [Header("기본 타워 정보")]
        [SerializeField] private GameObject _prefab;
        public GameObject Prefab => _prefab;

        [SerializeField] private int _cost;
        public int Cost => _cost;

        [Header("업그레이드 타워 정보")]
        [SerializeField] private GameObject _upgradedPrefab;
        public GameObject UpgradedPrefab => _upgradedPrefab;

        [SerializeField] private int _upgradeCost;
        public int UpgradeCost => _upgradeCost;
        #endregion

        #region Custom Method
        // [핵심] 외부(타일)에서 현재 상태를 알려주면, 그에 맞춰 환불 금액을 계산해 반환합니다.
        public int GetSellPrice(bool isUpgraded)
        {
            int totalSpent = Cost; // 기본 건설 비용

            if (isUpgraded)
            {
                totalSpent += UpgradeCost; // 업그레이드했다면 그 비용도 합산
            }

            // 총 투자한 금액의 절반(50%)을 반환합니다.
            return totalSpent / 2;
        }
        #endregion
    }
}
