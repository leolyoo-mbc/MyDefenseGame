using UnityEngine;
using UnityEngine.UI;

namespace MyDefenseGame
{
    public class LevelSelect : MonoBehaviour
    {
        #region Variables
        [SerializeField] private SceneFader _fader;

        [SerializeField] private Transform _content;
        private Button[] _levelButtons;
        #endregion

        #region Unity Event Method
        private void Start()
        {
            // [과제 4, 5 핵심] 기기에 저장된 유저의 진행도(nowLevel)를 불러옵니다.
            // "ReachedLevel"이라는 이름으로 저장된 값을 찾되, 처음 실행해서 데이터가 없으면 기본값인 1을 반환합니다.
            int nowLevel = PlayerPrefs.GetInt("ReachedLevel", 1);

            _levelButtons = new Button[_content.childCount];

            for (int i = 0; i < _content.childCount; i++)
            {
                Transform child = _content.GetChild(i);
                _levelButtons[i] = child.GetComponent<Button>();

                // i는 0부터 시작하므로, 레벨 1 버튼의 i는 0입니다.
                // 만약 nowLevel이 1이라면, i < 1 조건을 만족하는 0번째 버튼(1레벨)만 true가 됩니다.
                if (i < nowLevel)
                {
                    _levelButtons[i].interactable = true;  // UnLock
                }
                else
                {
                    _levelButtons[i].interactable = false; // Lock
                }
            }
        }
        #endregion

        #region Custom Method
        public void OnClickLevelButton(string sceneName)
        {
            _fader.FadeTo(sceneName);
        }
        #endregion
    }
}