using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Firebase.Extensions;
using Firebase.Storage;
using RPG;
using UnityEngine;
using UnityEngine.Networking;

namespace ResourceLoaders
{
    public class ResourceLoader : IResourceLoader
    {
        private GameStaticData _staticData;
        private string _fileName;

        public event Action<float> ProgressChanged;
        public event Action<UnityWebRequest.Result> StatusChanged;

        public void Load(GameStaticData staticData, StorageReference storageReference, string fileName)
        {
            _fileName = fileName;
            _staticData = staticData;

            var fileReference = storageReference.Child(fileName);
            
            fileReference.GetDownloadUrlAsync().ContinueWithOnMainThread(NewMethod);
        }

        private async void NewMethod(Task<Uri> task)
        {
            if (task.IsFaulted || task.IsCanceled) return;

            UnityWebRequest request = await GetModels(task);

            StatusChanged?.Invoke(request.result);
            
            Debug.Log(request.result == UnityWebRequest.Result.Success
                ? $"Assets recieved successfully"
                : $"Error: {request.error}");

            request.Dispose();
        }

        private async UniTask<UnityWebRequest> GetModels(Task<Uri> task)
        {
            Debug.Log("Download URL: " + task.Result);
                 
            StatusChanged?.Invoke(UnityWebRequest.Result.InProgress);
            UnityWebRequest request = UnityWebRequestAssetBundle.GetAssetBundle(task.Result, 0).SendWebRequest().webRequest;
            
            await UniTask.WaitUntil(() =>
            {
                ProgressChanged?.Invoke(request.downloadProgress);
                return request.isDone == true;
            });
            
            AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(request);
            _staticData.AddAssetBundle(_fileName, bundle);

            return request;
        }
    }
}