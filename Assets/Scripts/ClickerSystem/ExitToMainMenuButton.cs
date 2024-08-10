using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ExitToMainMenuButton : MonoBehaviour
{
    [SerializeField] private Button _button;

    private void OnEnable() => _button.onClick.AddListener(OnExitButtonClicked);

    private void OnDisable() => _button.onClick.RemoveListener(OnExitButtonClicked);

    private void OnExitButtonClicked()
    {
        SceneManager.LoadScene(0);
    }
}