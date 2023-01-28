using BeaconProject.Core.Services;
using UnityEngine;

namespace BeaconProject.Core.Factories
{
    public interface IGameFactory : IService
    {
        GameObject CreatePlayer(Vector3 position);
    }
}