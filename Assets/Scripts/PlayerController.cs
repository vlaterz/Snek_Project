using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public SnakeMovement SnakeController;
    private float _lastScreenXTapPosition;
    public float MovementSensetivityOffset = 0.1f;
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
