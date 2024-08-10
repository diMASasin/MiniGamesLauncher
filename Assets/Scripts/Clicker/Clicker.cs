using UnityEngine;
using UnityEngine.UI;

public class Clicker : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private Wallet _wallet;

    private readonly int _addingValue = 1;

    private void OnEnable() => _button.onClick.AddListener(OnButtonClicked);

    private void OnDisable() => _button.onClick.RemoveListener(OnButtonClicked);

    private void OnButtonClicked()
    {
        _wallet.Add(_addingValue);
    }
}
