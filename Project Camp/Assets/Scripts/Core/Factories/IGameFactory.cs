using Core.Cameras;
using UnityEngine;

namespace Core.Factories
{
    public interface IGameFactory
    {
        Player CreatePlayer(Vector3 spawnPosition);
        CameraFollower CreateCamera(Transform target);
    }
}