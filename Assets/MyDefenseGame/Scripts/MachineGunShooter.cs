using MyDefenseGame;
using System.Collections;
using UnityEngine;

namespace MyDefenseGame
{
    /// <summary>
    /// 타겟으로 탄환을 발사하는 클래스
    /// </summary>
    [RequireComponent(typeof(TowerTargetDetector))]
    public class MachineGunShooter : MonoBehaviour
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
            _shootCooldown -= Time.deltaTime;

            // 타겟이 없으면 아래 로직을 타지 않고 리턴
            if (_targetDetector.GetCurrentTarget() == null) return;

            if (_shootCooldown <= 0f)
            {
                // 쿨다운이 끝났다면 발사!
                print("Shoot!!!!!");

                //발사할때 총구(FirePoint) 위치에 탄환 객체 생성하기
                GameObject spawnedBullet = Instantiate(_bulletPrefab, _firePoint.position, Quaternion.identity);
                spawnedBullet.GetComponent<BulletController>().target = _targetDetector.GetCurrentTarget();
                //탄환 발사 코루틴 실행
                //StartCoroutine(ShootRoutine(_targetDetector.GetCurrentTarget(), spawnedBullet));

                _shootCooldown = _shootInterval;//발사 후 쿨다운 초기화
            }
        }
        #endregion

        #region Custom Method
        //탄환 발사 및 충돌체크하는 코루틴
        private IEnumerator ShootRoutine(Transform target, GameObject bullet)
        {
            //타겟과 탄환이 모두 있는 경우에만 실행
            while (target != null && bullet != null)
            {
                Vector3 targetPosition = target.position;//현재 프레임에서 널세이프하도록 로컬 변수에 저장

                Vector3 dir = targetPosition - bullet.transform.position;//타겟으로의 방향

                float moveDistance = _bulletSpeed * Time.deltaTime;//이번 프레임에 원래 이동할 거리
                float distanceToTarget = Vector3.Distance(targetPosition, bullet.transform.position);

                //이번 프레임에 이동 시 타겟을 지나칠 예정이면 타겟에 도착한 것으로 판정
                if (moveDistance >= distanceToTarget)
                {
                    //타겟 및 탄환 제거 및 반복문 탈출
                    Debug.Log("Hit Target!!!");
                    Destroy(bullet);
                    if (target != null) Destroy(target.gameObject);//제거 직전 널체크
                    break;
                }

                bullet.transform.Translate(moveDistance * dir.normalized, Space.World);//타겟으로 탄환 이동
                yield return null;
            }
            if (bullet != null) Destroy(bullet);
        }
        #endregion
    }
}