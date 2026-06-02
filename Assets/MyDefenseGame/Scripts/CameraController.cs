using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MyDefenseGame
{
    public class CameraController : MonoBehaviour
    {
        #region Variables
        [Tooltip("UI/Navigate 인풋 액션 레퍼런스")]
        [SerializeField] private InputActionReference _navigateAction;
        [Tooltip("UI/Point 인풋 액션 레퍼런스")]
        [SerializeField] private InputActionReference _pointAction;
        [Tooltip("UI/ScrollWheel 인풋 액션 레퍼런스")]
        [SerializeField] private InputActionReference _scrollWheelAction;

        [Tooltip("카메라 이동 속도")]
        [SerializeField] private float _speed = 10f;
        [Tooltip("화면 가장자리 기준 폭")]
        [SerializeField] private float _edgeThreshold = 10f;
        #endregion

        #region Unity Event Method
        private void Update()
        {
            //각 입력 소스로부터 수집한 가중치 벡터를 합산
            Vector2 combinedInput = GetPointActionInput() + GetNavigateActionInput();

            if (combinedInput != Vector2.zero)
            {
                //카메라의 현재 회전 상태(Transform)를 기준으로 최종 월드 이동 방향을 계산
                Vector3 moveDirection = (transform.right * combinedInput.x) + (transform.forward * combinedInput.y);
                transform.Translate(_speed * Time.deltaTime * moveDirection, Space.World);
            }
        }
        #endregion

        #region CustomMethod
        /// <summary>
        /// 포인터의 위치를 기반으로 화면 가장자리 스크롤 입력 값을 계산하는 메서드
        /// </summary>
        /// <returns>
        /// 화면 내부 경계 조건에 따라 계산된 정규화된 이동 방향 벡터(Vector2)를 반환 
        /// 마우스가 화면 영역을 완전히 벗어난 경우 Vector2.zero를 반환
        /// </returns>
        private Vector2 GetPointActionInput()
        {
            Vector2 input = Vector2.zero;
            Vector2 pointAmt = _pointAction.action.ReadValue<Vector2>();

            if (pointAmt.x < 0 || pointAmt.y < 0 || pointAmt.x > Screen.width || pointAmt.y > Screen.height) return input;

            // 순수한 가중치(-1, 1)만 대입
            if (pointAmt.x <= _edgeThreshold) input.x = -1f;
            else if (pointAmt.x >= Screen.width - _edgeThreshold) input.x = 1f;

            if (pointAmt.y <= _edgeThreshold) input.y = -1f;
            else if (pointAmt.y >= Screen.height - _edgeThreshold) input.y = 1f;

            return input.normalized;
        }

        /// <summary>
        /// 키보드나 게임패드 등의 내비게이션 조작을 통한 이동 입력 값을 가져오는 메서드
        /// </summary>
        /// <returns>
        /// Input System의 Navigate 액션으로부터 읽어온 -1에서 1 사이의 순수 Vector2 입력 값
        /// </returns>
        private Vector2 GetNavigateActionInput()
        {
            return _navigateAction.action.ReadValue<Vector2>();
        }
        #endregion
    }
}
