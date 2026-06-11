//EnemyController.cs
using UnityEngine;

namespace MyDefenseGame
{
    /// <summary>
    /// 적(Enemy)을 관리하는 클래스
    /// </summary>
    public class EnemyController : MonoBehaviour, IDamageable
    {
        //필드 선언부
        #region Variables
        private Transform _destination;//이동 목적지 트랜스폼
        [SerializeField] private float _speed = 10f;
        [SerializeField] private float _maxHp = 100f;
        [SerializeField] private int _reward = 50;
        private float _currentHp;
        private float _currentSlowRate = 0f;
        [SerializeField] private GameObject _deathEffectPrefab;
        private bool _dead = false;
        private bool _isSlowed = false;
        #endregion

        //유니티 이벤트 함수 구현부
        #region Unity Event Method
        private void Start()
        {
            //현재 체력 최대 체력으로 초기화
            _currentHp = _maxHp;
        }

        void Update()
        {
            //목적지까지의 방향
            Vector3 dirNormalized = (_destination.position - transform.position).normalized;

            //목적지까지의 거리
            float distanceToDestination = Vector3.Distance(_destination.position, transform.position);

            //이번 프레임에 원래 이동해야 할 거리
            float moveDistance;

            //슬로우 상태에 따라 계산
            if (_isSlowed) moveDistance = _speed * (1 - _currentSlowRate) * Time.deltaTime;
            else moveDistance = _speed * Time.deltaTime;

            //이번 프레임에 슬로우 적용이 끝났으므로 변수 초기화
            _isSlowed = false;
            _currentSlowRate = 0f;

            //이동할 거리가 남은 거리보다 크면 도착 판정
            if (moveDistance >= distanceToDestination)
            {
                //도착 위치에 강제 이동시키기
                this.transform.position = _destination.position;
                ArriveTarget();
            }
            else
            {
                //타겟을 향해 이동
                this.transform.Translate(moveDistance * dirNormalized, Space.World);
            }
        }
        #endregion

        //유저 구현 함수
        #region Custom Method
        private void ArriveTarget()
        {
            Debug.Log("종점 도착!!!!");
            GameData.Lives--;
            Destroy(this.gameObject);
        }

        public void Setup(Transform destination)
        {
            _destination = destination;
        }

        public void TakeDamage(float damage)
        {
            if (_dead) return;//죽은 상태면 중복 실행 방지
            _currentHp = Mathf.Max(_currentHp - damage, 0);
            if (_currentHp <= 0) Die();
        }

        public void ApplySlow(float slowRate)
        {
            //이미 슬로우 상태이면 더 큰 슬로우 비율 대입
            if (_isSlowed) _currentSlowRate = slowRate > _currentSlowRate ? slowRate : _currentSlowRate;
            else
            {
                _isSlowed = true;
                _currentSlowRate = slowRate;
            }
        }

        private void Die()
        {
            _dead = true;

            GameData.Money += _reward;

            if (_deathEffectPrefab != null) Instantiate(_deathEffectPrefab, transform.position, Quaternion.identity);

            Destroy(gameObject);
        }
        #endregion
    }
}