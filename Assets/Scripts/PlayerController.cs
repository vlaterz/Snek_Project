using System;
using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerController : MonoBehaviour
    {
        public static PlayerController GetInstance => _instance;
        private static PlayerController _instance;
        void Awake() => _instance = this;

        public SnakeController SnakeController;
        private float _lastScreenXTapPosition;
        public float MovementSensetivityOffset = 0.01f;
        void Start()
        {
            _lastScreenXTapPosition = (SnakeController.RoadLeftLimit + SnakeController.RoadRightLimit) / 2;
        }

        void Update()
        {
            if (!Input.GetMouseButton(0))
            {
                SnakeController.MovementDirection = SnakeDir.Center;
                return;
            }

            var screenPosNormalized = Input.mousePosition.x / Screen.width;
            _lastScreenXTapPosition = Mathf.Lerp(SnakeController.RoadLeftLimit,
                SnakeController.RoadRightLimit,
                screenPosNormalized);

            if (Math.Abs(_lastScreenXTapPosition - SnakeController.transform.position.x) >= MovementSensetivityOffset)
            {
                if(_lastScreenXTapPosition < SnakeController.transform.position.x)
                    SnakeController.MovementDirection = SnakeDir.Left;

                if(_lastScreenXTapPosition > SnakeController.transform.position.x)
                    SnakeController.MovementDirection = SnakeDir.Right;
            }
            else
                SnakeController.MovementDirection = SnakeDir.Center;
        }
    }
}
