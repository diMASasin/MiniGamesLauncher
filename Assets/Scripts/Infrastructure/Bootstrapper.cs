using MainMenu_;
using ResourceLoaders;
using UnityEngine;

namespace Infrastructure
{
    public class Bootstrapper : MonoBehaviour
    {
        [SerializeField] private GameMenu _clickerMenu;
        [SerializeField] private GameMenu _simulatorMenu;
        [SerializeField] private GameConfig _clickerConfig;
        [SerializeField] private GameConfig _simulatorConfig;
    
        private readonly ResourceLoader _clickerResourceLoader = new();
        private readonly ResourceLoader _simulatiorResourceLoader = new();
        private readonly GameStaticData _gameStaticData = new();
        private readonly SceneLoader _sceneLoader = new();
    
        private void Awake()
        {
            DontDestroyOnLoad(this);
        
            _clickerMenu.Init(_clickerResourceLoader, _gameStaticData, _sceneLoader, _clickerConfig);
            _simulatorMenu.Init(_simulatiorResourceLoader, _gameStaticData, _sceneLoader, _simulatorConfig);
        }
    }
}