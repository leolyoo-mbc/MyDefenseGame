using UnityEngine;
using UnityEngine.SceneManagement;

namespace MyDefenseGame
{
    public class MainMenuManager : MonoBehaviour
    {
        #region Custom Method
        public void OnClickPlay()
        {
            Debug.Log("Goto PlayScene");
            SceneManager.LoadScene(1);
        }

        public void OnClickQuit()
        {
            Debug.Log("Game Quit!!");
            Application.Quit(); // 실제 빌드된 게임에서 종료 명령
        }
        #endregion
    }
}
