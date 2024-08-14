using System;
using System.Collections.Generic;
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
        public event Action Unloaded;

        public void Load(GameStaticData staticData, StorageReference storageReference, string fileName)
        {
            _fileName = fileName;
            _staticData = staticData;

            var fileReference = storageReference.Child(fileName);

            fileReference.GetDownloadUrlAsync().ContinueWithOnMainThread(RequestLoad);
        }

        public void Unload(string name)
        {
            if (_staticData.TryGetAssetBundle(name, out var assetBundle) == false)
                throw new KeyNotFoundException(nameof(name));

            assetBundle.Unload(true);
            Unloaded?.Invoke();
        }

        public void Upload(StorageReference storageReference, string fileName)
        {
            StorageReference fileReference = storageReference.Child(fileName);

            fileReference.PutFileAsync(fileName).ContinueWith(task =>
            {
                if (!task.IsFaulted && !task.IsCanceled)
                {
                    Debug.Log(task.Exception.ToString());
                    return;
                }

                StorageMetadata metadata = task.Result;
                string md5Hash = metadata.Md5Hash;
                Debug.Log("Finished uploading...");
                Debug.Log("md5 hash = " + md5Hash);
            });
        }

        private async void RequestLoad(Task<Uri> task)
        {
            if (task.IsFaulted || task.IsCanceled) return;

            UnityWebRequest request = await LoadBundles(task);

            StatusChanged?.Invoke(request.result);

            Debug.Log(request.result == UnityWebRequest.Result.Success
                ? $"Assets recieved successfully"
                : $"Error: {request.error}");

            request.Dispose();
        }

        private async UniTask<UnityWebRequest> LoadBundles(Task<Uri> task)
        {
            Debug.Log("Download URL: " + task.Result);

            StatusChanged?.Invoke(UnityWebRequest.Result.InProgress);
            UnityWebRequest request =
                UnityWebRequestAssetBundle.GetAssetBundle(task.Result, 0).SendWebRequest().webRequest;

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