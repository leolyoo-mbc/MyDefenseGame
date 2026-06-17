using UnityEngine;
using UnityEngine.SceneManagement;

namespace MyDefenseGame
{
    public class MainMenuManager : MonoBehaviour
    {
        #region Variables 
        [SerializeField] private SceneFader fader;
        #endregion

        #region Unity Event Method
        private void Awake()
        {
            //시작 시 타임스케일 초기화
            Time.timeScale = 1.0f;
            fader.StartFadeIn(1f);
        }
        #endregion

        #region Custom Method
        public void OnClickPlay()
        {
            Debug.Log("Goto PlayScene");
            fader.FadeTo(SceneManager.GetSceneAt(1).name);
        }

        public void OnClickQuit()
        {
            Debug.Log("Game Quit!!");
            Application.Quit(); // 실제 빌드된 게임에서 종료 명령
        }
        #endregion
    }
}
