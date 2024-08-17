using System;
using Firebase.Storage;
using MainMenu;
using ResourceLoaders;
using UnityEngine;

namespace Infrastructure
{
    [Serializable]
    public class Game
    {
        [SerializeField] private GameMenu _gameMenu;
        [field: SerializeField] public GameConfig Config { get; private set; }

        private FirebaseStorage _storage;
        private ResourceLoader _bundleResourceLoader;
        private ResourceLoader _fileResourceLoader;
    
        public StorageReference StorageReference { get; private set; }
    

        public void Init()
        {
            _storage = FirebaseStorage.DefaultInstance;
            StorageReference = _storage.GetReferenceFromUrl(Config.StorageUrl);
        
            _bundleResourceLoader = new ResourceLoader(StorageReference, new AssetBundlesLoadMethod(new GameStaticData()));
            _fileResourceLoader = new ResourceLoader(StorageReference, new FileLoader());
        
            _gameMenu.Init(_bundleResourceLoader, _fileResourceLoader, this);
        }
    }
}