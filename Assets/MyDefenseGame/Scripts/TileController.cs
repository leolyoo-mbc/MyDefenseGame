using UnityEngine;

namespace MyDefenseGame
{
    [RequireComponent(typeof(Renderer))]
    public class TileController : MonoBehaviour
    {
        #region Variables
        private Renderer _renderer;
        private Material _defaultMaterial;
        [SerializeField] private Material _hoverMaterial;
        private Vector3 _placementPosition;
        private GameObject _installedTower;
        public TowerBlueprint InstalledBlueprint
        {
            get;
            private set;
        }
        public bool IsUpgraded
        {
            get;
            private set;
        }
        [SerializeField] private TilePopupManager _popupUI;
        [SerializeField] private ParticleSystem _sellEffect;
        #endregion

        #region Unity Event Method
        private void Awake()
        {
            _renderer = GetComponent<Renderer>();
            _defaultMaterial = _renderer.sharedMaterial;

            float upperSurfaceOffset = transform.localScale.y / 2f;
            _placementPosition = transform.position + new Vector3(0f, upperSurfaceOffset, 0f);
        }
        #endregion

        #region Custom Method
        public void OnHover()
        {
            if (BuildManager.Instance.IsTowerSelected)
                _renderer.sharedMaterial = _hoverMaterial;
        }

        public void OnHoverExit()
        {
            _renderer.sharedMaterial = _defaultMaterial;
        }

        public void OnClick()
        {
            if (BuildManager.Instance.IsTowerSelected) TryInstallTower();
            else if (_installedTower != null)// 타워가 설치된 타일을 클릭한 경우
            {

                _popupUI.TogglePopup(this);
            }
            else
            {
                //아무것도 해당 안 되는 빈 타일 클릭 시 인터페이스 닫기
                _popupUI.Close();
            }

        }

        public void TryInstallTower()
        {
            if (_installedTower != null) return;

            if (BuildManager.Instance.TryBuildTowerOn(_placementPosition, out GameObject tower, out TowerBlueprint blueprint))
            {
                _installedTower = tower;
                InstalledBlueprint = blueprint;
            }
        }

        public void OnUpgrade()
        {
            if (IsUpgraded) return;
            if (BuildManager.Instance.TryUpgradeTower(ref _installedTower, InstalledBlueprint))
            {
                print("업그레이드 완료!");
                IsUpgraded = true;
            }
        }

        public string GetUpgradeCostText()
        {
            if (IsUpgraded) return "MAX"; // 업그레이드 완료 시 표시할 텍스트
            if (InstalledBlueprint != null) return InstalledBlueprint.upgradeCost.ToString();
            return "-";
        }

        private int GetSellPrice()
        {
            if (InstalledBlueprint == null) return 0;

            int totalCost = InstalledBlueprint.cost;
            if (IsUpgraded) totalCost += InstalledBlueprint.upgradeCost;

            return totalCost / 2;
        }

        // 2. [수정] 텍스트 반환 메서드는 계산식을 직접 쓰지 않고 헬퍼 메서드를 호출합니다.
        public string GetSellPriceText()
        {
            if (InstalledBlueprint == null) return "-";
            return GetSellPrice().ToString(); // 계산된 숫자를 문자로만 바꿈
        }

        // 3. [완성] 타워 판매 로직의 4단계 흐름
        public void OnSell()
        {
            // 방어적 코드: 타워가 없으면 무시
            if (_installedTower == null) return;

            // Step 1: 환불 금액을 플레이어 소지금에 더해줍니다.
            GameData.Money += GetSellPrice();
            Debug.Log($"타워 판매 완료! 돌려받은 돈: {GetSellPrice()}, 남은 돈: {GameData.Money}");

            // Step 2: 씬에서 타워 오브젝트를 파괴합니다.
            Instantiate(_sellEffect, _installedTower.transform.position, _installedTower.transform.rotation);
            Destroy(_installedTower);

            // Step 3: 타일이 가지고 있던 상태 정보들을 모두 초기화합니다.
            _installedTower = null;
            InstalledBlueprint = null;
            IsUpgraded = false;

            // Step 4: 타워가 사라졌으므로 떠 있는 UI를 닫아줍니다.
            _popupUI.Close();
        }
        #endregion
    }
}
