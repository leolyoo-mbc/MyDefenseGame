using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MyDefenseGame
{
    public class TilePopupManager : MonoBehaviour
    {
        #region Variables
        private TileController _currentTile;
        [SerializeField] private TMP_Text _textUpgradeCost;
        [SerializeField] private Button _buttonUpgrade;
        [SerializeField] private TMP_Text _textSellPrice;
        #endregion

        #region Unity Event Method
        void Awake()
        {
            gameObject.SetActive(false);
        }
        #endregion

        #region Custom Method
        public void OnUpgradeButtonClicked()
        {
            if (_currentTile != null) _currentTile.OnUpgrade();
            Open(_currentTile);
        }

        public void OnSellButtonClicked()
        {
            if (_currentTile != null) _currentTile.OnSell();
        }

        public void Close()
        {
            gameObject.SetActive(false);
            _currentTile = null;
        }

        public void Open(TileController calledTile)
        {
            _currentTile = calledTile;
            transform.position = calledTile.transform.position;
            _textUpgradeCost.text = calledTile.GetUpgradeCostText();
            _buttonUpgrade.interactable = !calledTile.IsUpgraded;
            _textSellPrice.text = calledTile.GetSellPriceText();
            gameObject.SetActive(true);
        }

        public void TogglePopup(TileController calledTile)
        {
            // 방어적 코드: 알 수 없는 이유로 빈 데이터가 들어오면 무시합니다.
            if (calledTile == null) return;

            // A. 이미 화면에 켜져 있고, 누른 타일이 '나 자신'이라면? -> 닫고 끝낸다.
            if (gameObject.activeSelf && _currentTile == calledTile)
            {
                Close();
                return;
            }

            // B. 그 외의 모든 상황 (꺼져 있거나, 다른 타일을 눌렀거나) -> 연다.
            Open(calledTile);
        }
        #endregion
    }
}

