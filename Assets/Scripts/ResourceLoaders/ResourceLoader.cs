using System;
using System.Collections.Generic;
using System.IO;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Firebase.Extensions;
using Firebase.Storage;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;

namespace ResourceLoaders
{
    public class ResourceLoader : ILoadMethodActions
    {
        private readonly StorageReference _storageReference;
        private readonly GameStaticData _staticData = new();

        private string _fileName;
        private ILoadMethod _loadMethod;

        public event Action<UnityWebRequest.Result> StatusChanged;
        public event Action<float> ProgressChanged;
        public event Action Unloaded;

        public ResourceLoader(StorageReference storageReference, ILoadMethod loadMethod)
        {
            _loadMethod = loadMethod;
            _storageReference = storageReference;
        }

        public async UniTask Load(string fileName)
        {
            _fileName = fileName;
            
            if(Application.internetReachability == NetworkReachability.NotReachable)
                throw new InvalidConnectionException("Network not reachable");

            StorageReference fileReference = _storageReference.Child(fileName);
            
            await fileReference.GetDownloadUrlAsync().ContinueWithOnMainThread(async task =>
            {
                if (task.IsFaulted || task.IsCanceled)
                    throw new TaskCanceledException("Task cancelled");

                UnityWebRequest webRequest = _loadMethod.GetWebRequest(task, _fileName);
            
                StatusChanged?.Invoke(UnityWebRequest.Result.InProgress);

                await UniTask.WaitUntil(() =>
                {
                    ProgressChanged?.Invoke(webRequest.downloadProgress);
                    return webRequest.isDone == true;
                });
            
                Debug.Log("Download URL: " + task.Result);
            
                StatusChanged?.Invoke(UnityWebRequest.Result.Success);
                
                _loadMethod.OnResourceLoaded(webRequest, _fileName);
            });
        }

        public void Unload(string fileName)
        {
            _loadMethod.Unload(fileName);
            
            Unloaded?.Invoke();
        }

        public async UniTask Upload(string fileName)
        {
            if (Application.internetReachability == NetworkReachability.NotReachable)
                throw new InvalidConnectionException("Network not reachable");

            StorageReference fileReference = _storageReference.Child(fileName);

            await fileReference.PutFileAsync(fileName).ContinueWith(task =>
            {
                Debug.Log($"Finished uploading... file name = {fileName}");
            });
        }

    }
}