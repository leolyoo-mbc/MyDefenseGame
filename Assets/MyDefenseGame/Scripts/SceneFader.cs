using UnityEngine;
using UnityEngine.SceneManagement;
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
        [SerializeField] private float _fadeDuration = 1f;

        private Color _color;
        private bool _isFadingOut = false;
        private string _nextSceneName;
        #endregion

        #region Unity Event Method
        private void Start()
        {
            // 시작 시 완전히 불투명(a:1)에서 시작하여 서서히 투명(a:0)으로 변함
            _color = _faderImage.color;
            _color.a = 1f;
            _faderImage.color = _color;
            _faderImage.raycastTarget = true;
        }

        private void Update()
        {
            if (!_isFadingOut)
            {
                // 페이드 인 (a: 1 -> 0)
                if (_color.a > 0f)
                {
                    _color.a -= Time.deltaTime / _fadeDuration;
                    if (_color.a <= 0f)
                    {
                        _color.a = 0f;
                        _faderImage.raycastTarget = false; // 클릭 가능하게 해제
                    }
                    _faderImage.color = _color;
                }
            }
            else
            {
                // 페이드 아웃 (a: 0 -> 1)
                if (_color.a < 1f)
                {
                    _color.a += Time.deltaTime / _fadeDuration;
                    _faderImage.color = _color;
                }
                else
                {
                    // 페이드 아웃 완료 후 씬 이동
                    SceneManager.LoadScene(_nextSceneName);
                }
            }
        }
        #endregion

        #region Custom Method
        /// <summary>
        /// 다른 씬으로 이동할 때 호출하는 함수
        /// </summary>
        public void LoadScene(string sceneName)
        {
            _nextSceneName = sceneName;
            _isFadingOut = true;
            _faderImage.raycastTarget = true; // 페이드 중 클릭 방지
        }
        #endregion
    }
}