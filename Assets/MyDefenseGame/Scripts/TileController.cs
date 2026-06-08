using System;
using UnityEngine;
using UnityEngine.EventSystems;

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
        #endregion

        #region Custom Method
        public void SetHighlight(bool isOn)
        {
            _renderer.material = isOn ? _hoverMaterial : _defaultMaterial;
        }

        public void TryInstallTower()
        {
            if (_installedTower != null) return;

            _installedTower = BuildManager.Instance.BuildTowerOn(_placementPosition);
        }
        #endregion
    }
}
