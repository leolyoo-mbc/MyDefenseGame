using UnityEngine;
using UnityEngine.EventSystems;

namespace MyDefenseGame
{
    public class TileInputManager : MonoBehaviour
    {
        #region Variables
        [SerializeField] private LayerMask _tileLayer; // 타일 전용 레이어 설정 필수
        [SerializeField] private float _maxRayDistance = 1000f;
        private TileController _lastHoveredTile;
        #endregion

        #region Unity Event Method
        private void Update()
        {
            //1. UI 관통 방지
            if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
            {
                ClearHover();
                return;
            }

            //2. 레이캐스트 실행
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, _maxRayDistance, _tileLayer))
            {
                if (hit.collider.TryGetComponent<TileController>(out var currentTile))
                {
                    //A. 호버링 처리
                    if (currentTile != _lastHoveredTile)
                    {
                        ClearHover();
                        _lastHoveredTile = currentTile;

                        if (BuildManager.Instance.IsTowerSelected)
                            _lastHoveredTile.SetHighlight(true);
                    }

                    //B. 클릭 처리
                    if (Input.GetMouseButtonDown(0) && BuildManager.Instance.IsTowerSelected)
                    {
                        _lastHoveredTile.TryInstallTower();
                    }
                }
            }
            else
            {
                ClearHover();
            }
        }
        #endregion

        #region Custom Method
        private void ClearHover()
        {
            if (_lastHoveredTile != null)
            {
                _lastHoveredTile.SetHighlight(false);
                _lastHoveredTile = null;
            }
        }
        #endregion
    }
}