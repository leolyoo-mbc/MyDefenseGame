//EnemyController.cs
using UnityEngine;

namespace MyDefenseGame
{
    /// <summary>
    /// 적(Enemy)을 관리하는 클래스
    /// </summary>
    public class EnemyController : MonoBehaviour
    {
        //필드 선언부
        #region Variables
        private Transform _destination;//이동 목적지 트랜스폼
        [SerializeField] private float _speed = 10f;
        [SerializeField] private int _maxHp = 100;
        [SerializeField] private int _reward = 50;
        private int _currentHp;
        [SerializeField] private GameObject _deathEffectPrefab;
        private bool _dead = false;
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
            Vector3 dirNormalized = (_destination.position - this.transform.position).normalized;
            //타겟을 향해 이동
            this.transform.Translate(_speed * Time.deltaTime * dirNormalized, Space.World);

            //목적지까지의 거리
            float distanceToDestination = Vector3.Distance(_destination.position, this.transform.position);
            //이번 프레임에 원래 이동해야 할 거리
            float moveDistance = _speed * Time.deltaTime;

            //이동할 거리가 남은 거리보다 크면 도착 판정
            if (moveDistance >= distanceToDestination)
            {
                //도착 위치에 강제 이동시키기
                this.transform.position = _destination.position;
                ArriveTarget();
            }
        }
        #endregion

        //유저 구현 함수
        #region Custom Method
        private void ArriveTarget()
        {
            Debug.Log("종점 도착!!!!");
            Destroy(this.gameObject);
        }

        public void Setup(Transform destination)
        {
            _destination = destination;
        }

        public void TakeDamage(int damage)
        {
            if (_dead) return;//죽은 상태면 중복 실행 방지
            _currentHp -= damage;
            if (_currentHp <= 0) Die();
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