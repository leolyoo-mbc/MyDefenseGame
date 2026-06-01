using UnityEngine;

namespace MyDefenseGame
{
    [RequireComponent(typeof(Renderer))]
    public class TileController : MonoBehaviour
    {
        #region Variables
        private Renderer _renderer;
        private Material _defaultMaterial;
        [SerializeField] private Material _hoverMaterial;
        [SerializeField] private GameObject _towerPrefab;
        #endregion

        #region Unity Event Method
        private void Awake()
        {
            _renderer = GetComponent<Renderer>();
            _defaultMaterial = _renderer.material;
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
            GameObject spawnedTower = Instantiate(_towerPrefab, transform.position, transform.rotation);
        }
        #endregion

        #region Custom Method

        #endregion
    }
}
