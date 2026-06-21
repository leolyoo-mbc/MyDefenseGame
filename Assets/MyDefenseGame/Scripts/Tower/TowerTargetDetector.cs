using UnityEngine;

namespace MyDefenseGame
{
    /// <summary>
    /// 타워의 공격 범위 내의 타겟을 감지하는 클래스
    /// </summary>
    public class TowerTargetDetector : MonoBehaviour
    {
        #region Variables
        [Tooltip("타워의 공격 범위")]
        [SerializeField] private float _attackRange = 7f;

        [Tooltip("감지할 적의 물리 레이어 마스크")]
        [SerializeField] private LayerMask _enemyLayer;

        private GameObject _currentTarget;

        [Tooltip("적 감지 주기")]
        [SerializeField] private float _searchInterval = 0.1f;

        private float _searchCooldown = 0f;
        #endregion

        #region Unity Event Method
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;//공격 범위는 빨간색 선으로 표시
            Gizmos.DrawWireSphere(transform.position, _attackRange);
        }

        private void Update()
        {
            //0.2초마다 가장 가까운 적 탐색
            _searchCooldown = Mathf.Clamp(_searchCooldown - Time.deltaTime, 0f, _searchInterval);
            if (_searchCooldown <= 0)
            {
                FindClosestEnemy();
                _searchCooldown = _searchInterval;
            }
        }
        #endregion

        #region Custom Method
        private void FindClosestEnemy()
        {
            // 1. [변경] 맵 전체를 뒤지지 않고, 타워 위치를 기준으로 사정거리(_attackRange) 내에 있는 특정 레이어(_enemyLayer)의 콜라이더들만 가져옵니다.
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, _attackRange, _enemyLayer);

            GameObject closestEnemy = null;
            float shortestDistance = Mathf.Infinity;//최소값을 구하기 위해 최초 기준은 '무한대'로 설정

            //배열의 Enemy 중 거리가 가장 가까운 Enemy 탐색
            foreach (Collider enemyCollider in hitColliders)
            {
                // 콜라이더가 자식에 있더라도 무조건 최상위 부모(Root) 오브젝트를 타겟으로 지정합니다.
                GameObject enemy = enemyCollider.transform.root.gameObject;
                // 타워와 적 사이의 실제 거리 계산
                float distanceToEnemy = Vector3.Distance(transform.position, enemyCollider.transform.position);

                // 방금 재어본 거리가 기존에 알고 있던 '가장 짧은 거리'보다 더 가깝다면?
                if (distanceToEnemy < shortestDistance)
                {
                    shortestDistance = distanceToEnemy;//'가장 짧은 거리' 갱신
                    closestEnemy = enemy;//'가장 가까운 적' 기록
                }
            }

            if (closestEnemy != null && shortestDistance <= _attackRange)
            {
                //사정거리 안에 있고, 적을 찾았다면 타겟으로 설정
                _currentTarget = closestEnemy;
            }
            else
            {
                _currentTarget = null;//아니면 타겟 해제
            }
        }

        /// <summary>
        /// 현재 타워가 감지한 유효한 타겟이 있는지 확인하고, 타겟 오브젝트를 안전하게 반환합니다.
        /// </summary>
        /// <param name="currentTarget">감지된 타겟 게임 오브젝트가 할당될 out 매개변수입니다. 타겟이 없다면 null이 할당됩니다.</param>
        /// <returns>유효한 타겟이 존재하면 true, 그렇지 않으면 false를 반환합니다.</returns>
        public bool TryGetCurrentTarget(out GameObject currentTarget)
        {
            currentTarget = _currentTarget;
            return currentTarget != null;
        }
        #endregion
    }
}
