using UnityEngine;

namespace Core.Movements
{
    public interface IMoveable
    {
        public void Enable();

        public void Move(Vector3 cameraPosition, Vector3 input);

        public void Tick(float deltaTime);
        public void FixedTick(float deltaTime);
    }
}
