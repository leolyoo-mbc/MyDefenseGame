using UnityEngine;

namespace MyDefenseGame
{
    public class Rotate : MonoBehaviour
    {
        #region Variables
        [SerializeField]
        private Vector3 _rotationSpeed;
        #endregion

        #region Unity Event Method
        private void Update()
        {
            transform.localEulerAngles += _rotationSpeed * Time.deltaTime;
        }
        #endregion
    }
}
