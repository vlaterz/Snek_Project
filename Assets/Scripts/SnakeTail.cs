using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class SnakeTail : MonoBehaviour
    {
        public Transform SnakeHead;
        public float CircleDiameter;

        private List<Transform> _snakeCircles = new List<Transform>();
        private List<Vector3> _positions = new List<Vector3>();

        private void Awake()
        {
            _positions.Add(SnakeHead.position);
        }

        private void Update()
        {
            var distance = (SnakeHead.position - _positions[0]).magnitude;

            if (distance > CircleDiameter)
            {
                // Направление от старого положения головы, к новому
                var direction = (SnakeHead.position - _positions[0]).normalized;

                _positions.Insert(0, _positions[0] + direction * CircleDiameter);
                _positions.RemoveAt(_positions.Count - 1);

                distance -= CircleDiameter;
            }

            for (var i = 0; i < _snakeCircles.Count; i++)
            {
                _snakeCircles[i].position = Vector3.Lerp(_positions[i + 1], _positions[i], distance / CircleDiameter);
            }
        }

        public void AddCircle()
        {
            var circle = Instantiate(SnakeHead, _positions[_positions.Count - 1], Quaternion.identity, transform);
            _snakeCircles.Add(circle);
            _positions.Add(circle.position);
        }

        public void RemoveCircle()
        {
            Destroy(_snakeCircles[0].gameObject);
            _snakeCircles.RemoveAt(0);
            _positions.RemoveAt(1);
        }
    }
}