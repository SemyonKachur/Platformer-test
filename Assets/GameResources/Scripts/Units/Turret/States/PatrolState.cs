namespace Units.Turret
{
    using DG.Tweening;
    using DG.Tweening.Core;
    using DG.Tweening.Plugins.Options;
    using UnityEngine;

    public sealed class PatrolState : AbstractAiState
    {
        [SerializeField, Range(0, 10)]
        private float _speed = default;
        [SerializeField]
        private Ease _easeMode = Ease.Linear;
        [SerializeField]
        private Vector3 _rotation = new Vector3(360, 0, 0);
       
        private TweenerCore<Quaternion, Vector3, QuaternionOptions> _tween = default;

        public override void Enter()
        {
            _tween = turret.transform
                .DORotate(_rotation, _speed, RotateMode.LocalAxisAdd)
                .SetLoops(-1, LoopType.Restart)
                .SetEase(_easeMode);
        }

        public override void Exit()
        {
            base.Exit();
            _tween.Kill();
        } 
    }
}