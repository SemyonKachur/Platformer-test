namespace View
{
    using DG.Tweening;
    using GameFlow;
    using UnityEngine;
    public sealed class EndGameAnimation : MonoBehaviour
    {
        [SerializeField]
        private AbstractCondition _endGameCondition = default;
        [SerializeField]
        private CanvasGroup _view = default;
        [SerializeField]
        private CanvasGroup _restart = default;

        [SerializeField, Range(0,5)]
        private float _duration = 2;

        private void OnEnable() => _endGameCondition.onValueChanged += UpdateView;

        private void UpdateView()
        {
            if (!_endGameCondition.IsComplete)
            {
                _view.gameObject.SetActive(true);
                Sequence sequence = DOTween.Sequence();
                
                sequence.Append(_view.DOFade(1, _duration).SetUpdate(true));
                sequence.Append(_restart.DOFade(1, _duration).SetUpdate(true));
                sequence.SetUpdate(true);
                sequence.Play();
            }
        }

        private void OnDisable() => _endGameCondition.onValueChanged -= UpdateView;
    }
}
