using System;
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

        private TowerBlueprint _blueprintSelected;
        public bool IsTowerSelected => _blueprintSelected != null;
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
        public bool TryBuildTowerOn(Vector3 position, out GameObject tower, out TowerBlueprint blueprint)
        {
            if (_blueprintSelected == null)
            {
                Debug.Log("설치할 타워 프리팹이 등록되지 않았습니다!");
                tower = null;
                blueprint = null;
                return false;
            }
            //타워 생성
            if (GameData.Money < _blueprintSelected.Cost)
            {
                Debug.Log("돈이 부족합니다");
                tower = null;
                blueprint = null;
                return false;
            }
            tower = Instantiate(_blueprintSelected.Prefab, position, Quaternion.identity);
            blueprint = _blueprintSelected;
            GameData.Money -= _blueprintSelected.Cost;
            Debug.Log($"건설하고 남은돈 : {GameData.Money}");
            _blueprintSelected = null;//타워 선택 초기화
            return true;
        }

        public void SelectTower(TowerBlueprint blueprint)
        {
            _blueprintSelected = blueprint;
        }

        public bool TryUpgradeTower(ref GameObject tower, TowerBlueprint blueprint)
        {
            //타워 업그레이드하는 로직 구현
            if (blueprint.UpgradedPrefab == null) return false;
            if (GameData.Money < blueprint.UpgradeCost) return false;
            //이미 업그레이드 되어있으면 안되도록 해야함
            Transform transform = tower.transform;
            Destroy(tower);
            tower = Instantiate(blueprint.UpgradedPrefab, transform.position, transform.rotation);
            GameData.Money -= blueprint.UpgradeCost;
            return true;
        }
        #endregion
    }
}