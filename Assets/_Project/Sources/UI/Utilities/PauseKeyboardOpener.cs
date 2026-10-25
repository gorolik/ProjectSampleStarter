using System.Linq;
using Sources.Infrastructure;
using Sources.Services.Input;
using Sources.Services.Windows;
using Sources.UI.WindowsSystem;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Sources.UI.Utilities
{
    public class PauseKeyboardOpener : MonoBehaviour
    {
        private IInputService _inputService;
        private WindowService _windowService;
        
        private void Awake()
        {
            _inputService = Locator.G.InputService;
            _windowService = Locator.G.WindowService;
        }

        private void OnEnable()
        {
            if (_inputService != null)
                _inputService.InputActions.UI.Cancel.performed += OnKeyPressed;
        }

        private void OnDisable()
        {
            if (_inputService != null)
                _inputService.InputActions.UI.Cancel.performed -= OnKeyPressed;
        }

        private void OnKeyPressed(InputAction.CallbackContext context)
        {
            if (_windowService.OpenedWindows.All(x => x.WindowId != WindowId.Pause))
                _windowService.Open(WindowId.Pause);
        }
    }
}