using System.Collections.Generic;
using ResourceLoaders;
using UnityEngine;
using UnityEngine.UI;

namespace ClickerSystem
{
    public class ClickerBootstrapper : MonoBehaviour
    {
        [SerializeField] private Canvas _gameplayCanvas;
        [SerializeField] private WalletView _walletView;

        public const string ClickerBundleName = "pizza";
        public const string PizzaObjectName = "Pizza";
        
        private readonly Clicker _clicker = new();
        private readonly Wallet _wallet = new();

        public void Init(GameStaticData staticData)
        {
            if (staticData.TryGetAssetBundle(ClickerBundleName, out var assetBundle) == false)
                throw new KeyNotFoundException(ClickerBundleName);
            
            var pizzaResource = assetBundle.LoadAsset<GameObject>(PizzaObjectName);
            var button = Instantiate(pizzaResource.GetComponent<Button>(), _gameplayCanvas.transform);
            
            _walletView.Init(_wallet);
            _clicker.Init(_wallet, button);
        }
    }
}