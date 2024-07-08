/*using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

namespace Source.UserInput
{
    public class KeyboardController : MonoBehaviour
    {
        private XRBaseController controller;
        private InputAction selectAction;

        void Awake()
        {
            // Initialize the input action
            selectAction = new InputAction("Select", binding: "<Keyboard>/space");
            selectAction.performed += OnSelectPerformed;
            selectAction.canceled += OnSelectCanceled;
        }

        void OnEnable()
        {
            selectAction.Enable();
        }

        void OnDisable()
        {
            selectAction.Disable();
        }

        private void OnSelectPerformed(InputAction.CallbackContext context)
        {
            Debug.Log("Space key pressed");
            // Handle the action performed (e.g., simulating a button press on the controller)
            if (controller != null)
            {
                controller.SendHapticImpulse(0.5f, 0.1f);
            }
        }

        private void OnSelectCanceled(InputAction.CallbackContext context)
        {
            Debug.Log("Space key released");
            // Handle the action canceled (e.g., simulating button release on the controller)
        }

        public void SetController(XRBaseController xrController)
        {
            controller = xrController;
        }

    }
}*/