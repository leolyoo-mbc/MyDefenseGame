using System;
using UnityEngine;

namespace MyDefenseGame
{
    /// <summary>
    /// 현재 감지된 타겟 방향으로 터렛을 회전시키는 클래스
    /// </summary>
    public class TowerTurretRotator : MonoBehaviour
    {
        #region Variables
        [SerializeField] private TowerTargetDetector _targetDetector;
        [SerializeField] private Transform _turretRotator;
        [SerializeField] private float _rotationSpeed = 10f;
        #endregion

        #region Unity Event Method
        private void Update()
        {
            if (_targetDetector != null)
            {
                if (_targetDetector.GetCurrentTarget() != null)
                {
                    //타겟으로 터렛을 회전
                    RotateTurretToTarget(_targetDetector.GetCurrentTarget());
                }
            }
        }
        #endregion

        #region Custom Method
        /// <summary>
        /// 타겟으로 터렛을 회전시키는 메서드
        /// </summary>
        /// <param name="target">회전시킬 타겟의 Transform</param>
        private void RotateTurretToTarget(Transform target)
        {
            Vector3 directionToTarget = target.position - _turretRotator.position;
            Quaternion targetLookRotation = Quaternion.LookRotation(directionToTarget);

            // 1. Lerp로 부드럽게 조준하되
            _turretRotator.rotation = Quaternion.Slerp(_turretRotator.rotation, targetLookRotation, Time.deltaTime * _rotationSpeed);

            // 2. 💡 [실무 필수 체크] 목표 각도와 현재 각도의 차이가 0.01도 이하로 좁혀졌다면?
            if (Quaternion.Angle(_turretRotator.rotation, targetLookRotation) < 0.01f)
            {
                // 3. 강제로 목적지 값(100%)을 꽂아넣어 연산을 완전히 끝냅니다.
                _turretRotator.rotation = targetLookRotation;
            }
        }
        #endregion
    }
}
