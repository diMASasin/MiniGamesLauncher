using System;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace ResourceLoaders
{
    public class ResourceLoadProgressView : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private TMP_Text _statusText;
        
        private IResourceLoader _resourceLoader;
        private float _maxProgressValue;

        public void Init(IResourceLoader resourceLoader)
        {
            _resourceLoader = resourceLoader;
            _maxProgressValue = _slider.maxValue;
            
            _resourceLoader.ProgressChanged += OnProgressChanged;
            _resourceLoader.StatusChanged += OnStatusChanged;
            _resourceLoader.Unloaded += OnUnloaded;
        }

        private void OnDestroy()
        {
            _resourceLoader.ProgressChanged -= OnProgressChanged;
            _resourceLoader.StatusChanged -= OnStatusChanged;
            _resourceLoader.Unloaded -= OnUnloaded;
        }

        private void OnProgressChanged(float progress)
        {
            _slider.value = progress;
        }

        private void OnStatusChanged(UnityWebRequest.Result result)
        {
            _statusText.text = result.ToString();
        }

        public void OnLoaded()
        {
            OnProgressChanged(_maxProgressValue);
            OnStatusChanged(UnityWebRequest.Result.Success);
        }

        private void OnUnloaded()
        {
            OnProgressChanged(_slider.minValue);
            _statusText.text = string.Empty;
        }
    }
}