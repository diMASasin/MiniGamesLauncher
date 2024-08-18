using System;
using System.Threading.Tasks;
using UnityEngine.Networking;

namespace ResourceLoaders
{
    public interface ILoadMethod
    {
        UnityWebRequest GetWebRequest(Task<Uri> task, string fileName);
        void Unload(string fileName);
        void OnResourceLoaded(UnityWebRequest request, string fileName);
    }

    public interface ILoadMethodActions
    {
        event Action<UnityWebRequest.Result> StatusChanged;
        event Action<float> ProgressChanged;
        event Action Unloaded;
    }
}