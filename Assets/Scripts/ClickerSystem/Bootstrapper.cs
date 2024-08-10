using ResourceLoaders;
using UnityEngine;
using UnityEngine.UI;

namespace ClickerSystem
{
    public class Bootstrapper : MonoBehaviour
    {
        [SerializeField] private Image _image;
        
        private void Awake()
        {
            _image.sprite = ClickerResourceLoader.Sprite;
        }
    }
}