using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace MySample
{
    public class UITest : MonoBehaviour
    {
        #region Variables
        [SerializeField] private TextMeshProUGUI _textScore;
        private int _score = 100;
        #endregion

        #region Unity Event Method
        private void Start()
        {
            _textScore.text = $"Score: {_score}";
        }
        #endregion

        #region Custom Method
        public void OnFireButtonPressed()
        {
            Debug.Log("발사 하였습니다");
        }

        public void OnJumpButtonPressed()
        {
            Debug.Log("플레이어가 점프하였습니다");
            _score += 10;
            _textScore.text = $"Score: {_score}";
        }
        #endregion
    }
}
