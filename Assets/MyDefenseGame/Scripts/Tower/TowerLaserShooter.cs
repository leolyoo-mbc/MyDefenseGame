using UnityEngine;

namespace MyDefenseGame
{
    [RequireComponent(typeof(TowerTargetDetector))]
    public class TowerLaserShooter : MonoBehaviour
    {
        #region Variables
        private TowerTargetDetector _targetDetector;

        [SerializeField] private Transform _firePoint;
        [Header("Laser Settings")]
        [SerializeField] private LineRenderer _lineRenderer; // 레이저 빔 시각화용
        [SerializeField] private float _damagePerSecond = 30f;  // 초당 데미지 (DPS)
        [SerializeField] private float _slowRate = 0.4f;  // 초당 데미지 (DPS)
        [SerializeField] private float _bonusDamage = 15f;
        [SerializeField] private float _bonusDamageTime = 1.5f;
        private EnemyController _lastTarget;  // 직전 프레임에 때렸던 타겟 기억
        private float _attackDuration = 0f;   // 동일한 타겟을 공격한 누적 시간
        // 레이저 타격 이펙트 (타겟에 맞는 부위에 생성할 파티클)
        // [SerializeField] private GameObject _laserHitEffect; 
        #endregion

        #region Unity Event Method
        private void Awake()
        {
            _targetDetector = GetComponent<TowerTargetDetector>();
            // 시작할 때는 레이저를 꺼둡니다.
            _lineRenderer.enabled = false;
        }

        private void Update()
        {
            EnemyController currentTarget = _targetDetector.GetCurrentTarget();

            if (currentTarget != null)
            {
                if (_lastTarget != currentTarget)
                {
                    // 타겟이 바뀌었다면 (기존 타겟 사망 or 더 가까운 적 등장) 타이머 초기화
                    _attackDuration = 0f;
                    _lastTarget = currentTarget;
                }
                else
                {
                    // 같은 타겟을 계속 때리고 있다면 타이머(누적 시간) 증가
                    _attackDuration += Time.deltaTime;
                }

                // 2. 레이저 발사 및 데미지 적용
                ShootLaser(currentTarget);
            }
            else
            {
                // 타겟이 없으면 레이저를 끕니다.
                if (_lineRenderer.enabled) _lineRenderer.enabled = false;
                _attackDuration = 0f; // 누적 시간 초기화
                _lastTarget = null;   // 기억하던 타겟 삭제
            }
        }
        #endregion

        #region Custom Method
        private void ShootLaser(EnemyController target)
        {
            // 1. 레이저 시각화 켜기
            if (!_lineRenderer.enabled)
            {
                _lineRenderer.enabled = true;
            }

            // 2. LineRenderer의 시작점(총구)과 끝점(타겟) 위치 갱신
            _lineRenderer.SetPosition(0, _firePoint.position);
            _lineRenderer.SetPosition(1, target.transform.position);

            float finalDPS = _damagePerSecond;

            // 누적 시간이 보너스 조건을 충족했다면 데미지 증폭
            if (_attackDuration >= _bonusDamageTime) finalDPS += _bonusDamage;

            // 3. 지속 데미지 적용
            target.TakeDamage(finalDPS * Time.deltaTime);

            // 4. 슬로우 효과 적용
            target.ApplySlow(_slowRate);
        }
        #endregion
    }
}
