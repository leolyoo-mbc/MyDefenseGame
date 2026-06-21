using UnityEngine;

namespace MyDefenseGame
{
    public class Damageable : MonoBehaviour
    {
        #region Variables
        [SerializeField] private float _maxHealth = 100;
        private float _currentHealth;
        private bool _isDead = false;

        private IDeathListener[] _deathListeners;
        private IHealthChangeListener[] _healthChangeListeners;
        #endregion

        #region  Unity Event Method
        private void Awake()
        {
            _currentHealth = _maxHealth;
            // 자신과 자식 오브젝트에 있는 모든 IDeathListener를 찾아서 배열에 저장합니다.
            _deathListeners = GetComponentsInChildren<IDeathListener>();
            _healthChangeListeners = GetComponentsInChildren<IHealthChangeListener>();
        }
        #endregion

        #region Custom Method
        public void TakeDamage(float damageAmount)
        {
            if (_isDead || damageAmount <= 0) return;

            _currentHealth -= damageAmount;
            _currentHealth = Mathf.Max(_currentHealth, 0);

            foreach (var listener in _healthChangeListeners) listener.OnHealthChanged(_currentHealth, _maxHealth);

            if (_currentHealth == 0)
            {
                Die();
            }
        }

        private void Die()
        {
            _isDead = true;

            // 인터페이스를 구현한 모든 컴포넌트에 사망 사실을 전파합니다.
            foreach (var listener in _deathListeners) listener.OnDeath();

        }
        #endregion
    }
}