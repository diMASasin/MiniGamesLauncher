using MainMenu_;
using ResourceLoaders;
using UnityEngine;

namespace Infrastructure
{
    public class Bootstrapper : MonoBehaviour
    {
        [SerializeField] private MainMenu _mainMenuPrefab;
    
        private readonly ResourceLoader _clickerResourceLoader = new();
        private readonly ResourceLoader _simulatiorResourceLoader = new();
        private readonly GameStaticData _clickerStaticData = new();
        private readonly GameStaticData _gameStaticData = new();
        private readonly SceneLoader _sceneLoader = new();
    
        private MainMenu _mainMenu;

        private void Awake()
        {
            _mainMenu = Instantiate(_mainMenuPrefab);
        
            DontDestroyOnLoad(this);
        
            _mainMenu.Init(_clickerResourceLoader, _simulatiorResourceLoader, _clickerStaticData, _gameStaticData, _sceneLoader);
        
        }
    }
}