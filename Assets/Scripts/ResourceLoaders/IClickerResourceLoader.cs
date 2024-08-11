using System;
using Firebase.Storage;
using UnityEngine.Networking;

namespace ResourceLoaders
{
    public interface IResourceLoader
    {
        public event Action<float> ProgressChanged;
        public event Action<UnityWebRequest.Result> StatusChanged;

        void Load(GameStaticData staticData, StorageReference storageReference, string fileName);
    }
}