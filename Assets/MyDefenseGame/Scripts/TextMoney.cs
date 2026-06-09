using TMPro;
using UnityEngine;

namespace MyDefenseGame
{
    public class TextMoney : MonoBehaviour
    {
        #region Variables
        private TextMeshProUGUI moneyText;
        #endregion

        #region Unity Event Method
        private void Awake()
        {
            moneyText = GetComponent<TextMeshProUGUI>();
        }

        private void OnEnable()
        {
            // GameData의 돈이 바뀌면 UpdateMoneyUI 메서드를 실행하겠다고 등록
            GameData.OnMoneyChanged += UpdateMoneyUI;
        }

        private void OnDisable()
        {
            // 오브젝트가 꺼질 때는 등록 해제 (메모리 누수 방지)
            GameData.OnMoneyChanged -= UpdateMoneyUI;
        }

        private void Start()
        {
            // 게임 시작 시 처음 한 번 돈 표시
            UpdateMoneyUI();
        }
        #endregion

        #region Custom Method
        private void UpdateMoneyUI()
        {
            if (moneyText != null)
            {
                moneyText.text = GameData.Money.ToString("N0"); // 400 -> 400, 1000 -> 1,000
            }
        }
        #endregion
    }
}
