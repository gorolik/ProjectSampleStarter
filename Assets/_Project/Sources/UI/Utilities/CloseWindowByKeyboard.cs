using Sources.Infrastructure;
using Sources.Services.Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Sources.UI.WindowsSystem
{
    [RequireComponent(typeof(WindowBase))]
    public class CloseWindowByKeyboard : MonoBehaviour
    {
        private WindowBase _windowBase;
        private IInputService _inputService;
        
        private void Awake()
        {
            _inputService = Locator.G.InputService;
            _windowBase = GetComponent<WindowBase>();
        }

        private void OnEnable() => 
            _inputService.InputActions.UI.Cancel.performed += OnKeyPressed;

        private void OnDisable() => 
            _inputService.InputActions.UI.Cancel.performed -= OnKeyPressed;

        private void OnKeyPressed(InputAction.CallbackContext context) => 
            _windowBase.Close();
    }
}