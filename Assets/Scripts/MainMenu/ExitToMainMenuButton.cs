using Infrastructure;
using Infrastructure.SaveSystem;
using Infrastructure.SceneLoader;
using UnityEngine;
using UnityEngine.UI;

namespace MainMenu
{
    public class ExitToMainMenuButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
    
        private readonly SceneLoader _sceneLoader = new();
        private OnExitProgressSaver _onExitProgressSaver;

        public void Init(OnExitProgressSaver onExitProgressSaver)
        {
            _onExitProgressSaver = onExitProgressSaver;
        }
        
        private void OnEnable() => _button.onClick.AddListener(OnExitButtonClicked);

        private void OnDisable() => _button.onClick.RemoveListener(OnExitButtonClicked);

        private void OnExitButtonClicked()
        {
            _onExitProgressSaver.SaveAndUploadProgress();
            _sceneLoader.Load(SceneNames.MainMenu);
        }
    }
}