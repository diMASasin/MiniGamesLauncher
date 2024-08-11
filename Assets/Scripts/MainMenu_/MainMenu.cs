using ClickerSystem;
using Firebase.Storage;
using Infrastructure;
using ResourceLoaders;
using RPG;
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
        [SerializeField] private ResourceLoadProgressView _progressView;
        [SerializeField] private ResourceLoadProgressView _progressView2;

        private const string StorageUrl = "gs://minigames-a639c.appspot.com";
        
        private FirebaseStorage _storage;
        
        private ResourceLoader _clickerResourceLoader;
        private ResourceLoader _simulatiorResourceLoader;
        private GameStaticData _clickerStaticData;
        private GameStaticData _simulatorStaticData;
        private ISceneLoader _sceneLoader;
        private StorageReference _storageReference;

        private void Start()
        {
            if (_clickerStaticData != null && _clickerStaticData.IsLoaded == true) 
                _progressView.OnLoaded();
            
            if (_simulatorStaticData != null && _simulatorStaticData.IsLoaded == true) 
                _progressView2.OnLoaded();

        }

        public void Init(ResourceLoader clickerResourceLoader, ResourceLoader simulatiorResourceLoader,
            GameStaticData clickerStaticData, GameStaticData gameStaticData, ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
            _simulatorStaticData = gameStaticData;
            _clickerResourceLoader = clickerResourceLoader;
            _clickerStaticData = clickerStaticData;
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
            Debug.Log($"{_clickerStaticData.IsLoaded}");

            if (_clickerStaticData.IsLoaded == false) return;
            
            _sceneLoader.Load("Game1", () =>
            {
                var clicker = FindObjectOfType<ClickerBootstrapper>();
                clicker.Init(_clickerStaticData);
            });
        }

        private void OnPlayButtonClicked2()
        {
            Debug.Log($"{_simulatorStaticData.IsLoaded}");

            if (_simulatorStaticData.IsLoaded)
            {
                _sceneLoader.Load("Game2", () =>
                {
                    var clicker = FindObjectOfType<SimulatorBootstrapper>();
                    clicker.Init(_clickerStaticData);
                });
            }
        }

        private void OnLoadButtonClicked()
        {
            _clickerResourceLoader.Load(_clickerStaticData, _storageReference.Child("pizza"));
        }

        private void OnLoadButtonClicked2()
        {
            _simulatiorResourceLoader.Load(_simulatorStaticData, _storageReference.Child("walkingsimulator"));
        }
    }
}