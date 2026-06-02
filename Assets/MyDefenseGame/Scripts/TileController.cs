using UnityEngine;
using UnityEngine.EventSystems;

namespace MyDefenseGame
{
    [RequireComponent(typeof(Renderer))]
    public class TileController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        #region Variables
        private Renderer _renderer;
        private Material _defaultMaterial;
        [SerializeField] private Material _hoverMaterial;
        private Vector3 _placementPosition;
        private GameObject _installedTower;
        #endregion

        #region Unity Event Method
        private void Awake()
        {
            _renderer = GetComponent<Renderer>();
            _defaultMaterial = _renderer.material;

            float _upperSurfaceOffset = transform.localScale.y / 2f;
            _placementPosition = transform.position + new Vector3(0f, _upperSurfaceOffset, 0f);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _renderer.material = _hoverMaterial;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _renderer.material = _defaultMaterial;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (_installedTower != null) return;//타워가 이미 설치되어 있으면 리턴
            Debug.Log("마우스 클릭 - 여기에 터렛 설치");
            _installedTower = BuildManager.Instance.BuildTowerOn(_placementPosition);
        }
        #endregion

        #region Custom Method

        #endregion
    }
}
