namespace Factory
{
    using System;
    using Units.Player;
    using UnityEngine;
    
    public abstract class AbstractPlayerFactory : MonoBehaviour, IPlayerfactory
    {
        public event Action onPlayerSpawned = delegate { }; 
        
        public abstract Player CreatePlayer(Vector3 position);

        protected virtual void OnPlayerSpawnHandler() => onPlayerSpawned();
    }
}
