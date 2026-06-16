using UnityEngine;

namespace MyDefenseGame
{

    public class LookAtCamera : MonoBehaviour
    {
        #region Variables
        private Camera mainCamera;
        #endregion
        #region Unity Event Method
        private void Start()
        {
            mainCamera = Camera.main;
        }
        private void Update()
        {
            //카메라를 바라보도록 회전
            //transform.LookAt(mainCamera.transform);

            //카메라의 x포지션을 오브젝트의 x포지션과 동일하게 한다
            Vector3 targetPosition = new Vector3(this.transform.position.x, mainCamera.transform.position.y, mainCamera.transform.position.z);
            transform.LookAt(targetPosition);
        }
        #endregion
    }
}
