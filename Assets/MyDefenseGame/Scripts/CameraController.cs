using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

namespace MyDefenseGame
{
    public class CameraController : MonoBehaviour
    {
        #region Variables
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

        private Vector2 _navigateInputDirection = Vector2.zero;
        private Vector2 _pointerPosition = Vector2.zero;
        private Vector2 _scrollInputDirection = Vector2.zero;
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
            if (!_isCameraControlEnabled) return;
            if (!Application.isFocused) return;

            Vector2 edgeMoveInput = Vector2.zero;

            //포인터(마우스/터치/펜)가 화면 내부에 안전하게 있을 때만 에지 가중치 계산
            if (_pointerPosition.x >= 0 && _pointerPosition.y >= 0 && _pointerPosition.x <= Screen.width && _pointerPosition.y <= Screen.height)
            {
                if (_pointerPosition.x <= _edgeThreshold) edgeMoveInput.x = -1f;
                else if (_pointerPosition.x >= Screen.width - _edgeThreshold) edgeMoveInput.x = 1f;

                if (_pointerPosition.y <= _edgeThreshold) edgeMoveInput.y = -1f;
                else if (_pointerPosition.y >= Screen.height - _edgeThreshold) edgeMoveInput.y = 1f;
            }

            //각 입력 소스로부터 수집한 가중치 벡터를 합산 (같이 누르면 더 빠르게 이동하도록, 반대로 누르면 안 움직이게)
            Vector2 combinedInput = _navigateInputDirection + edgeMoveInput.normalized;

            if (combinedInput != Vector2.zero)
            {
                //카메라의 현재 회전 상태(Transform)를 기준으로 최종 월드 이동 방향을 계산
                Vector3 moveDirection = (transform.right * combinedInput.x) + (transform.forward * combinedInput.y);
                transform.Translate(_speed * Time.deltaTime * moveDirection, Space.World);
            }

            //줌 처리
            if (_childCamera == null) return;
            float scrollDelta = _scrollInputDirection.y;

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
        public void OnNavigate(InputAction.CallbackContext context)
        {
            _navigateInputDirection = context.ReadValue<Vector2>();
        }

        public void OnPoint(InputAction.CallbackContext context)
        {
            _pointerPosition = context.ReadValue<Vector2>();
        }

        public void OnScrollWheel(InputAction.CallbackContext context)
        {
            _scrollInputDirection = context.ReadValue<Vector2>();
        }

        public void OnToggleControl(InputAction.CallbackContext context)
        {
            if (context.performed) _isCameraControlEnabled = !_isCameraControlEnabled;
        }
        #endregion
    }
}
