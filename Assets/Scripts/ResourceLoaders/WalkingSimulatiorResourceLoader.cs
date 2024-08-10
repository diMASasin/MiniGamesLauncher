using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Firebase.Extensions;
using Firebase.Storage;
using UnityEngine;
using UnityEngine.Networking;

namespace ResourceLoaders
{
    public class WalkingSimulatiorResourceLoader : IClickerResourceLoader
    {
        public void Load()
        {
            FirebaseStorage storage = FirebaseStorage.DefaultInstance;
            StorageReference storageReference = storage.GetReferenceFromUrl("gs://minigames-a639c.appspot.com");
            StorageReference pizzaReference = storageReference.Child("Pizza.png");

            pizzaReference.GetDownloadUrlAsync().ContinueWithOnMainThread(NewMethod);
        }

        private async void NewMethod(Task<Uri> task)
        {
            if (task.IsFaulted || task.IsCanceled) return;

            UnityWebRequest request = await GetPizza(task);

            Debug.Log(request.result == UnityWebRequest.Result.Success
                ? $"Recieved {request.downloadHandler.text}"
                : $"Error: {request.error}");

            request.Dispose();
        }

        private async UniTask<UnityWebRequest> GetPizza(Task<Uri> task)
        {
            Debug.Log("Download URL: " + task.Result);
            UnityWebRequest request = UnityWebRequestTexture.GetTexture(task.Result);
            request = await request.SendWebRequest();

            Texture2D texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);

            // Sprite = sprite;

            return request;
        }
    }
}