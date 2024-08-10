using ResourceLoaders;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MainMenu_
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _playButton2;
        [SerializeField] private Button _loadButton;
        [SerializeField] private Button _loadButton2;

        private ClickerResourceLoader _clickerResourceLoader;
        private WalkingSimulatiorResourceLoader _simulatiorResourceLoader;

        public void Init(ClickerResourceLoader clickerResourceLoader, WalkingSimulatiorResourceLoader simulatiorResourceLoader)
        {
            _clickerResourceLoader = clickerResourceLoader;
            _simulatiorResourceLoader = simulatiorResourceLoader;
        }
        
        private void OnEnable()
        {
            _playButton.onClick.AddListener(OnPlayButtonClicked);
            _playButton2.onClick.AddListener(OnPlayButtonClicked2);
            _loadButton.onClick.AddListener(OnLoadButtonClicked);
            _loadButton2.onClick.AddListener(OnLoadButtonClicked2);
        }

        private void OnDisable()
        {
            _playButton.onClick.RemoveListener(OnPlayButtonClicked);
            _playButton2.onClick.RemoveListener(OnPlayButtonClicked2);
            _loadButton.onClick.RemoveListener(OnLoadButtonClicked);
            _loadButton2.onClick.RemoveListener(OnLoadButtonClicked2);
        }

        private void OnPlayButtonClicked()
        {
            SceneManager.LoadScene(1);
        }

        private void OnPlayButtonClicked2()
        {
            SceneManager.LoadScene(2);
        }

        private void OnLoadButtonClicked()
        {
            _clickerResourceLoader.Load();
        }

        private void OnLoadButtonClicked2()
        {
            _simulatiorResourceLoader.Load();
        }
    }
}