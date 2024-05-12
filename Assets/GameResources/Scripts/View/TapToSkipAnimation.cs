namespace View
{
    using DG.Tweening;
    using UnityEngine;
    using UnityEngine.EventSystems;

    [RequireComponent(typeof(CanvasGroup))]
    public class TapToSkipAnimation : MonoBehaviour, IPointerClickHandler
    {
        private CanvasGroup _canvas = default;

        [SerializeField]
        private float _targetAlpha = 0;
        [SerializeField, Range(0,5)]
        private float _duration = 1;

        private void Awake() => _canvas = GetComponent<CanvasGroup>();

        public void OnPointerClick(PointerEventData eventData) => _canvas.DOFade(_targetAlpha, _duration).OnComplete(() => gameObject.SetActive(false));
    }
}
