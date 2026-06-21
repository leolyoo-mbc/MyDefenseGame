using MyDefenseGame;
using System.Collections;
using UnityEngine;

namespace MyDefenseGame
{
    /// <summary>
    /// 타겟으로 탄환을 발사하는 클래스
    /// </summary>
    [RequireComponent(typeof(TowerTargetDetector))]
    public class TowerProjectileShooter : MonoBehaviour
    {
        #region Variables
        private TowerTargetDetector _targetDetector;
        [SerializeField] private Transform _firePoint;
        [SerializeField] private ProjectileController _projectilePrefab;
        [SerializeField] private float _shootInterval = 1f;

        private float _shootCooldown = 0f;
        #endregion

        #region Unity Event Method
        private void Awake()
        {
            _targetDetector = GetComponent<TowerTargetDetector>();
        }

        private void Update()
        {
            _shootCooldown = Mathf.Max(_shootCooldown - Time.deltaTime, 0f);

            if (_shootCooldown <= 0f)
            {
                //쿨다운이 끝났다면 발사!
                if (_targetDetector.TryGetCurrentTarget(out GameObject currentTarget))
                {
                    print("Shoot!!!!!");
                    ProjectileController spawnedProjectile = Instantiate(_projectilePrefab, _firePoint.position, _firePoint.rotation);
                    spawnedProjectile.Shoot(currentTarget);

                    _shootCooldown = _shootInterval;//발사 성공 시 쿨다운 초기화
                }
            }
        }
        #endregion
    }
}