using UnityEngine;

namespace MyDefenseGame
{
    /// <summary>
    /// 탄환을 관리하는 클래스
    /// </summary>
    public class BulletController : ProjectileController
    {
        #region Custom Method
        protected override void ApplyDamage()
        {
            Debug.Log("Hit Target!!!");
            if (_target != null) Destroy(_target.gameObject);
        }
        #endregion
    }
}
