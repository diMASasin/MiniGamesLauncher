using ResourceLoaders;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MainMenu
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _loadButton;

        private ClickerResourceLoader _clickerResourceLoader;
        
        private void OnEnable()
        {
            _playButton.onClick.AddListener(OnPlayButtonClicked);
            _loadButton.onClick.AddListener(OnLoadButtonClicked);
        }

        private void OnDisable()
        {
            _playButton.onClick.RemoveListener(OnPlayButtonClicked);
            _loadButton.onClick.RemoveListener(OnLoadButtonClicked);
        }

        private void OnPlayButtonClicked()
        {
            SceneManager.LoadScene(1);
        }

        private void OnLoadButtonClicked()
        {
            _clickerResourceLoader.Load();
        }
    }
}