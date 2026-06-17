using UnityEngine;

namespace MyDefenseGame
{
    /// <summary>
    /// 타일 상태에 따라 타일의 외형을 변경하고 자신 위의 타워를 관리하는 클래스
    /// </summary>
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
        [SerializeField] private ParticleSystem _buildEffect;
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

            if (_installedTower != null)// 타워가 설치된 타일을 클릭한 경우
            {
                _popupUI.TogglePopup(this);
            }
            else
            {
                if (BuildManager.Instance.IsTowerSelected) TryInstallTower();
                else _popupUI.Close();
            }

        }

        public void TryInstallTower()
        {
            if (_installedTower != null) return;

            if (BuildManager.Instance.TryBuildTowerOn(_placementPosition, out GameObject tower, out TowerBlueprint blueprint))
            {
                _installedTower = tower;
                InstalledBlueprint = blueprint;
                Instantiate(_buildEffect, transform.position, Quaternion.identity);
                _popupUI.Close();
            }
        }

        public void OnUpgrade()
        {
            if (IsUpgraded) return;
            if (BuildManager.Instance.TryUpgradeTower(ref _installedTower, InstalledBlueprint))
            {
                print("업그레이드 완료!");
                IsUpgraded = true;
                Instantiate(_buildEffect, transform.position, Quaternion.identity);
                _popupUI.Close();
            }
        }

        public void OnSell()
        {
            // 방어적 코드: 타워가 없으면 무시
            if (_installedTower == null) return;

            // Step 1: 환불 금액을 플레이어 소지금에 더해줍니다.
            GameData.Money += InstalledBlueprint.GetSellPrice(IsUpgraded);
            Debug.Log($"타워 판매 완료! 돌려받은 돈: {InstalledBlueprint.GetSellPrice(IsUpgraded)}, 남은 돈: {GameData.Money}");

            // Step 2: 판매 이펙트를 재생하고 씬에서 타워 오브젝트를 파괴합니다.
            Instantiate(_sellEffect, transform.position, Quaternion.identity);
            Destroy(_installedTower);

            // Step 3: 타일이 가지고 있던 상태 정보들을 모두 초기화합니다.
            _installedTower = null;
            InstalledBlueprint = null;
            IsUpgraded = false;

            _popupUI.Close();
        }
        #endregion
    }
}
