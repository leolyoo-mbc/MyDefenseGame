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
        [SerializeField] private float _damagePerSecond = 30f;
        [SerializeField] private float _slowRate = 0.4f;

        [SerializeField] private float _bonusDamage = 15f;
        [SerializeField] private float _bonusDamageTime = 1.5f;
        private GameObject _lastTarget;  // 직전 프레임에 때렸던 타겟 기억
        private Damageable _lastTargetDamageable; // GetComponent 호출 최소화를 위해 별도로 관리
        private EnemyController _lastTargetSlowable; // GetComponent 호출 최소화를 위해 별도로 관리
        private float _attackDuration = 0f;   // 동일한 타겟을 공격한 누적 시간

        // 레이저 타격 이펙트 (타겟에 맞는 부위에 생성할 파티클)
        [SerializeField] private ParticleSystem _laserHitEffect;
        [SerializeField] private Light _laserLight;
        #endregion

        #region Unity Event Method
        private void Awake()
        {
            _targetDetector = GetComponent<TowerTargetDetector>();
            // 시작할 때는 레이저를 꺼둡니다.
            HideLaser();
        }

        private void Update()
        {

            if (_targetDetector.TryGetCurrentTarget(out GameObject currentTarget))
            {
                if (_lastTarget != currentTarget)
                {
                    // 타겟이 바뀌었다면 (기존 타겟 사망 or 더 가까운 적 등장) 타이머 초기화
                    _attackDuration = 0f;

                    _lastTarget = currentTarget;
                    if (currentTarget.TryGetComponent<Damageable>(out Damageable currentTargetDamageable)) _lastTargetDamageable = currentTargetDamageable;
                    else _lastTargetDamageable = null;
                    if (currentTarget.TryGetComponent<EnemyController>(out EnemyController currentTargetSlowable)) _lastTargetSlowable = currentTargetSlowable;
                    else _lastTargetSlowable = null;
                }
                else
                {
                    // 같은 타겟을 계속 때리고 있다면 타이머(누적 시간) 증가
                    _attackDuration += Time.deltaTime;
                }

                // 2. 레이저 발사 및 데미지 적용
                VisualizeLaser(currentTarget);
                if (_lastTargetDamageable != null) ApplyDamage(_lastTargetDamageable);
                if (_lastTargetSlowable != null) _lastTargetSlowable.ApplySlow(_slowRate);
            }
            else
            {
                // 타겟이 없으면 레이저를 끕니다.
                HideLaser();
                _attackDuration = 0f; // 누적 시간 초기화
                _lastTarget = null;   // 기억하던 타겟 삭제
                _lastTargetDamageable = null;
                _lastTargetSlowable = null;
            }
        }
        #endregion

        #region Custom Method
        private void ApplyDamage(Damageable target)
        {
            float finalDPS = _damagePerSecond;

            // 누적 시간이 보너스 조건을 충족했다면 데미지 증폭
            if (_attackDuration >= _bonusDamageTime) finalDPS += _bonusDamage;

            // 3. 지속 데미지 적용
            target.TakeDamage(finalDPS * Time.deltaTime);
        }

        private void VisualizeLaser(GameObject target)
        {
            // 1. 레이저 시각화 켜기
            if (!_lineRenderer.enabled) _lineRenderer.enabled = true;
            if (_laserHitEffect != null && !_laserHitEffect.isPlaying)
            {
                _laserHitEffect.Play();
                _laserLight.enabled = true;
            }


            // 2. LineRenderer의 시작점(총구)과 끝점(타겟) 위치 갱신
            _lineRenderer.SetPosition(0, _firePoint.position);
            _lineRenderer.SetPosition(1, target.transform.position);

            Vector3 direction = _firePoint.position - target.transform.position;
            _laserHitEffect.transform.SetPositionAndRotation(target.transform.position + direction.normalized / 2, Quaternion.LookRotation(direction));
        }

        private void HideLaser()
        {
            if (_lineRenderer.enabled) _lineRenderer.enabled = false;
            if (_laserHitEffect != null && _laserHitEffect.isPlaying)
            {
                _laserHitEffect.Stop();
                _laserLight.enabled = false;
            }
        }
        #endregion
    }
}
