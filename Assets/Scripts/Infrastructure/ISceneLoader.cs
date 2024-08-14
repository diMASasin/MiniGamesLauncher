using System;

namespace Infrastructure
{
    public interface ISceneLoader
    {
        public event Action<float> ProgressChanged;
        
        void Load(string name, Action onLoaded = null);
        void LoadBattleMap(int index, Action onLoaded = null);
    }
}