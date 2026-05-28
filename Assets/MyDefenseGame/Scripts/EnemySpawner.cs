//EnemySpawner.cs
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
        //적 스폰 기능 구현
        //시작 지점 위치에 적 1개 스폰
        //이후 적 5초 간격으로 1마리씩 스폰
        //이후 5초마다 1마리씩 추가해서 스폰
        //화면 상단에 Text로 타이머 UI
        #region Variables
        [Header("스폰 기본 설정")]

        [Tooltip("스폰할 적의 기본 프리팹")]
        [SerializeField] private GameObject _enemyPrefab;

        [Tooltip("적이 나타날 시작 위치 오브젝트")]
        [SerializeField] private Transform _spawnPoint;

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
        [SerializeField] private TextMeshProUGUI _timerText;// 화면 상단에 배치할 TextMeshPro-Text 오브젝트

        private int _spawnCount = 1;// 한 번에 스폰할 적의 수 (기본 1마리 시작)
        #endregion

        #region Unity Event Method
        private void Start()
        {
            // 5초 간격으로 점점 늘어나며 스폰하는 코루틴 실행
            StartCoroutine(SpawnRoutine());
        }
        #endregion

        #region Custom Method
        /// <summary>
        /// 지정된 시간 간격마다 적을 스폰하는 코루틴
        /// </summary>
        private IEnumerator SpawnRoutine()
        {
            while (true)
            {
                // 현재 설정된 spawnCount만큼 반복해서 적을 스폰합니다.
                for (int i = 0; i < _spawnCount; i++)
                {
                    //스폰 포인트에 적 스폰
                    Instantiate(_enemyPrefab, _spawnPoint.position, Quaternion.identity);
                    //적 스폰 간격만큼 대기
                    yield return new WaitForSeconds(_enemySpawnInterval);
                }

                // 스폰 후 다음 스폰까지 spawnCount(마리 수)를 1 증가시킵니다.
                // (1마리 스폰 -> 다음엔 2마리 -> 다음엔 3마리 ...)
                _spawnCount++;

                //5초 대기 및 UI 표시 루틴 호출
                yield return StartCoroutine(CountdownRoutine(_waveSpawnInterval));
            }
        }

        /// <summary>
        /// 지정된 시간만큼 UI에 카운트다운을 표시하고 대기하는 코루틴
        /// </summary>
        private IEnumerator CountdownRoutine(float timeToWait)
        {
            float remainingTime = timeToWait;

            while (remainingTime > 0f)
            {
                if (_timerText != null)
                {
                    // 소수점 첫째 자리까지 카운트다운 표시
                    _timerText.text = $"Next Wave: {remainingTime:F1}s";
                }

                // 매 프레임 흐른 시간(Time.deltaTime)만큼 남은 시간에서 차감
                remainingTime -= Time.deltaTime;

                // 다음 프레임까지 대기
                yield return null;
            }

            // 카운트다운이 정확히 0이 되었을 때의 처리
            if (_timerText != null)
            {
                _timerText.text = "Spawning...";
            }
        }
        #endregion
    }
}