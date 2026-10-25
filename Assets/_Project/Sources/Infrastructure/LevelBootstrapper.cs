using UnityEngine;

namespace Sources.Infrastructure
{
    public class LevelBootstrapper : MonoBehaviour
    {
        private void Awake()
        {
            // Locator.L initializations
        }

        private void Start()
        {
            // game start...
            
            SignalBus.TriggerGameStarted();
        }
    }
}