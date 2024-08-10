using TMPro;
using UnityEngine;

public class WalletView : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Wallet _wallet;

    private void OnEnable() => _wallet.PointsValueChanged += OnPointsValueChanged;

    private void OnDisable() => _wallet.PointsValueChanged -= OnPointsValueChanged;

    private void OnPointsValueChanged(int points)
    {
        _text.text = points.ToString();
    }
}