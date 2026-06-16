using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace MyDefenseGame
{
    /// <summary>
    /// 씬 전환 시 페이드 효과를 관리하는 클래스
    /// 씬 시작 시 페이드 인, 씬 종료 시 페이드 아웃을 담당
    /// 페이드 아웃 시 다음 씬으로 전환하는 기능도 포함
    /// </summary>
    public class SceneFader : MonoBehaviour
    {
        #region Variables
        [SerializeField] private Image _faderImage;
        #endregion

        #region Unity Event Method

        #endregion

        #region Custom Method
        public IEnumerator FadeIn()
        {
            _faderImage.color = new Color(0, 0, 0, 1); // 시작: 검정
            float time = 0f;
            while (time < 1f)
            {
                time += Time.deltaTime;
                _faderImage.color = new Color(0, 0, 0, Mathf.Lerp(1f, 0f, time));
                yield return null;
            }
            _faderImage.color = new Color(0, 0, 0, 0); // 완료: 투명
        }
        #endregion
    }
}