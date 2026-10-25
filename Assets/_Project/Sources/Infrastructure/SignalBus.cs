using System;
using UnityEngine;

namespace Sources.Infrastructure
{
    public static class SignalBus
    {
        public static event Action GameStarted;
        public static void TriggerGameStarted() => GameStarted?.Invoke();

        /// <summary>
        /// Сбрасывать все события, чтобы можно было перезапускать игру без 'Reload domain'.
        /// </summary>
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void Init()
        {
            GameStarted = null;
        }
    }
}