namespace Factory
{
    using Units.Player;
    using Unity.Mathematics;
    using UnityEngine;
    public class ResourcesPlayerSpawner : AbstractPlayerFactory
    {
        public Player Player => _gameplayPrefab;
        private Player _gameplayPrefab = default;
        
        [SerializeField]
        private Transform _parent = default;
        [SerializeField]
        protected string _path = "player";

        private Player _resourcesPrefab = default;
        
        public override Player CreatePlayer(Vector3 position)
        {
            _resourcesPrefab = Resources.Load<Player>(_path);
            if (_resourcesPrefab != null)
            {
                _gameplayPrefab = Instantiate(_resourcesPrefab, position, quaternion.identity, _parent);
                OnPlayerSpawnHandler();
                return _gameplayPrefab;
            }
            else
            {
                Debug.LogError("Не удалось загрузить префаб игрока по заданному пути");
                return null;
            }
        }
    }
}
