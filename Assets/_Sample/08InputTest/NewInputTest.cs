using UnityEngine;
using UnityEngine.InputSystem;

namespace MySample
{
    public class NewInputTest : MonoBehaviour
    {
        public void OnMove(InputAction.CallbackContext context)
        {
            Vector2 inputVector = context.ReadValue<Vector2>();
            print($"{inputVector.x}, {inputVector.y}");
        }

        public void OnPoint(InputAction.CallbackContext context)
        {
            Vector2 inputVector = context.ReadValue<Vector2>();
            print($"{inputVector.x}, {inputVector.y}");
        }

        public void OnScrollWheel(InputAction.CallbackContext context)
        {
            Vector2 inputVector = context.ReadValue<Vector2>();
            print($"{inputVector.x}, {inputVector.y}");
        }

        public void OnEscToggle(InputAction.CallbackContext context)
        {
            if (context.started) print("started");
            if (context.performed) print("performed");
            if (context.canceled) print("canceled");
        }
    }
}
