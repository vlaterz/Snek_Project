using Assets.Scripts;
using UnityEngine;

public enum SnakeDir
{
    Left,
    Center,
    Right
}

public class SnakeMovement : MonoBehaviour
{
   

    public float Speed = 2f;
    public float TurnSpeed = 5f;

    public float RoadLeftLimit = -5f;
    public float RoadRightLimit = 5f;

    public SnakeDir MovementDirection = SnakeDir.Center;

    private SnakeTail _componentSnakeTail;

    private void Start()
    {
        _componentSnakeTail = GetComponent<SnakeTail>();
        _componentSnakeTail.AddCircle();
        _componentSnakeTail.AddCircle();
        _componentSnakeTail.AddCircle();
        _componentSnakeTail.AddCircle();
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector3.forward * Speed * Time.deltaTime);

        if (MovementDirection == SnakeDir.Left)
        {
            if (transform.position.x >= RoadLeftLimit)
            {
                transform.Translate(Vector3.forward * Speed * Time.deltaTime +
                                    Vector3.left * TurnSpeed * Time.deltaTime);
                return;
            }
        }

        if (MovementDirection == SnakeDir.Right)
        {
            if (transform.position.x <= RoadRightLimit)
            {
                transform.Translate(Vector3.forward * Speed * Time.deltaTime +
                                    Vector3.right * TurnSpeed * Time.deltaTime);
                return;
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            _componentSnakeTail.AddCircle();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            _componentSnakeTail.RemoveCircle();
        }
    }
}
