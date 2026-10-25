using Sources.Infrastructure;
using Sources.Services.Scene;
using Sources.UI.WindowsSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.UI.Windows
{
    public class MainMenuWindow : WindowBase
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _quitButton;
        [Space] 
        [SerializeField] private int _playSceneIndex = 2;

        private SceneService _sceneService;
        
        private void Awake() => 
            _sceneService = Locator.G.SceneService;

        protected override void SubscribeUpdates()
        {
            _playButton.onClick.AddListener(OnPlayButtonClicked);
            _quitButton.onClick.AddListener(OnQuitButtonClicked);
        }

        protected override void Cleanup()
        {
            _playButton.onClick.RemoveListener(OnPlayButtonClicked);
            _quitButton.onClick.RemoveListener(OnQuitButtonClicked);
        }

        private void OnPlayButtonClicked() => 
            _sceneService.Load(_playSceneIndex, true);

        private void OnQuitButtonClicked() => 
            Application.Quit();
    }
}