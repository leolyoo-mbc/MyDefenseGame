using UnityEngine;
using UnityEngine.UIElements;

namespace MyDefenseGame
{
    [RequireComponent(typeof(Renderer))]
    public class TileController : MonoBehaviour
    {
        #region Variables
        private Renderer _renderer;
        private Material _defaultMaterial;
        [SerializeField] private Material _hoverMaterial;
        private Vector3 _placementPosition;
        #endregion

        #region Unity Event Method
        private void Awake()
        {
            _renderer = GetComponent<Renderer>();
            _defaultMaterial = _renderer.material;

            float _upperSurfaceOffset = transform.localScale.y / 2f;
            _placementPosition = transform.position + new Vector3(0f, _upperSurfaceOffset, 0f);
        }

        private void OnMouseEnter()
        {
            _renderer.material = _hoverMaterial;
        }

        private void OnMouseExit()
        {
            _renderer.material = _defaultMaterial;
        }

        private void OnMouseDown()
        {
            Debug.Log("마우스 클릭 - 여기에 터렛 설치");
            // 1. 타일의 윗면 높이 계산 (변수로 분리하여 가독성 확보)

            BuildManager.Instance.BuildTurretOn(_placementPosition);
        }
        #endregion

        #region Custom Method

        #endregion
    }
}
