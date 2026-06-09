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
        [SerializeField] private int _damagePerSecond = 50;  // 초당 데미지 (DPS)

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
            GameObject currentTarget = _targetDetector.GetCurrentTarget();

            if (currentTarget != null)
            {
                // 타겟이 있으면 레이저를 쏘고 데미지를 줍니다.
                ShootLaser(currentTarget);
            }
            else
            {
                // 타겟이 없으면 레이저를 끕니다.
                if (_lineRenderer.enabled)
                {
                    _lineRenderer.enabled = false;
                    // 이펙트도 여기서 꺼줍니다.
                }
            }
        }
        #endregion

        #region Custom Method
        private void ShootLaser(GameObject target)
        {
            // 1. 레이저 시각화 켜기
            if (!_lineRenderer.enabled) _lineRenderer.enabled = true;

            // 2. LineRenderer의 시작점(총구)과 끝점(타겟) 위치 갱신
            _lineRenderer.SetPosition(0, _firePoint.position);
            _lineRenderer.SetPosition(1, target.transform.position);

            // 3. 지속 데미지 적용 (추후 구현)
            // 레이저는 매 프레임 공격하므로 Time.deltaTime을 활용한 틱(Tick) 데미지 처리가 필요합니다.
        }
        #endregion
    }
}
