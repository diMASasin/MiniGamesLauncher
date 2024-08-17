using TMPro;
using UnityEngine;

namespace ClickerSystem.Wallet
{
    public class WalletView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;

        private Wallet _wallet;

        public void Init(Wallet wallet)
        {
            _wallet = wallet;

            OnPointsValueChanged(wallet.Points);
            
            _wallet.PointsValueChanged += OnPointsValueChanged;
        }

        private void OnDestroy()
        {
            if (_wallet != null) _wallet.PointsValueChanged -= OnPointsValueChanged;
        }

        private void OnPointsValueChanged(int points) => _text.text = points.ToString();
    }
}