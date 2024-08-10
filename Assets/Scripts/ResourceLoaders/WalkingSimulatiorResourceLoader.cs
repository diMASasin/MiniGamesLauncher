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
    public class WalkingSimulatiorResourceLoader : IClickerResourceLoader
    {
        public static GameObject Character;
        public static GameObject Building;

        public void Load()
        {
            FirebaseStorage storage = FirebaseStorage.DefaultInstance;
            StorageReference storageReference = storage.GetReferenceFromUrl("gs://minigames-a639c.appspot.com");
            StorageReference fileReference = storageReference.Child("walkingsimulator");

            fileReference.GetDownloadUrlAsync().ContinueWithOnMainThread(NewMethod);
        }

        private async void NewMethod(Task<Uri> task)
        {
            if (task.IsFaulted || task.IsCanceled) return;

            UnityWebRequest request = await GetModels(task);

            Debug.Log(request.result == UnityWebRequest.Result.Success
                ? $"Recieved "
                : $"Error: {request.error}");

            request.Dispose();
        }

        private async UniTask<UnityWebRequest> GetModels(Task<Uri> task)
        {
            Debug.Log("Download URL: " + task.Result);
                 
            UnityWebRequest request = await UnityWebRequestAssetBundle.GetAssetBundle(task.Result, 0).SendWebRequest();
            
            AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(request);
            Character = bundle.LoadAsset<GameObject>("HumanMale_Character_FREE");
            Building = bundle.LoadAsset<GameObject>("rpgpp_lt_building_04");

            return request;
        }
    }
}