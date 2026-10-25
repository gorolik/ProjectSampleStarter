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
    public static class Locator
    {
        private static Global _globalContext = new();
        private static Local _localContext = new();
        
        /// <summary>
        /// Глобальная область видимости.
        /// Объекты доступны с запуска игры.
        /// </summary>
        public static Global G => _globalContext;
        
        /// <summary>
        /// Локальнгая область видимости.
        /// Объекты доступны на определенной сцене.
        /// </summary>
        public static Local L => _localContext;

        /// <summary>
        /// Сброс статического состояния для работы без 'Reload domain'.
        /// </summary>
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void Init()
        {
            _globalContext = new Global();
            _localContext = new Local();
        }

        public class Global
        {
            // Config
            public GlobalGameSettings GlobalGameSettings;
            public WindowsList WindowsList;
        
            // Services
            public IDataService DataService;
            public SceneService SceneService;
            public UIFactory UIFactory;
            public IInputService InputService;
            public WindowService WindowService;
            public UISoundsSource UISoundsSource;
            public CoroutineRunner CoroutineRunner;
        }

        public class Local
        {
            
        }
    }
}