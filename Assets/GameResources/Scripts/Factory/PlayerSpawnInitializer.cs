namespace Factory
{
    using Units.Player;
    using UnityEngine;

    public class PlayerSpawnInitializer : MonoBehaviour
    {
        [SerializeField]
        private AbstractPlayerFactory _factory = default;
        [SerializeField]
        private Transform _spawnPosition = default;
        [SerializeField]
        private PlayerContainerInstance _playerContainer = default;
        
        private Player _player = default;

        private void Awake()
        {
            _player = _factory.CreatePlayer(_spawnPosition.position);
            if (_player != null)
            {
                _playerContainer.Player = _player;
            }    
        }
    }
}
