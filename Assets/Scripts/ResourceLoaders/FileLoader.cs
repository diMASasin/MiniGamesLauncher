using System;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace ResourceLoaders
{
    public class FileLoader : ILoadMethod
    {
        private static string _fileName;

        public UnityWebRequest GetWebRequest(Task<Uri> task, string fileName)
        {
            UnityWebRequest request = UnityWebRequest.Get(task.Result).SendWebRequest().webRequest;
            
            _fileName = fileName;
            return request;
        }

        public void OnResourceLoaded(UnityWebRequest request, string fileName)
        {
            File.WriteAllText(fileName, request.downloadHandler.text);
        }

        public void Unload(string fileName)
        {
            if (File.Exists(_fileName))
                File.Delete(_fileName);

            Debug.Log($"File unloaded ({_fileName})");
        }
    }
}