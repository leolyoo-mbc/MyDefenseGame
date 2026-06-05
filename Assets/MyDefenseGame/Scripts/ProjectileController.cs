using UnityEngine;

namespace MyDefenseGame
{
    /// <summary>
    /// 발사체를 관리하는 추상 클래스
    /// </summary>
    public abstract class ProjectileController : MonoBehaviour
    {
        #region Variables
        protected Transform _target;

        [Tooltip("발사체의 속도")]
        [SerializeField] protected float _speed;
        [Tooltip("충돌 효과 프리팹")]
        [SerializeField] protected GameObject _hitEffectPrefab;
        #endregion

        #region Unity Event Method
        protected virtual void Update()
        {
            if (_target == null)
            {
                //타겟이 없으면 발사체 제거
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
        /// 발사체의 타겟을 초기화하는 메서드
        /// </summary>
        /// <param name="target">발사체가 향할 타겟의 Transform</param>
        public virtual void Setup(Transform target)
        {
            _target = target;
        }

        /// <summary>
        /// 발사체의 충돌을 처리하는 메서드
        /// </summary>
        protected virtual void HitTarget()
        {
            //1. 공통 기능: 이펙트 생성
            if (_hitEffectPrefab != null) Instantiate(_hitEffectPrefab, transform.position, Quaternion.identity);

            //2. 고유 기능: 자식 클래스가 구현한 실제 공격 로직 호출
            ApplyDamage();

            //3. 공통 기능: 내 자신(발사체) 파괴
            Destroy(gameObject);
        }

        /// <summary>
        /// 자식 클래스가 반드시 구현해야 하는 '실제 데미지/적 파괴' 로직
        /// </summary>
        protected abstract void ApplyDamage();
        #endregion
    }
}
