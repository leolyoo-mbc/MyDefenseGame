using UnityEngine;
using UnityEngine.InputSystem;

namespace MyDefenseGame
{
    public class GameManager : MonoBehaviour
    {
        #region Variables
        private int _cheatGoldAmount = 100000;
        [SerializeField] private bool _isCheatMode = false;
        #endregion

        #region Custom Method
        public void OnAddCheatGold(InputAction.CallbackContext context)
        {
            if (!_isCheatMode) return;
            Debug.Log("치트 발동!");
            if (context.performed) GameData.Money += _cheatGoldAmount;
        }
        #endregion
    }
}
