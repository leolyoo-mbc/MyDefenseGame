using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MyDefenseGame
{
    public class GameManager : MonoBehaviour
    {
        #region Variables
        private int _cheatGoldAmount = 100000;
        [SerializeField] private bool _isCheatMode = false;

        public static event Action OnGameOver;//게임오버 발생 시 외부(UI 등)에 알릴 이벤트
        public static event Action OnPause;
        public static bool IsPaused { get; private set; } = false;
        public bool _isGameOver = false;
        #endregion

        #region Unity Event Method
        private void OnEnable()
        {
            // 라이프가 변할 때마다 체크하도록 구독
            GameData.OnLivesChanged += CheckGameOver;
        }

        private void OnDisable()
        {
            GameData.OnLivesChanged -= CheckGameOver;
        }
        #endregion

        #region Custom Method
        private void CheckGameOver()
        {
            if (GameData.Lives <= 0) GameOver();
        }

        private void GameOver()
        {
            if (_isGameOver) return;
            _isGameOver = true;
            OnGameOver?.Invoke(); // 게임오버 이벤트 발생
        }

        public void OnCheatGold(InputAction.CallbackContext context)
        {
            if (!_isCheatMode) return;
            if (context.performed) GameData.Money += _cheatGoldAmount;
        }

        public void OnCheatGameOver(InputAction.CallbackContext context)
        {
            if (!_isCheatMode) return;

            if (context.performed) GameOver();

        }

        public void OnPauseKeyPressed(InputAction.CallbackContext context)
        {
            // 이미 게임오버 상태라면 일시정지를 하지 못하도록 막음
            if (_isGameOver) return;

            // 중요: 정확히 키가 눌린 순간(performed)에만 작동하도록 제한
            if (context.performed)
            {
                // 1. 상태 반전 (true면 false로, false면 true로)
                IsPaused = !IsPaused;

                // 2. 시간에 반영 (일시정지 상태면 시간 속도 0, 아니면 원래 속도 1)
                Time.timeScale = IsPaused ? 0f : 1f;

                // 3. UI 매니저 등 리스너들에게 일시정지 상태가 바뀌었음을 알림
                OnPause?.Invoke();
            }
        }
        #endregion
    }
}
