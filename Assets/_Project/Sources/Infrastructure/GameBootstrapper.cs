using BaseSettings.Scripts;
using BaseSettings.Scripts.Data;
using Sources.Data;
using Sources.Data.ScriptableObjects;
using Sources.Data.ScriptableObjects.UI;
using Sources.Services.Data;
using Sources.Services.Factories;
using Sources.Services.Input;
using Sources.Services.Scene;
using Sources.Services.Windows;
using Sources.UI.Utilities.Sounds;
using UnityEngine;

namespace Sources.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour
    {
        [SerializeField] private SettingsDataInitializer _settingsDataInitializer;
        [SerializeField] private PlayerSettingsInterpreter _playerSettingsInterpreter;
        [SerializeField] private int _nextSceneIndex = 1;
        [Space]
        [SerializeField] private GlobalGameSettings _globalGameSettings;
        [SerializeField] private WindowsList _windowsList;
        [SerializeField] private CoroutineRunner _coroutineRunner;
        [SerializeField] private UISoundsSource _uiSoundsSource;
        
        private SceneService _sceneService;
        private IDataService _dataService;

        private void Start()
        {
            RegisterGlobalConfigs();
            RegisterGlobalServices();
            GetServices();
            
            LoadPlayerData();
            _settingsDataInitializer.Initialize();
            _playerSettingsInterpreter.ApplySettingsFromData();

            LoadGame();
        }

        private void RegisterGlobalConfigs()
        {
            Locator.G.GlobalGameSettings = _globalGameSettings;
            Locator.G.WindowsList = _windowsList;
        }

        private void RegisterGlobalServices()
        {
            UIFactory uiFactory = new UIFactory(_windowsList);
            
            Locator.G.DataService = new PlayerPrefsDataService();
            Locator.G.SceneService = new SceneService(_coroutineRunner, uiFactory);
            Locator.G.InputService = new KeyboardMouseInputService();
            Locator.G.WindowService = new WindowService(uiFactory);
            Locator.G.UISoundsSource = _uiSoundsSource;
            Locator.G.CoroutineRunner = _coroutineRunner;
        }

        private void GetServices()
        {
            _sceneService = Locator.G.SceneService;
            _dataService = Locator.G.DataService;
        }

        private void LoadPlayerData()
        {
            if (_dataService.TryLoad(out PlayerData playerData))
                GameData.GameDataContent.PlayerData = playerData;
            else
            {
                PlayerData newData = new PlayerData();
                newData.TestIntValue = 31;
                
                GameData.GameDataContent.PlayerData = newData;
                _dataService.Save(newData);
                
                Debug.LogWarning("Cant load playerData, creating new data");
            }
        }

        private void LoadGame()
        {
            if (GameData.GameDataContent.ReturnSceneIndex != 0)
                _sceneService.Load(GameData.GameDataContent.ReturnSceneIndex, true);
            else
                _sceneService.Load(_nextSceneIndex, true);
        }
    }
}