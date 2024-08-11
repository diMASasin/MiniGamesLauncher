using TMPro;
using UnityEngine;

namespace ClickerSystem
{
    public class WalletView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
    
        private Wallet _wallet;

        public void Init(Wallet wallet)
        {
            _wallet = wallet;
        }
    
        private void OnEnable() => _wallet.PointsValueChanged += OnPointsValueChanged;

        private void OnDisable() => _wallet.PointsValueChanged -= OnPointsValueChanged;

        private void OnPointsValueChanged(int points)
        {
            _text.text = points.ToString();
        }
    }
}