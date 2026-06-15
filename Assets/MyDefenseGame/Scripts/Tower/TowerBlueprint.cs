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

        #region Custom Method
        // [핵심] 외부(타일)에서 현재 상태를 알려주면, 그에 맞춰 환불 금액을 계산해 반환합니다.
        public int GetSellPrice(bool isUpgraded)
        {
            int totalSpent = cost; // 기본 건설 비용

            if (isUpgraded)
            {
                totalSpent += upgradeCost; // 업그레이드했다면 그 비용도 합산
            }

            // 총 투자한 금액의 절반(50%)을 반환합니다.
            return totalSpent / 2;
        }
        #endregion
    }
}
