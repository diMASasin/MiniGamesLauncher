using System;

namespace Infrastructure
{
    public interface ISceneLoader
    {
        public SceneNames SceneNames { get; }

        public event Action<float> ProgressChanged;
        
        void Load(string name, Action onLoaded = null);
        void LoadBattleMap(int index, Action onLoaded = null);
    }
}