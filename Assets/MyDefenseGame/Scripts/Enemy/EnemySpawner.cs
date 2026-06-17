using System.Collections;
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
        [Tooltip("적이 나타날 시작 위치 오브젝트")]
        [SerializeField] private Transform _spawnPoint;
        [Tooltip("스폰된 적의 이동 목적지 오브젝트")]
        [SerializeField] private Transform _destination;
        [Tooltip("스폰된 적의 이동 웨이포인트 오브젝트")]
        [SerializeField] private WayPoints _wayPoints;

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
        #endregion

        #region Unity Event Method
        private void Start()
        {
            _waveInfoUI.SetActive(false);
            GameData.Rounds = _waveIndex + 1;
        }

        private void Update()
        {
            _waveInfoText.text = $"{enemyAlive} / {_enemyMax}";
            if (enemyAlive <= 0) ReadyNextWave();

        }
        #endregion

        #region Custom Method
        public void StartWave()
        {
            GameData.Rounds = _waveIndex + 1;
            _waveStartButton.gameObject.SetActive(false);
            _waveInfoUI.SetActive(true);
            StartCoroutine(SpawnRoutine(_waves[_waveIndex], _spawnPoint, _destination));
            _waveIndex++;
        }

        /// <summary>
        /// 한 웨이브 내에서 지정된 수만큼 적을 연달아 생성하는 서브 코루틴
        /// </summary>
        private IEnumerator SpawnRoutine(Wave wave, Transform spawnPoint, Transform destination)
        {
            _enemyMax = wave.spawns.Length;
            enemyAlive = _enemyMax;

            foreach (var spawn in wave.spawns)
            {
                GameObject spawnedEnemy = Instantiate(spawn.enemyPrefab.gameObject, spawnPoint.position, Quaternion.identity);
                spawnedEnemy.GetComponent<EnemyController>().Setup(_wayPoints);//웨이포인트 지정
                yield return new WaitForSeconds(spawn.delayNextSpawn);
            }
        }

        void ReadyNextWave()
        {
            if (_waveStartButton.gameObject.activeSelf) return;

            if (_waveIndex >= _waves.Length)
            {
                print("LEVEL CLEAR");
                gameObject.SetActive(false);
                return;
            }

            _waveStartButton.gameObject.SetActive(true);
            _waveInfoUI.SetActive(false);
        }
        #endregion
    }
}