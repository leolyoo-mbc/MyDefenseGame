using TMPro;
using UnityEngine;

namespace MyDefenseGame
{
    public class UIManager : MonoBehaviour
    {
        #region Variables
        [SerializeField] private TMP_Text moneyText;
        [SerializeField] private TMP_Text _livesText;
        [SerializeField] private TMP_Text _roundsText;

        [Header("UI Panels")]
        [SerializeField] private GameObject _gameOverPanel;
        [SerializeField] private TMP_Text _survivedRoundsText;
        [SerializeField] private GameObject _pausePanel;
        #endregion

        #region Unity Event Method
        private void OnEnable()
        {
            // GameData의 돈이 바뀌면 UpdateMoneyUI 메서드를 실행하겠다고 등록
            GameData.OnMoneyChanged += UpdateMoneyUI;
            GameData.OnLivesChanged += UpdateLivesUI;
            GameData.OnRoundsChanged += UpdateRoundsUI;

            GameManager.OnGameOver += ShowGameOverUI;
            GameManager.OnPause += TogglePauseUI;
        }

        private void OnDisable()
        {
            // 오브젝트가 꺼질 때는 등록 해제 (메모리 누수 방지)
            GameData.OnMoneyChanged -= UpdateMoneyUI;
            GameData.OnLivesChanged -= UpdateLivesUI;
            GameData.OnRoundsChanged -= UpdateRoundsUI;

            GameManager.OnGameOver -= ShowGameOverUI;
            GameManager.OnPause -= TogglePauseUI;
        }

        private void Start()
        {
            // 게임 시작 시 3개의 텍스트를 한 번에 초기화합니다.
            UpdateMoneyUI();
            UpdateLivesUI();
            UpdateRoundsUI();

            if (_gameOverPanel != null) _gameOverPanel.SetActive(false);
            if (_pausePanel != null) _pausePanel.SetActive(false);
        }
        #endregion

        #region Custom Method
        private void UpdateMoneyUI()
        {
            if (moneyText != null)
            {
                moneyText.text = $"{GameData.Money:N0} Gold"; //N0: 천 단위마다 쉼표 추가
            }
        }

        private void UpdateLivesUI()
        {
            if (_livesText != null) _livesText.text = $"Lives: {GameData.Lives}";
        }

        private void UpdateRoundsUI()
        {
            // 라운드의 경우 단순히 숫자만 표시할 수도 있고, 꾸밀 수도 있습니다.
            if (_roundsText != null) _roundsText.text = $"Round: {GameData.Rounds}";
        }

        public void OnClickRestart()
        {
            Debug.Log("Run RESTART");
        }

        public void OnClickMainMenu()
        {
            Debug.Log("Goto Menu");
        }

        private void ShowGameOverUI()
        {
            if (_gameOverPanel != null)
            {
                _survivedRoundsText.text = $"{GameData.Rounds} ROUNDS SURVIVED";
                _gameOverPanel.SetActive(true);
            }
        }

        private void TogglePauseUI()
        {
            // GameManager가 정지 상태(true)면 켜지고, 해제 상태(false)면 꺼집니다.
            if (_pausePanel != null) _pausePanel.SetActive(GameManager.IsPaused);

        }
        #endregion
    }
}
