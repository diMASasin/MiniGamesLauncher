using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infrastructure.SceneLoader
{
    public class SceneNames
    {
        public const string BattleScenePrefix = "Map";
        
        public const string MainMenu = nameof(MainMenu);
        public const string Game1 = nameof(Game1);
        public const string Game2 = nameof(Game2);
    }

    public class SceneLoader : ISceneLoader
    {
        public event Action<float> ProgressChanged;

        public void Load(string name, Action onLoaded = null) => 
            LoadScene(name, onLoaded).Forget();
        
        public void LoadBattleMap(int index, Action onLoaded = null) => 
            LoadScene($"{SceneNames.BattleScenePrefix}{index}", onLoaded).Forget();

        private async UniTaskVoid LoadScene(string name, Action onLoaded = null)
        {
            AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(name);
        
            await UniTask.WaitUntil(() =>
            {
                ProgressChanged?.Invoke(waitNextScene.progress);
                return waitNextScene.isDone == true;
            });
        
            onLoaded?.Invoke();
        }

    }
}