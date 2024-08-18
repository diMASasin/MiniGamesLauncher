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
        
        private ILoadMethodActions[] _loadMethods;
        private float _maxProgressValue;
        private int _loadedResources;

        public void Init(params ILoadMethodActions[] resourceLoader)
        {
            _loadMethods = resourceLoader;
            _maxProgressValue = _slider.maxValue;

            foreach (var loader in _loadMethods)
            {
                loader.ProgressChanged += OnProgressChangedMax;
                loader.StatusChanged += OnStatusChanged;
                loader.Unloaded += OnUnloaded;
            }
        }

        private void OnDestroy()
        {
            foreach (var loader in _loadMethods)
            {
                loader.ProgressChanged -= OnProgressChangedMax;
                loader.StatusChanged -= OnStatusChanged;
                loader.Unloaded -= OnUnloaded;
            }
        }

        public void OnLoaded()
        {
            OnProgressChanged(_maxProgressValue);
            ShowStatus(UnityWebRequest.Result.Success);
        }

        private void OnProgressChanged(float progress) => 
            _slider.value = progress;
        
        private void OnProgressChangedMax(float progress) => 
            OnProgressChanged(Mathf.Max(progress, _slider.value));

        private void OnStatusChanged(UnityWebRequest.Result result)
        {
            if (result == UnityWebRequest.Result.Success)
            {
                _loadedResources++;

                if (_loadedResources < _loadMethods.Length)
                    return;
            }

            ShowStatus(result);
        }

        private void ShowStatus(UnityWebRequest.Result result) => _statusText.text = result.ToString();

        private void OnUnloaded()
        {
            OnProgressChanged(_slider.minValue);
            _statusText.text = string.Empty;
        }
    }
}