using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Utilities;

namespace Sources.Services.Input
{
    public class KeyboardMouseInputService : IInputService
    {
        private readonly GameInputActions _inputActions;

        public GameInputActions InputActions => _inputActions;

        public event Action<DeviceType> OnCurrentDeviceChanged;

        private bool _isGamepadUsed;

        public KeyboardMouseInputService()
        {
            _inputActions = new GameInputActions();
            _inputActions.Enable();
            
            InputSystem.onAnyButtonPress.Call(OnAnyButtonPress);
        }

        public bool IsJoystickInUse() => 
            _isGamepadUsed;

        public Vector2 GetLookDirection() => 
            _inputActions.Player.Look.ReadValue<Vector2>();

        public Vector3 GetMoveDirection()
        {
            Vector2 input = _inputActions.Player.Move.ReadValue<Vector2>();
            return new Vector3(input.x, input.y, 0);
        }

        public Vector3 GetRotationDirection() => 
            new(0, 0, _inputActions.Player.Roll.ReadValue<float>());

        public float GetManipulatorRotationAxis() => 
            _inputActions.Player.ManipulatorRotation.ReadValue<float>();

        public bool IsCursorLockButtonPerformed() => 
            _inputActions.Player.SwitchCursorState.WasPerformedThisFrame();

        public bool IsFireDown() => 
            _inputActions.Player.Attack.WasPressedThisFrame();

        public bool IsFireUp() => 
            _inputActions.Player.Attack.WasReleasedThisFrame();

        public bool IsJumpDown() => 
            _inputActions.Player.Jump.WasPressedThisFrame();

        public bool IsCrouchDown() => 
            _inputActions.Player.Crouch.WasPressedThisFrame();

        public bool IsCrouchUp() => 
            _inputActions.Player.Crouch.WasReleasedThisFrame();

        public bool IsRunning() => 
            _inputActions.Player.Sprint.IsPressed();

        private void OnAnyButtonPress(InputControl inputControl)
        {
            if (inputControl.device is Gamepad)
            {
                OnCurrentDeviceChanged?.Invoke(DeviceType.Console);
                _isGamepadUsed = true;
            }
            else
            {
                OnCurrentDeviceChanged?.Invoke(DeviceType.Desktop);
                _isGamepadUsed = false;
            }
        }
    }
}
