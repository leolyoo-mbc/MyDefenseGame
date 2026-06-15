using UnityEngine;

namespace MyDefenseGame
{
    public class AnimatorTest : MonoBehaviour
    {
        #region Variables
        //Animator 인스턴스 가져오기
        private Animator _animator;
        [SerializeField] private float _animationInterval = 1f;
        private float _animationCooldown;
        #endregion

        #region Unity Event Method
        private void Start()
        {
            _animator = GetComponent<Animator>();
            _animationCooldown = _animationInterval;
        }

        private void Update()
        {
            _animationCooldown = _animationCooldown > Time.deltaTime ? _animationCooldown - Time.deltaTime : 0;
            if (_animationCooldown <= 0)
            {
                RandomFlameAnimation();
                _animationCooldown = _animationInterval;
            }
        }
        #endregion

        #region Custom Method
        private void RandomFlameAnimation()
        {
            //_animator.SetInteger("LightMode", 1);
            //_animator.SetInteger("LightMode", 2);
            //_animator.SetInteger("LightMode", 3);

            int lightMode = Random.Range(1, 4);
            _animator.SetInteger("LightMode", lightMode);
        }
        #endregion
    }
}
