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

            //호출 타일의 위치로 이동
            transform.position = calledTile.transform.position;
            //업그레이드 가격 표시
            if (calledTile.IsUpgraded) _textUpgradeCost.text = "DONE";
            else _textUpgradeCost.text = calledTile.InstalledBlueprint.upgradeCost.ToString();
            //이미 업그레이드 된 경우 업그레이드 버튼 비활성화
            _buttonUpgrade.interactable = !calledTile.IsUpgraded;
            //판매 가격 표시
            _textSellPrice.text = calledTile.InstalledBlueprint.GetSellPrice(calledTile.IsUpgraded).ToString();

            gameObject.SetActive(true);
        }

        public void TogglePopup(TileController calledTile)
        {
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

