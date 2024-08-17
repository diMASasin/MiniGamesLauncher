using System;
using Cysharp.Threading.Tasks;
using Infrastructure;
using Infrastructure.SceneLoader;
using ResourceLoaders;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MainMenu
{
    public class GameMenu : MonoBehaviour
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _loadButton;
        [SerializeField] private Button _unloadButton;
        [SerializeField] private TMP_Text _errorText;
        [SerializeField] private TMP_InputField _inputField;
        [SerializeField] private ResourceLoadProgressView _progressView;

        private readonly GameStaticData _gameStaticData = new();
        private readonly ISceneLoader _sceneLoader = new SceneLoader();

        private GameConfig _config;
        private AssetBundlesLoadMethod _assetBundlesLoadMethod;
        private FileLoader _fileLoadMethod;
        private ResourceLoader _fileResourceLoader;
        private bool _isResourcesLoading = true;
        private ResourceLoader _bundleResourceLoader;
        private string _nickname = "Player 1";

        private void Start()
        {
            if (_gameStaticData != null && _gameStaticData.TryGetAssetBundle(_config.BundleName, out _))
                _progressView.OnLoaded();

            _inputField.text = _nickname;
        }

        public void Init(ResourceLoader bundleResourceLoader, ResourceLoader fileResourceLoader, Game game)
        {
            _config = game.Config;
            _bundleResourceLoader = bundleResourceLoader;
            _fileResourceLoader = fileResourceLoader;

            _assetBundlesLoadMethod = new AssetBundlesLoadMethod(_gameStaticData);
            _fileLoadMethod = new FileLoader();

            _progressView.Init(_fileResourceLoader);
        }

        private void OnEnable()
        {
            _playButton.onClick.AddListener(OnPlayButtonClicked);
            _loadButton.onClick.AddListener(OnLoadButtonClicked);
            _unloadButton.onClick.AddListener(OnUnloadButtonClicked);
            _inputField.onValueChanged.AddListener(OnNicknameChanged);
        }

        private void OnDisable()
        {
            _playButton.onClick.RemoveListener(OnPlayButtonClicked);
            _loadButton.onClick.RemoveListener(OnLoadButtonClicked);
            _unloadButton.onClick.RemoveListener(OnUnloadButtonClicked);
            _inputField.onValueChanged.RemoveListener(OnNicknameChanged);
        }

        private void OnNicknameChanged(string nickname) => _nickname = nickname;

        private void OnPlayButtonClicked()
        {
            bool gameDataLoaded = _gameStaticData.TryGetAssetBundle(_config.BundleName, out _);

            Debug.Log($"{gameDataLoaded}");

            if (gameDataLoaded == false)
            {
                SetErrorText("Load data first").Forget();
                return;
            }

            _sceneLoader.Load(_config.SceneName, () =>
            {
                var gameBootstrapper = FindObjectOfType<GameBootstrapper>();
                gameBootstrapper.Init(_config, _fileResourceLoader, _nickname);
            });
        }

        private async void OnLoadButtonClicked()
        {
            if(_isResourcesLoading == false)
                return;
            
            try
            {
                _isResourcesLoading = false;
                
                await _bundleResourceLoader.Load(_config.BundleName);
                _fileResourceLoader.Load(_config.ProgressSavePath).Forget();

                _isResourcesLoading = true;
            }
            catch (Exception exception)
            {
                SetErrorText(exception.Message).Forget();
                
                if(exception is ArgumentException)
                    _progressView.OnLoaded();
            }
        }

        private async void OnUnloadButtonClicked()
        {
            try
            {
                await _fileResourceLoader.Upload(_config.ProgressSavePath);
                _fileResourceLoader.Unload(_config.ProgressSavePath);
                _bundleResourceLoader.Unload(_config.BundleName);
            }
            catch (Exception exception)
            {
                SetErrorText(exception.Message).Forget();
            }
        }

        private async UniTaskVoid SetErrorText(string exception)
        {
            _errorText.text = exception;
            await UniTask.Delay(TimeSpan.FromSeconds(1));

            _errorText.text = string.Empty;
        }
    }
}