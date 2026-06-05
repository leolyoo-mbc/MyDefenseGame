using UnityEngine;

namespace MyDefenseGame
{
    /// <summary>
    /// 타워 건설을 관리하는 클래스
    /// </summary>
    public class BuildManager : MonoBehaviour
    {
        #region Variables
        private static BuildManager _instance;

        public static BuildManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    // 1. 씬에 이미 배치되어 있는지 확인
                    _instance = FindFirstObjectByType<BuildManager>();

                    // 2. 씬에 없다면, 빈 오브젝트를 하나 만들어서 컴포넌트를 붙여줌 (자동 생성)
                    if (_instance == null)
                    {
                        GameObject go = new(typeof(BuildManager).Name);
                        _instance = go.AddComponent<BuildManager>();
                    }
                }
                return _instance;
            }
        }

        [Header("머신건 타워 프리팹")]
        [SerializeField] private GameObject _machineGunTowerPrefab;
        [Header("미사일 런처 타워 프리팹")]
        [SerializeField] private GameObject _missileLauncherTowerPrefab;

        public static bool IsTowerSelected { get; private set; } = false;
        private GameObject _selectedTowerPrefab;
        #endregion

        #region Unity Event Method
        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;

                // 씬이 바뀌어도 파괴되지 않도록 설정 (선택 사항)
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                // 이미 인스턴스가 존재한다면 중복된 것은 파괴
                if (_instance != this)
                {
                    Destroy(gameObject);
                }
            }

        }
        #endregion

        #region Custom Method
        /// <summary>
        /// 지정된 위치에 타워를 생성하는 메서드
        /// </summary>
        /// <param name="position">타워 생성 위치 Vector3</param>
        /// <returns>생성된 타워 GameObject</returns>
        public GameObject BuildTowerOn(Vector3 position)
        {
            if (!IsTowerSelected)
            {
                Debug.Log("타워를 설치하지 못했습니다.!!");
                return null;
            }
            else IsTowerSelected = false;

            if (_selectedTowerPrefab == null)
            {
                Debug.LogError("설치할 타워 프리팹이 등록되지 않았습니다!");
                return null;
            }

            // 타워 생성 및 반환
            return Instantiate(_selectedTowerPrefab, position, Quaternion.identity);
        }

        /// <summary>
        /// 머신건 타워 선택 버튼을 클릭했을 때 호출될 함수
        /// </summary>
        public void SelectMachineGunTower()
        {
            IsTowerSelected = true;
            Debug.Log("머신건 타워를 선택 하였습니다!!");
            _selectedTowerPrefab = _machineGunTowerPrefab;
        }

        public void SelectMissileLauncherTower()
        {
            IsTowerSelected = true;
            Debug.Log("미사일 런처 타워 선택 하였습니다!!");
            _selectedTowerPrefab = _missileLauncherTowerPrefab;
        }
        #endregion
    }
}