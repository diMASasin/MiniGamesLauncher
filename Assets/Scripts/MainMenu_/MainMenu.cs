using ClickerSystem;
using Firebase.Storage;
using Infrastructure;
using ResourceLoaders;
using RPG;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static ClickerSystem.ClickerBootstrapper;
using static RPG.SimulatorBootstrapper;

namespace MainMenu_
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _playButton2;
        [SerializeField] private Button _loadButton;
        [SerializeField] private Button _loadButton2;
        [SerializeField] private ResourceLoadProgressView _progressView;
        [SerializeField] private ResourceLoadProgressView _progressView2;

        private const string StorageUrl = "gs://minigames-a639c.appspot.com";
        
        private FirebaseStorage _storage;
        
        private ResourceLoader _clickerResourceLoader;
        private ResourceLoader _simulatiorResourceLoader;
        private GameStaticData _gameStaticData;
        private ISceneLoader _sceneLoader;
        private StorageReference _storageReference;

        private void Start()
        {
            if (_gameStaticData != null && _gameStaticData.TryGetAssetBundle(ClickerBundleName, out _))
                _progressView.OnLoaded();

            if (_gameStaticData != null && _gameStaticData.TryGetAssetBundle(SimulatorBundleName, out _)) 
                _progressView2.OnLoaded();

        }

        public void Init(ResourceLoader clickerResourceLoader, ResourceLoader simulatiorResourceLoader,
            GameStaticData clickerStaticData, ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
            _clickerResourceLoader = clickerResourceLoader;
            _gameStaticData = clickerStaticData;
            _simulatiorResourceLoader = simulatiorResourceLoader;

            _storage = FirebaseStorage.DefaultInstance;
            _storageReference = _storage.GetReferenceFromUrl(StorageUrl);
            
            _progressView.Init(_clickerResourceLoader);
            _progressView2.Init(_simulatiorResourceLoader);
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
            bool clickerDataLoaded = _gameStaticData.TryGetAssetBundle(ClickerBundleName, out _);
            
            Debug.Log($"{clickerDataLoaded}");

            if (clickerDataLoaded == false) return;
            
            _sceneLoader.Load("Game1", () =>
            {
                var clicker = FindObjectOfType<ClickerBootstrapper>();
                clicker.Init(_gameStaticData);
            });
        }

        private void OnPlayButtonClicked2()
        {
            bool simulatorDataLoaded = _gameStaticData.TryGetAssetBundle(SimulatorBundleName, out _);
            
            Debug.Log($"{simulatorDataLoaded}");

            if (simulatorDataLoaded == false) return;
            
            _sceneLoader.Load("Game2", () =>
            {
                var clicker = FindObjectOfType<SimulatorBootstrapper>();
                clicker.Init(_gameStaticData);
            });
        }

        private void OnLoadButtonClicked()
        {
            string fileName = ClickerBundleName;
            _clickerResourceLoader.Load(_gameStaticData, _storageReference, fileName);
        }

        private void OnLoadButtonClicked2()
        {
            string fileName = SimulatorBundleName;
            _simulatiorResourceLoader.Load(_gameStaticData, _storageReference, fileName);
        }
    }
}