using Infrastructure;
using UnityEngine;
using UnityEngine.UI;

namespace ClickerSystem
{
    public class ExitToMainMenuButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
    
        private readonly SceneLoader _sceneLoader = new();
        private void OnEnable() => _button.onClick.AddListener(OnExitButtonClicked);

        private void OnDisable() => _button.onClick.RemoveListener(OnExitButtonClicked);

        private void OnExitButtonClicked()
        {
            _sceneLoader.Load("MainMenu");
        }
    }
}