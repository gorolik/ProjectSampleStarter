using System;
using UnityEngine;

namespace Sources.Services.Input
{
    public interface IInputService
    {
        event Action<DeviceType> OnCurrentDeviceChanged;
        GameInputActions InputActions { get; }
        
        bool IsJoystickInUse();
        
        Vector2 GetLookDirection();
        Vector3 GetMoveDirection();
        bool IsCursorLockButtonPerformed();
        
        bool IsFireDown();
        bool IsFireUp();
        bool IsJumpDown();
        bool IsCrouchDown();
        bool IsCrouchUp();
        bool IsRunning();
    }
}
