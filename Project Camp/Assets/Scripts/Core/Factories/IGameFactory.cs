using UnityEngine;

namespace Core.Factories
{
    public interface IGameFactory
    {
        Player CreatePlayer(Vector3 spawnPosition);
        void CreateCamera(Transform target);
    }
}