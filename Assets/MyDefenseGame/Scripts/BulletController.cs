using UnityEngine;

namespace MyDefenseGame
{
    /// <summary>
    /// 탄환을 관리하는 클래스
    /// </summary>
    public class BulletController : MonoBehaviour
    {
        #region Variables
        private Transform _target;
        [SerializeField] private float _speed = 70f;
        [SerializeField] private GameObject _hitEffectPrefab;
        #endregion

        #region Unity Event Method
        private void Update()
        {
            if (_target == null)
            {
                //타겟이 없으면 탄환 제거
                Destroy(gameObject);
                return;
            }

            Vector3 targetPosition = _target.position;//이번 프레임에 널세이프하도록 로컬변수에 저장
            Vector3 direction = targetPosition - transform.position;//타겟을 향한 방향 벡터
            Quaternion lookRotation = Quaternion.LookRotation(direction);//타겟을 바라보는 회전 값

            float moveDistance = _speed * Time.deltaTime;//이번 프레임에 원래 이동할 거리
            float distanceToTarget = Vector3.Distance(targetPosition, transform.position);

            //이번 프레임에 이동 시 타겟을 지나칠 예정이면 타겟에 도착한 것으로 판정
            if (moveDistance >= distanceToTarget)
            {
                HitTarget();
            }
            else
            {
                //월드 기준 이동
                transform.Translate(moveDistance * direction.normalized, Space.World);
                transform.rotation = lookRotation;//타겟을 바라보도록 회전
            }
        }
        #endregion

        #region Custom Method
        /// <summary>
        /// 탄환의 속성을 초기화하는 메서드
        /// </summary>
        /// <param name="target">탄환이 향할 타겟의 Transform</param>
        public void Setup(Transform target)
        {
            _target = target;
        }

        /// <summary>
        /// 타겟 공격 처리 메서드
        /// </summary>
        private void HitTarget()
        {
            if (_hitEffectPrefab != null)
            {
                GameObject hitEffect = Instantiate(_hitEffectPrefab, transform.position, Quaternion.identity);
                Destroy(hitEffect, 2f);
            }
            Debug.Log("Hit Target!!!");
            if (_target != null) Destroy(_target.gameObject);
            Destroy(gameObject);
        }
        #endregion
    }
}
