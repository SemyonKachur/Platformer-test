namespace View
{
    using GameFlow;
    using UnityEngine;
    using UnityEngine.UI;

    [RequireComponent(typeof(Text))]
    public sealed class ResultTextView : MonoBehaviour
    {
        [SerializeField]
        private AbstractCondition _winCondition = default;

        [SerializeField]
        private string _winText = "You win!";
        [SerializeField]
        private string _looseText = "You loose!";
        
        private Text _text = default;

        private void Awake() => _text = GetComponent<Text>();

        private void OnEnable() => _text.text = _winCondition.IsComplete ? _looseText : _winText;
    }
}
