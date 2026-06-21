using System.Collections;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace MyDefenseGame
{
    /// <summary>
    /// 적 스폰을 관리하는 클래스
    /// </summary>
    public class EnemySpawner : MonoBehaviour
    {
        #region Variables
        [Header("스폰 기본 설정")]
        [SerializeField] private Wave[] _waves;
        [Tooltip("적이 나타날 시작 노드")]
        [SerializeField] private Node _startNode;

        [Space(20)]
        [Header("UI 세팅")]
        [SerializeField]
        private Button _waveStartButton;
        [SerializeField]
        private GameObject _waveInfoUI;
        [SerializeField]
        private TMP_Text _waveInfoText;

        private int _enemyMax = 0;
        public static int enemyAlive = 0;

        private int _waveIndex = 0;

        private bool _isSpawning = false;
        #endregion

        #region Unity Event Method
        private void Start()
        {
            _waveInfoUI.SetActive(false);
            GameData.Rounds = _waveIndex + 1;
        }

        private void Update()
        {
            // 나쁜 예 (매 프레임 가비지 발생)
            // _waveInfoText.text = $"{enemyAlive} / {_enemyMax}";

            // 좋은 예 (가비지 발생 없음 또는 극소화)
            _waveInfoText.SetText("{0} / {1}", enemyAlive, _enemyMax);

            if (enemyAlive <= 0 && !_isSpawning) ReadyNextWave();
        }
        #endregion

        #region Custom Method
        public void StartWave()
        {

            GameData.Rounds = _waveIndex + 1;
            _waveStartButton.gameObject.SetActive(false);
            _waveInfoUI.SetActive(true);
            StartCoroutine(SpawnRoutine(_waves[_waveIndex], _startNode));
            _waveIndex++;
        }

        /// <summary>
        /// 한 웨이브 내에서 지정된 수만큼 적을 연달아 생성하는 서브 코루틴
        /// </summary>
        private IEnumerator SpawnRoutine(Wave wave, Node startNode)
        {
            _isSpawning = true;

            // 1. [추가] 스폰 시작 전, 이번 웨이브의 총 적 마리 수를 미리 모두 더합니다.
            _enemyMax = 0;
            foreach (var spawnGroup in wave.SpawnGroups)
            {
                _enemyMax += spawnGroup.SpawnCount;
            }

            enemyAlive = 0; // 살아있는 적 수는 0으로 초기화

            foreach (var spawnGroup in wave.SpawnGroups)
            {
                for (int i = 0; i < spawnGroup.SpawnCount; i++)
                {
                    EnemyController spawnedEnemy = Instantiate(spawnGroup.EnemyPrefab, startNode.Position, Quaternion.identity);
                    spawnedEnemy.Init(startNode);
                    enemyAlive++;
                    yield return new WaitForSeconds(spawnGroup.SpawnInterval);
                }
                yield return new WaitForSeconds(spawnGroup.DelayNextSpawnGroup);
            }

            _isSpawning = false;
        }

        void ReadyNextWave()
        {
            if (_waveStartButton.gameObject.activeSelf) return;

            if (_waveIndex >= _waves.Length)
            {
                print("LEVEL CLEAR");

                // 레벨 클리어 로직
                GameManager.isClearLevel = true;

                gameObject.SetActive(false);
                return;
            }

            _waveStartButton.gameObject.SetActive(true);
            _waveInfoUI.SetActive(false);
        }
        #endregion
    }
}

