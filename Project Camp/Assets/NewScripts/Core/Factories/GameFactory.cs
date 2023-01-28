using BeaconProject.Core.AssetManagement;
using UnityEngine;

namespace BeaconProject.Core.Factories
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssets _assets;
        
        public GameFactory(IAssets assets)
        {
            _assets = assets;
        }

        public GameObject CreatePlayer(Vector3 position) =>
            _assets.Instantiate(AssetPath.PlayerPath, position);
    }
}