using UnityEngine;

namespace MyDefenseGame
{
    public class BuildManager : MonoBehaviour
    {
        #region Variables
        // 1. 싱글톤 인스턴스 선언
        public static BuildManager Instance { get; private set; }

        [Header("타워 프리팹 설정")]
        [SerializeField] private GameObject _machineGunTowerPrefab;
        #endregion

        #region Unity Event Method
        private void Awake()
        {
            // 2. 싱글톤 예외 처리 및 초기화
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;

            // 씬이 바뀌어도 유지하고 싶다면 활성화 (선택 사항)
            // DontDestroyOnLoad(gameObject); 
        }
        #endregion

        #region Custom Method
        /// <summary>
        /// 지정된 위치에 터렛을 생성하는 메서드
        /// </summary>
        public void BuildTurretOn(Vector3 position)
        {
            if (_machineGunTowerPrefab == null)
            {
                Debug.LogError("BuildManager에 머신건 타워 프리팹이 등록되지 않았습니다!");
                return;
            }

            // 터렛 생성
            Instantiate(_machineGunTowerPrefab, position, Quaternion.identity);
        }
        #endregion
    }
}