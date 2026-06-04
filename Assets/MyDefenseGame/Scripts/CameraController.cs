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
        [Tooltip("Cancel 인풋 액션 레퍼런스")]
        [SerializeField] private InputActionReference _cancelAction;

        [Tooltip("카메라 이동 속도")]
        [SerializeField] private float _speed = 10f;
        [Tooltip("화면 가장자리 기준 폭")]
        [SerializeField] private float _edgeThreshold = 10f;

        [Header("Zoom Settings")]
        [Tooltip("줌 속도")]
        [SerializeField] private float _zoomSpeed = 30f;
        [Tooltip("카메라의 바닥으로부터 최소 높이")]
        [SerializeField] private float _minCameraHeight = 5f;
        [Tooltip("자동 계산 실패 시 사용할 예비(Fallback) 최대 줌 거리")]
        [SerializeField] private float _fallbackMaxZoomDistance = 30f;
        private float _maxZoomDistance;
        private float _currentZoom = 0f;

        private Camera _childCamera;
        private bool _isCameraControlEnabled = true;
        #endregion

        #region Unity Event Method
        private void Awake()
        {
            //자식 오브젝트에 있는 카메라 컴포넌트를 컴파일 시점에 안전하게 가져옵니다.
            _childCamera = GetComponentInChildren<Camera>();

            // 1. 허용된 최대 수직 낙하 거리
            float maxDescentHeight = transform.position.y - _minCameraHeight;

            // 2. 카메라가 바라보는 대각선 방향 벡터 추출
            Vector3 zoomDirection = _childCamera.transform.localRotation * Vector3.forward;

            // 3. 카메라가 수평이거나 하늘을 향해 바닥에 닿을 수 없는 경우 예외 처리
            if (zoomDirection.y >= 0f) _maxZoomDistance = _fallbackMaxZoomDistance;
            // 4. (총 낙하 거리 / 1단위당 낙하 비율) 공식을 통해 지면에 닿는 최대 줌 대각선 길이 도출
            else _maxZoomDistance = maxDescentHeight / Mathf.Abs(zoomDirection.y);
        }

        private void Update()
        {
            if (_cancelAction.action.WasPressedThisFrame()) _isCameraControlEnabled = !_isCameraControlEnabled;
            if (!_isCameraControlEnabled) return;

            //각 입력 소스로부터 수집한 가중치 벡터를 합산 (같이 누르면 더 빠르게 이동하도록, 반대로 누르면 안 움직이게)
            Vector2 combinedInput = GetPointActionInput() + GetNavigateActionInput();

            if (combinedInput != Vector2.zero)
            {
                //카메라의 현재 회전 상태(Transform)를 기준으로 최종 월드 이동 방향을 계산
                Vector3 moveDirection = (transform.right * combinedInput.x) + (transform.forward * combinedInput.y);
                transform.Translate(_speed * Time.deltaTime * moveDirection, Space.World);
            }

            //줌 처리
            if (_childCamera == null) return;
            //휠의 y축 입력 값(-1 또는 1)을 읽어옵니다.
            float scrollDelta = _scrollWheelAction.action.ReadValue<Vector2>().y;

            // 1. 이번 프레임에 전진/후진할 '거리'를 계산합니다. (위로 굴리면 양수, 아래는 음수)
            float zoomStep = scrollDelta * _zoomSpeed * Time.deltaTime;

            // 2. 현재 누적 거리에 더해보고, 0 ~ 최대 줌인 거리 사이를 절대 벗어나지 않도록 가둡니다.
            _currentZoom += zoomStep;
            _currentZoom = Mathf.Clamp(_currentZoom, 0f, _maxZoomDistance);

            // 3. 자식 카메라가 바라보는 50도 각도의 '대각선 방향 벡터'를 추출합니다.
            Vector3 zoomDirection = _childCamera.transform.localRotation * Vector3.forward;

            // 4. 추출한 50도 대각선 방향에 한계가 적용된 거리를 곱해 최종 위치에 꽂아 넣습니다.
            _childCamera.transform.localPosition = zoomDirection * _currentZoom;
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
