using UnityEngine;
namespace MySample
{
    public class UITest : MonoBehaviour
    {
        #region Custom Method
        public void OnFireButtonPressed()
        {
            Debug.Log("발사 하였습니다");
        }

        public void OnJumpButtonPressed()
        {
            Debug.Log("플레이어가 점프하였습니다");
        }
        #endregion
    }
}
