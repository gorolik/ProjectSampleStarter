using System;
using Sources.Infrastructure;
using Sources.Services.Input;
using Sources.Services.Windows;
using Sources.UI.WindowsSystem;
using UnityEngine;

namespace Sources.Utilities
{
    public class CursorLocker : MonoBehaviour
    {
        [SerializeField] private bool _lockAtStart = true;
        [SerializeField] private bool _unlockAtDestroy = true;
        [SerializeField] private bool _lockAtFocus = true;

        public event Action<bool> CursorStateChanged;

        private bool _isLocked;
        private bool _beforeWindowState;
        private IInputService _inputService;
        private WindowService _windowService;

        public bool IsLocked => _isLocked;
        
        private void Awake()
        {
            _inputService = Locator.G.InputService;
            _windowService = Locator.G.WindowService;
        }

        private void Start()
        {
            Initialize();

            _windowService.OnWindowEnabled += OnWindowEnabled;
            _windowService.OnWindowClosed += OnWindowClosed;
        }

        private void Initialize() =>
            SetCursorLock(_lockAtStart);

        private void OnDestroy()
        {
            if (_unlockAtDestroy)
                SetCursorLock(false);

            if (_windowService != null)
            {
                _windowService.OnWindowEnabled -= OnWindowEnabled;
                _windowService.OnWindowClosed -= OnWindowClosed;
            }
        }

        private void Update()
        {
            if(_inputService != null && _inputService.IsCursorLockButtonPerformed())
                SetCursorLock(!_isLocked);
        }

        private void OnApplicationFocus(bool focus)
        {
            if (focus && _lockAtFocus && _isLocked)
                SetCursorLock(true);
        }

        public void SetCursorLock(bool isLocked, bool byWindowSystem = false)
        {
            _isLocked = isLocked;

            if (!byWindowSystem)
                _beforeWindowState = isLocked;
            
            if (isLocked)
                Cursor.lockState = CursorLockMode.Locked;
            else
                Cursor.lockState = CursorLockMode.None;

            CursorStateChanged?.Invoke(_isLocked);
        }

        private void OnWindowClosed(WindowBase window)
        {
            if (_windowService.OpenedWindows.Count == 0) 
                SetCursorLock(_beforeWindowState, true);
        }

        private void OnWindowEnabled(WindowBase window) => 
            SetCursorLock(!window.ShouldUnlockCursor, true);
    }
}
