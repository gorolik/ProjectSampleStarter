using Sources.Infrastructure;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Sources.Utilities
{
    public class ToInitSceneReturner : MonoBehaviour  
    {
        [SerializeField] private bool _returnToThisScene = true; 

        private void Awake()
        {
            if (GameData.Instance == null)
            {
                if (_returnToThisScene) 
                    GameData.GameDataContent.ReturnSceneIndex = SceneManager
                        .GetActiveScene()
                        .buildIndex;

                SceneManager.LoadScene(0);
            }
        }
    }
}