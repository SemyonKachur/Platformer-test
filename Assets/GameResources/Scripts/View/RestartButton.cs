namespace View
{
    using UnityEngine;
    using UnityEngine.SceneManagement;
    using UnityEngine.UI;

    [RequireComponent(typeof(Button))]
    public class RestartButton : MonoBehaviour
    {
        private Button _button = default;

        private void Awake() => _button = GetComponent<Button>();

        private void OnEnable() => _button.onClick.AddListener(Restart);

        private void Restart() => SceneManager.LoadScene("Game");

        private void OnDisable() => _button.onClick.RemoveListener(Restart);
    }
}
