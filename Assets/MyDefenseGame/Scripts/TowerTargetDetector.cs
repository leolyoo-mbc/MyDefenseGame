using UnityEngine;

namespace MyDefenseGame
{
    /// <summary>
    /// 타워의 공격 범위를 바탕으로 기즈모로 타겟을 감지하는 클래스
    /// </summary>
    public class TowerTargetDetector : MonoBehaviour
    {
        #region Variables
        [Tooltip("타워의 공격 범위")]
        [SerializeField] private float attackRange = 7f;

        private Transform currentTarget;
        #endregion

        #region Unity Event Method
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;//공격 범위는 빨간색 선으로 표시
            Gizmos.DrawWireSphere(transform.position, attackRange);
        }

        private void Update()
        {
            // 매 프레임 가장 가까운 적 탐색
            FindClosestEnemy();
        }
        #endregion

        #region Custom Method
        private void FindClosestEnemy()
        {
            //현재 맵 위에 있는 모든 Enemy 태그를 가진 오브젝트들을 배열에 저장
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

            GameObject closestEnemy = null;
            float shortestDistance = Mathf.Infinity;//최소값을 구하기 위해 최초 기준은 '무한대'로 설정

            //배열의 Enemy 중 거리가 가장 가까운 Enemy 탐색
            foreach (GameObject enemy in enemies)
            {
                //타워와 적 사이의 거리
                float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

                //방금 재어본 거리가 기존에 알고 있던 '가장 짧은 거리'보다 더 가깝다면?
                if (distanceToEnemy < shortestDistance)
                {
                    shortestDistance = distanceToEnemy;//'가장 짧은 거리' 갱신
                    closestEnemy = enemy;//'가장 가까운 적' 기록
                }
            }


            if (closestEnemy != null && shortestDistance <= attackRange)
            {
                //사정거리 안에 있고, 적을 찾았다면 타겟으로 설정
                currentTarget = closestEnemy.transform;
            }
            else
            {
                currentTarget = null;//아니면 타겟 해제
            }
        }

        /// <summary>
        /// 현재 감지된 타겟을 반환하는 메서드
        /// </summary>
        /// <returns>현재 감지된 타겟의 Transform</returns>
        public Transform GetCurrentTarget()
        {
            return currentTarget;
        }
        #endregion
    }
}
