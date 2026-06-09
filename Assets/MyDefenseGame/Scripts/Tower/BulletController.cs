using UnityEngine;

namespace MyDefenseGame
{
    /// <summary>
    /// 탄환을 관리하는 클래스
    /// </summary>
    public class BulletController : ProjectileController
    {
        #region Variables
        [SerializeField] private int _damage = 50;
        #endregion

        #region Custom Method
        protected override void ApplyDamage(GameObject target)
        {
            Debug.Log("Hit Target!!!");
            if (_target == null) return;
            if (target.TryGetComponent(out EnemyController enemy)) enemy.TakeDamage(_damage);
        }
        #endregion
    }
}
