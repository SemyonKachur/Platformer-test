namespace Factory
{
    using System;
    using Units.Player;
    using UnityEngine;

    [CreateAssetMenu(fileName = "PlayerContainerInstance", menuName = "Scriptable Objects/PlayerContainerInstance")]
    public class PlayerContainerInstance : ScriptableObject
    {
        public event Action onPlayerInit = delegate { };

        public Player Player
        {
            get => _player;
            set
            {
                _player = value;
                onPlayerInit();
            }
        }
        [SerializeField, Header("Для дебага")]
        private Player _player = default;
    }
}
