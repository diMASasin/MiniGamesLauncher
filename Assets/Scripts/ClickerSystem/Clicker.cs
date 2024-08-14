using UnityEngine.UI;

namespace ClickerSystem
{
    public class Clicker
    {
        private Wallet _wallet;
        private Button _button;
        private readonly int _addingValue = 1;

        public void Init(Wallet wallet, Button button)
        {
            _wallet = wallet;
            _button = button;
            
            _button.onClick.AddListener(OnButtonClicked);
        }

        private void OnDestroy() => _button.onClick.RemoveListener(OnButtonClicked);

        private void OnButtonClicked()
        {
            _wallet.Add(_addingValue);
        }
    }
}
