using Core.Factories;
using UnityEngine;
using Environment;
using GUI;

namespace Core
{
    public class Game 
    {
        public Player Player { get; private set; }

        private readonly IGameFactory _factory;
        
        public Game(IGameFactory factory, LevelEnvironment environment, GUIController gui, Vector3 spawnPosition)
        {
            _factory = factory;

            Player = _factory.CreatePlayer(spawnPosition);
            _factory.CreateCamera(Player.transform);
            
            environment.Init(Player);
            gui.Init(Camera.main, Player, environment);
        }
    }
}
