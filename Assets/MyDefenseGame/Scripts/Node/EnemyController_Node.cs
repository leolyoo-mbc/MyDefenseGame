using UnityEngine;
using UnityEngine.UI;

namespace MyDefenseGame
{
    /// <summary>
    /// 적(Enemy)을 관리하는 클래스 (노드 이동 방식)
    /// </summary>
    public class EnemyController_Node : MonoBehaviour, IDamageable
    {
        #region Variables
        [SerializeField] private float _speed = 10f;
        [SerializeField] private float _maxHp = 100f;
        [SerializeField] private int _reward = 50;

        private float _currentHp;
        private float _currentSlowRate = 0f;

        [SerializeField] private GameObject _deathEffectPrefab;
        private bool _dead = false;
        private bool _isSlowed = false;
        [SerializeField] private Image _healthBarImage;

        // [변경] 이제 인덱스나 배열 대신, 현재 이동해야 할 목적지 노드를 직접 참조합니다.
        private Node _nextNode;
        #endregion

        #region Unity Event Method
        private void Start()
        {
            _currentHp = _maxHp;
        }

        void Update()
        {
            // 방어적 코드: 목적지 노드가 지정되지 않았거나 최종 종점에 도달했다면 이동 중지
            if (_nextNode == null) return;

            // 1. [수정] 현재 향하고 있는 노드의 transform을 목적지로 설정합니다.
            Transform destination = _nextNode.transform;

            // 목적지까지의 방향
            Vector3 dirNormalized = (destination.position - transform.position).normalized;

            // 타겟을 바라보도록 회전
            if (dirNormalized != Vector3.zero) transform.rotation = Quaternion.LookRotation(dirNormalized);

            // 목적지까지의 거리
            float distanceToDestination = Vector3.Distance(destination.position, transform.position);

            // 이번 프레임에 원래 이동해야 할 거리 계산
            float moveDistance;
            if (_isSlowed) moveDistance = _speed * (1 - _currentSlowRate) * Time.deltaTime;
            else moveDistance = _speed * Time.deltaTime;

            // 슬로우 변수 초기화
            _isSlowed = false;
            _currentSlowRate = 0f;

            // 이동할 거리가 남은 거리보다 크면 해당 노드 도착 판정
            if (moveDistance >= distanceToDestination)
            {
                // 노드 위치에 정확히 강제 고정
                this.transform.position = destination.position;

                // 2. [수정] 노드가 보유한 프로퍼티를 통해 '다음 노드'로 목적지를 갱신합니다.
                _nextNode = _nextNode.NextNode;

                // 3. [수정] 다음 노드가 존재하지 않는다면(null), 최종 종점에 도달한 것입니다.
                if (_nextNode == null)
                {
                    ArriveTarget();
                }
            }
            else
            {
                // 아직 노드에 도달하지 않았다면 타겟을 향해 이동
                this.transform.Translate(moveDistance * dirNormalized, Space.World);
            }

            // 체력바 업데이트
            if (_healthBarImage != null) _healthBarImage.fillAmount = _currentHp / _maxHp;
        }
        #endregion

        #region Custom Method
        private void ArriveTarget()
        {
            Debug.Log("종점 도착!!!!");
            GameData.Lives--;
            EnemySpawner.enemyAlive--;

            Destroy(this.gameObject);
        }

        // [수정] 스포너가 적을 소환할 때, 첫 번째 노드를 매개변수로 넘겨주어 경로 설정을 시작합니다.
        public void Setup(Node startNode)
        {
            _nextNode = startNode;
        }

        public void TakeDamage(float damage)
        {
            if (_dead) return;
            _currentHp = Mathf.Max(_currentHp - damage, 0);
            if (_currentHp <= 0) Die();
        }

        public void ApplySlow(float slowRate)
        {
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

            EnemySpawner.enemyAlive--;

            Destroy(gameObject);
        }
        #endregion
    }
}