using Sources.Infrastructure;
using Sources.Services.Windows;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.UI.WindowsSystem
{
    [RequireComponent(typeof(Button))]
    public class OpenWindowButton : MonoBehaviour
    {
        [SerializeField] private WindowId _windowId;
        
        [Tooltip("Окно, которое активировать после закрытия, открываемого окна. Может быть null.")]
        [SerializeField] private WindowBase _payloadWindow = null;

        private WindowService _windowService;
        private Button _button;

        private void Awake()
        {
            _windowService = Locator.G.WindowService;
            _button = GetComponent<Button>();
        }

        private void OnEnable() =>
            _button.onClick.AddListener(OpenWindow);

        private void OnDisable() =>
            _button.onClick.RemoveListener(OpenWindow);

        private void OpenWindow() =>
            _windowService.Open(_windowId, _payloadWindow);
    }
}