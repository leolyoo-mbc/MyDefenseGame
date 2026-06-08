using System.Collections;
using TMPro;
using UnityEngine;

namespace MyDefenseGame
{
    /// <summary>
    /// 적 스폰을 관리하는 클래스
    /// </summary>
    public class EnemySpawner : MonoBehaviour
    {
        #region Variables
        [Header("스폰 기본 설정")]
        [Tooltip("스폰할 적의 기본 프리팹")]
        [SerializeField] private GameObject _enemyPrefab;

        [Tooltip("적이 나타날 시작 위치 오브젝트")]
        [SerializeField] private Transform _spawnPoint;
        [Tooltip("스폰된 적의 이동 목적지 오브젝트")]
        [SerializeField] private Transform _destination;

        [Space(20)]
        [Header("게임 밸런스 조정")]
        [Tooltip("다음 웨이브가 시작될 때까지의 대기 시간(초)")]
        [Range(1f, 30f)]
        [SerializeField] private float _waveSpawnInterval = 5f;

        [Tooltip("한 웨이브 내에서 적들이 연달아 나오는 간격(초)")]
        [Range(0.1f, 2f)]
        [SerializeField] private float _enemySpawnInterval = 0.5f;

        [Space(20)]
        [Header("UI 세팅")]
        [SerializeField] private TextMeshProUGUI _timerText;

        private int _spawnCount = 1; // 한 번에 스폰할 적의 수 (기본 1마리 시작)
        #endregion

        #region Unity Event Method
        private void Start()
        {
            // 게임이 시작되면 스폰 시스템 전체를 관리하는 코루틴을 가동합니다.
            StartCoroutine(SpawnSystemRoutine());
            _timerText.text = "";
        }
        #endregion

        #region Custom Method
        /// <summary>
        /// 전체 웨이브 루프와 타이머 UI를 총괄하는 메인 코루틴
        /// </summary>
        private IEnumerator SpawnSystemRoutine()
        {
            while (true)
            {
                // 1. 이번 웨이브의 적들을 스폰합니다.
                // yield return을 앞에 붙여줌으로써, "스폰이 완전히 끝날 때까지" 이 코루틴도 대기합니다.
                yield return StartCoroutine(SpawnRoutine(_enemyPrefab, _spawnPoint, _spawnCount, _enemySpawnInterval));

                // 2. 스폰이 끝났으므로 다음 웨이브의 스폰 마리 수를 증가시킵니다.
                _spawnCount++;

                // 3. 다음 웨이브 시작 전까지 실시간으로 타이머 UI를 깎으며 대기합니다.
                float cooldownTimer = _waveSpawnInterval;
                while (cooldownTimer > 0f)
                {
                    cooldownTimer -= Time.deltaTime;
                    // 혹시 0 아래로 내려가면 0으로 고정
                    cooldownTimer = Mathf.Max(0f, cooldownTimer);

                    _timerText.text = $"Next Wave In: {cooldownTimer:F1}s";

                    yield return null; // 매 프레임 대기 (Update처럼 작동)
                }
            }
        }

        /// <summary>
        /// 한 웨이브 내에서 지정된 수만큼 적을 연달아 생성하는 서브 코루틴
        /// </summary>
        private IEnumerator SpawnRoutine(GameObject prefab, Transform spawnPoint, int spawnCount, float spawnInterval)
        {
            for (int i = 0; i < spawnCount; i++)
            {
                GameObject spawnedEnemy = Instantiate(prefab, spawnPoint.position, Quaternion.identity);
                spawnedEnemy.GetComponent<EnemyController>().Setup(_destination);//이동 목적지 지정
                yield return new WaitForSeconds(spawnInterval);
            }
        }
        #endregion
    }
}