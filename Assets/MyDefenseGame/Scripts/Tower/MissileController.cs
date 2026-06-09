using UnityEngine;

namespace MyDefenseGame
{
    public class MissileController : ProjectileController
    {
        #region Variables
        [Tooltip("미사일의 공격 반경")]
        [SerializeField] private float _damageRange = 3.5f;

        [Tooltip("감지할 적의 물리 레이어 마스크")]
        [SerializeField] private LayerMask _enemyLayer;

        [SerializeField] private int _damage = 50;
        #endregion

        #region Unity Event Method
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;//공격 범위는 빨간색 선으로 표시
            Gizmos.DrawWireSphere(transform.position, _damageRange);
        }
        #endregion

        #region Custom Method
        protected override void ApplyDamage(GameObject target)
        {
            Debug.Log("미사일 타격! 광역 폭발 발생!");

            // 1. 폭발 중심지(미사일 위치)를 기준으로 반경(3.5f) 내의 모든 충돌체를 배열로 가져옵니다.
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, _damageRange, _enemyLayer);

            // 2. 배열에 담긴 충돌체들을 하나씩 검사합니다.
            foreach (Collider hitCol in hitColliders)
            {
                // 3. 해당 충돌체가 '적(Enemy)'인지 확인합니다. (태그를 사용한 예시)
                if (hitCol.TryGetComponent(out EnemyController enemy))
                {
                    // [임시 처리] 적을 즉사(Kill) 시킵니다.
                    enemy.TakeDamage(_damage);
                }
            }
        }
        #endregion
    }
}