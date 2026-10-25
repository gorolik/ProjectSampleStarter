using Sources.Infrastructure;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.UI.Utilities.Sounds
{
    [RequireComponent(typeof(Button))]
    public class ButtonClickSoundSource : MonoBehaviour
    {
        private Button _button;
        private UISoundsSource _uiSoundsSource;
        
        private void Awake()
        {
            _uiSoundsSource = Locator.G.UISoundsSource;
            _button = GetComponent<Button>();
        }

        private void OnEnable() => 
            _button.onClick.AddListener(OnButtonClicked);

        private void OnDisable() => 
            _button.onClick.RemoveListener(OnButtonClicked);

        private void OnButtonClicked()
        {
            if (_uiSoundsSource)
                _uiSoundsSource.PlayButtonClickSound();
            else
                Debug.Log("uiSoundsSource is null, cant play press button sound");
        }
    }
}