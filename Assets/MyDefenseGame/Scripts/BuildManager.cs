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

        private TowerBlueprint _towerSelected;
        public bool IsTowerSelected => _towerSelected != null;
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
            if (_towerSelected == null)
            {
                Debug.Log("설치할 타워 프리팹이 등록되지 않았습니다!");
                return null;
            }
            //타워 생성
            if (GameData.Money < _towerSelected.cost)
            {
                Debug.Log("돈이 부족합니다");
                return null;
            }
            GameObject spawnedTower = Instantiate(_towerSelected.prefab, position, Quaternion.identity);
            GameData.Money -= _towerSelected.cost;
            Debug.Log($"건설하고 남은돈 : {GameData.Money}");
            _towerSelected = null;//타워 선택 초기화
            return spawnedTower;
        }

        public void SelectTower(TowerBlueprint blueprint)
        {
            _towerSelected = blueprint;
        }
        #endregion
    }
}