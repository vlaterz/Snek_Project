using Assets.Scripts.interfaces;
using UnityEngine;

namespace Assets.Scripts.GOScripts
{
    public class Collectable : MonoBehaviour, ICollectable
    {
        public float FlySpeed = 2f;
        public float FlyAcceleration = 5f;
        private bool _isCollected;
    
        void Update()
        {
            if (!_isCollected) return;
            var direction = (SnakeController.GetInstance.transform.position - transform.position).normalized;
            transform.Translate(direction * FlySpeed * Time.deltaTime);
            FlySpeed += FlyAcceleration * Time.deltaTime;
        }

        public void Collect()
        {
            _isCollected = true;
        }
    }
}
