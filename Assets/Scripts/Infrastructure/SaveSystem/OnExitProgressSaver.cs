using System;
using MainMenu;
using ResourceLoaders;

namespace Infrastructure.SaveSystem
{
    public class OnExitProgressSaver
    {
        private SaveSystem _saveSystem;
        private object _objectToSave;
        private GameConfig _gameConfig;
        private Type _type;
        private ResourceLoader _resourceLoader;

        public void Init(object objectToSave, SaveSystem saveSystem, ResourceLoader fileResourceLoader, GameConfig gameConfig)
        {
            _resourceLoader = fileResourceLoader;
            _objectToSave = objectToSave;
            _saveSystem = saveSystem;
            _gameConfig = gameConfig;
        }

        private void OnApplicationQuit() => SaveAndUploadProgress();

        public void Save()
        {
            if (_saveSystem != null) _saveSystem.Save(_objectToSave);
        }

        public async void SaveAndUploadProgress()
        {
            Save();
            await _resourceLoader.Upload(_gameConfig.ProgressSavePath);
        }      
    }
}