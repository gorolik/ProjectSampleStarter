using System;
using Sources.Infrastructure;
using Sources.Services.Windows;
using Sources.UI.WindowsSystem;
using UnityEngine;

namespace Sources.UI.MainMenu
{
    public class MainMenuBootstrapper : MonoBehaviour
    {
        private WindowService _windowService;
        
        private void Awake()
        {
            _windowService = Locator.G.WindowService;
        }

        private void Start()
        {
            _windowService.Open(WindowId.MainMenu);
        }
    }
}