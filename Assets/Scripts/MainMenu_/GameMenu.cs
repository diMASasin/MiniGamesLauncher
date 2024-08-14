using ClickerSystem;
using Firebase.Storage;
using Infrastructure;
using ResourceLoaders;
using UnityEngine;
using UnityEngine.UI;

namespace MainMenu_
{
    public class GameMenu : MonoBehaviour
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _loadButton;
        [SerializeField] private Button _unloadButton;
        [SerializeField] private ResourceLoadProgressView _progressView;

        private FirebaseStorage _storage;

        private ResourceLoader _resourceLoader;
        private GameStaticData _gameStaticData;
        private ISceneLoader _sceneLoader;
        private StorageReference _storageReference;
        private GameConfig _config;

        private void Start()
        {
            if (_gameStaticData != null && _gameStaticData.TryGetAssetBundle(_config.BundleName, out _))
                _progressView.OnLoaded();
        }

        public void Init(ResourceLoader resourceLoader, GameStaticData gameStaticData, ISceneLoader sceneLoader, 
            GameConfig config)
        {
            _config = config;   
            _sceneLoader = sceneLoader;
            _resourceLoader = resourceLoader;
            _gameStaticData = gameStaticData;

            _storage = FirebaseStorage.DefaultInstance;
            _storageReference = _storage.GetReferenceFromUrl(_config.StorageUrl);

            _progressView.Init(_resourceLoader);
        }

        private void OnEnable()
        {
            _playButton.onClick.AddListener(OnPlayButtonClicked);
            _loadButton.onClick.AddListener(OnLoadButtonClicked);
            _unloadButton.onClick.AddListener(OnUnloadButtonClicked);
        }

        private void OnDisable()
        {
            _playButton.onClick.RemoveListener(OnPlayButtonClicked);
            _loadButton.onClick.RemoveListener(OnLoadButtonClicked);
            _unloadButton.onClick.RemoveListener(OnUnloadButtonClicked);
        }

        private void OnPlayButtonClicked()
        {
            bool clickerDataLoaded = _gameStaticData.TryGetAssetBundle(_config.BundleName, out _);

            Debug.Log($"{clickerDataLoaded}");

            if (clickerDataLoaded == false) return;

            _sceneLoader.Load(_config.SceneName, () =>
            {
                var gameBootstrapper = FindObjectOfType<GameBootstrapper>();
                gameBootstrapper.Init(_gameStaticData, _config);
            });
        }

        private void OnLoadButtonClicked()
        {
            string fileName = _config.BundleName;
            _resourceLoader.Load(_gameStaticData, _storageReference, fileName);
        }

        private void OnUnloadButtonClicked()
        {
            _resourceLoader.Unload(_config.BundleName);
        }

        [ContextMenu("Upload")]
        private void Upload()
        {
            _resourceLoader.Upload(_storageReference, _config.ProgressSavePath);
        }
    }
}