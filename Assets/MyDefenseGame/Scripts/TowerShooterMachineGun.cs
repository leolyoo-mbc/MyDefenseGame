using MyDefenseGame;
using System.Collections;
using UnityEngine;

namespace MyDefenseGame
{
    /// <summary>
    /// 타겟으로 탄환을 발사하는 클래스
    /// </summary>
    [RequireComponent(typeof(TowerTargetDetector))]
    public class TowerShooterMachineGun : MonoBehaviour
    {
        #region Variables
        private TowerTargetDetector _targetDetector;
        [SerializeField] private Transform _firePoint;
        [SerializeField] private GameObject _bulletPrefab;
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
                if (TryShoot())
                {
                    _shootCooldown = _shootInterval;//발사 성공 시 쿨다운 초기화
                }
            }
        }
        #endregion

        #region Custom Method
        /// <summary>
        /// 탄환을 발사하는 메서드
        /// </summary>
        /// <returns>발사 성공 여부 bool</returns>
        private bool TryShoot()
        {
            Transform currentTarget = _targetDetector.GetCurrentTarget();
            //타겟이 없으면 아래 로직을 타지 않고 리턴
            if (currentTarget == null) return false;
            print("Shoot!!!!!");
            //발사할때 총구(FirePoint) 위치에 탄환 객체 생성하기
            GameObject spawnedBullet = Instantiate(_bulletPrefab, _firePoint.position, _firePoint.rotation);
            //탄환 객체에 타겟 할당
            spawnedBullet.GetComponent<BulletController>().Setup(currentTarget);
            return true;
        }
        #endregion
    }
}