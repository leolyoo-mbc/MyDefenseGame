using UnityEngine;

namespace MySample
{
    public class OldInputTest : MonoBehaviour
    {
        #region Unity Event Method
        private void Update()
        {
            if (Input.GetKey(KeyCode.W))
            {
                Debug.Log("w키를 누르고 있습니다.");
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                Debug.Log("w키를 눌렀습니다.");
            }
            if (Input.GetKeyUp(KeyCode.W))
            {
                Debug.Log("w키를 눌렀다가 떼었습니다.");
            }
            if (Input.GetButton("Jump"))
            {
                Debug.Log("Jump키를 누르고 있습니다.");
            }
            if (Input.GetButtonDown("Jump"))
            {
                Debug.Log("Jump키를 눌렀습니다.");
            }
            if (Input.GetButtonUp("Jump"))
            {
                Debug.Log("Jump키를 눌렀다가 떼었습니다.");
            }

            float hValue = Input.GetAxis("Horizontal");
            if (hValue != 0) Debug.Log($"Horizontal GetAxis value: {hValue}");
            float vValue = Input.GetAxis("Vertical");
            if (vValue != 0) Debug.Log($"Vertical GetAxis value: {vValue}");

            float mouseX = Input.mousePosition.x;
            float mouseY = Input.mousePosition.y;
            Debug.Log($"Mouse Position: ({mouseX}, {mouseY})");
        }
        #endregion
    }
}
