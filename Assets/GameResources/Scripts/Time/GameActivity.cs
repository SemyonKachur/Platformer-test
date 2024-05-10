namespace Time
{
    using GameFlow;
    using UnityEngine;
    using Time = UnityEngine.Time;

    public sealed class GameActivity : MonoBehaviour
    {
        private const float STOP_GAME = 0;
        private const float ACTIVE_GAME = 1;
    
        [SerializeField]
        private AbstractGameState startGame = default;
    
        private void Awake() => Time.timeScale = STOP_GAME;

        private void OnEnable() => startGame.onStateValueChanged += CheckGameMode;

        private void CheckGameMode() => Time.timeScale = startGame.IsActive ? ACTIVE_GAME : STOP_GAME;

        private void OnDisable() => startGame.onStateValueChanged -= CheckGameMode;
    }
}
