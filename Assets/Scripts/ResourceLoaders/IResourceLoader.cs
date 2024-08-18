using System;
using UnityEngine.Networking;

namespace ResourceLoaders
{
    public interface IResourceLoader
    {
        public event Action<float> ProgressChanged;
        public event Action<UnityWebRequest.Result> StatusChanged;      
        public event Action Unloaded;
        
        void Load(string fileName);
    }
}