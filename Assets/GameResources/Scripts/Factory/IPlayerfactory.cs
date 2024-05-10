namespace Factory
{
    using Units.Player;
    using UnityEngine;

    public interface IPlayerfactory
    {
        public Player CreatePlayer(Vector3 position);
    }
}