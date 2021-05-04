using System.Collections;
using Assets.Scripts.interfaces;
using UnityEngine;

namespace Assets.Scripts
{
    public enum SnakeDir
    {
        Left,
        Center,
        Right
    }

    public class SnakeController : MonoBehaviour
    {
        public static SnakeController GetInstance => _instance;
        private static SnakeController _instance;

        public Color SnakeColor = Color.white;
        public bool HasFever;
        public float Speed = 2f;
        public float TurnSpeed = 5f;
        public float RoadLeftLimit = -5f;
        public float RoadRightLimit = 5f;
        public float PickupCollectRange = 2.5f;
        public float CollectAngle = 45f;
        public float FeverSpdModifier = 3f;
        public float FeverTimer = 5f;
        public SnakeDir MovementDirection = SnakeDir.Center;
        public SnakeTail ComponentSnakeTail;
        private ISnakeCollisionHandler _collisionHandler; //Обработчик столкновений змеи с объектами

        private float RoadCenter => (RoadLeftLimit + RoadRightLimit) / 2;

        private void OnTriggerEnter(Collider col)
        {
            if (HasFever)
                _collisionHandler.HandleFeverCollision(col);
            else
                _collisionHandler.HandleCollision(col);
        }
        
        void Awake()
        {
            _instance = this;
            _collisionHandler = new SnakeCollisionHandler(this);
        }

        private void Start()
        {
            ComponentSnakeTail = GetComponent<SnakeTail>();
            ComponentSnakeTail.AddTailPart();
            StartCoroutine(VacuumCollectablesChecker());
        }

        private void FixedUpdate()
        {
            if (HasFever)
                FeverMovementHandler();
            else
                ControlledMovementHandler();
        }

        public void FeverMovementHandler()
        {
            if (Mathf.Abs(transform.position.x - RoadCenter) < 0.01f) // Ползем по центру
                transform.Translate(Vector3.forward * Speed * FeverSpdModifier * Time.deltaTime);
            else
            {
                if (transform.position.x < RoadCenter) //Ползем вправо
                    transform.Translate(Vector3.forward * Speed * Time.deltaTime +
                                        Vector3.right * TurnSpeed * Time.deltaTime);

                if (transform.position.x > RoadCenter) //Ползем влево 
                    transform.Translate(Vector3.forward * Speed * Time.deltaTime +
                                        Vector3.left * TurnSpeed * Time.deltaTime);
            }
        }

        public void ControlledMovementHandler()
        {
            switch (MovementDirection)
            {
                case SnakeDir.Left when transform.position.x >= RoadLeftLimit:
                    transform.Translate(Vector3.forward * Speed * Time.deltaTime +
                                        Vector3.left * TurnSpeed * Time.deltaTime);
                    return;
                case SnakeDir.Right when transform.position.x <= RoadRightLimit:
                    transform.Translate(Vector3.forward * Speed * Time.deltaTime +
                                        Vector3.right * TurnSpeed * Time.deltaTime);
                    return;
                default:
                    transform.Translate(Vector3.forward * Speed * Time.deltaTime);
                    break;
            }
        }
        public void ChangeSnakeColor(Color color)
        {
            SnakeColor = color;
            foreach (var renderer in GetComponentsInChildren<Renderer>())
                renderer.material.SetColor("_BaseColor", color);
        }
        public void StartFever() => StartCoroutine(FeverTime());
        private IEnumerator FeverTime()
        {
            HasFever = true;
            PlayerController.GetInstance.gameObject.SetActive(false);
            yield return new WaitForSeconds(FeverTimer);
            PlayerController.GetInstance.gameObject.SetActive(true);
            HasFever = false;
        }

        /// <summary>
        /// Корутина проверяет наличие коллайдеров в конусе перед головой
        /// </summary>
        /// <returns></returns>
        private IEnumerator VacuumCollectablesChecker()
        {
            while (true)
            {
                var colliders = Physics.OverlapSphere(transform.position, PickupCollectRange);
                foreach (var collectable in colliders)
                {
                    var collectableDirection = (collectable.transform.position - transform.position).normalized;
                    var angle = Vector3.Angle(Vector3.forward, collectableDirection);
                    if(angle <= CollectAngle)
                        collectable.GetComponent<ICollectable>()?.Collect();
                }
                yield return null;
            }
        }
    }
}