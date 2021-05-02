using UnityEngine;

namespace Assets.Scripts.interfaces
{
    public interface ISnakeCollisionHandler
    {
        void HandleCollision(Collider col);
        void HandleFeverCollision(Collider col);
    }
}
