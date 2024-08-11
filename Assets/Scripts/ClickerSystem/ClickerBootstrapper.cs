using ResourceLoaders;
using UnityEngine;
using UnityEngine.UI;

namespace ClickerSystem
{
    public class ClickerBootstrapper : MonoBehaviour
    {
        [SerializeField] private Canvas _gameplayCanvas;
        [SerializeField] private WalletView _walletView;
        
        private readonly Clicker _clicker = new();
        private readonly GameStaticData _staticData = new();
        private readonly Wallet _wallet = new();
        
        public void Init(GameStaticData staticData)
        {
            var pizzaResource = staticData.AssetBundle.LoadAsset<GameObject>("Pizza");
            var button = Instantiate(pizzaResource.GetComponent<Button>(), _gameplayCanvas.transform);
            
            _walletView.Init(_wallet);
            _clicker.Init(_wallet, button);
        }
    }
}