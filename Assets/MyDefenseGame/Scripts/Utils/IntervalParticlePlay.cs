using UnityEngine;

namespace MyDefenseGame
{
    public class IntervalParticlePlay : MonoBehaviour
    {
        #region Variables
        [Tooltip("재생할 파티클 시스템 (지정하지 않으면 현재 게임 오브젝트의 파티클 시스템을 사용합니다)")]
        [SerializeField] private ParticleSystem _effectParticle;
        [SerializeField] private float _effectInterval;
        [SerializeField] private float _initialDelay;
        private float _effectCooldown;
        #endregion

        #region Unity Event Method
        private void Start()
        {
            _effectCooldown = _initialDelay;

            if (_effectParticle == null)
            {
                if (!TryGetComponent(out _effectParticle))
                {
                    Debug.LogWarning("IntervalParticlePlay: No ParticleSystem assigned and none found on the GameObject. Disabling script.");
                    enabled = false;
                }
                ;
            }
        }

        private void Update()
        {
            _effectCooldown = _effectCooldown > Time.deltaTime ? _effectCooldown - Time.deltaTime : 0f;
            if (_effectCooldown <= 0f)
            {
                _effectParticle.Play();
                _effectCooldown = _effectInterval;
            }
        }
        #endregion
    }
}
