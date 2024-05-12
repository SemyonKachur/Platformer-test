namespace Factory
{
    using Unity.Cinemachine;
    using UnityEngine;

    public class CameraFollow : PlayerContainerProvider
    {
        [SerializeField]
        private CinemachineVirtualCamera _virtualCamera = default;

        protected override void DoAction()
        {
            player = _playerContainer.Player;
            _virtualCamera.Follow = player.transform;
        } 
    }
}
