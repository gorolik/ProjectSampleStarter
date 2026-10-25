using BaseSettings.Scripts.Data;
using Sources.Data;
using UnityEngine;

namespace Sources.Infrastructure
{
    public class GameData : MonoBehaviour
    {
        public static GameData Instance { get; private set; }
        public static GameDataContent GameDataContent { get; private set; } = new();

        /// <summary>
        /// Сброс статического состояния для работы без 'Reload domain'.
        /// </summary>
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void Init()
        {
            Instance = null;
            GameDataContent = new();
        }
        
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public class GameDataContent
    {
        public PlayerData PlayerData;
        public PlayerSettingsData PlayerSettingsData;
        public int ReturnSceneIndex;
    }
}