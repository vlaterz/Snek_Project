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

        public float Speed = 2f;
        public float TurnSpeed = 5f;
        public float RoadLeftLimit = -5f;
        public float RoadRightLimit = 5f;
        public float PickupCollectRange = 2.5f;
        private float RoadCenter => (RoadLeftLimit + RoadRightLimit) / 2;
        public float FeverSpdModifier = 3f;
        public Color SnakeColor = Color.white;
        public SnakeDir MovementDirection = SnakeDir.Center;

        private ISnakeCollisionHandler _collisionHandler; //Обработчик столкновений змеи с объектами
        private void OnTriggerEnter(Collider col)
        {
            if (_hasFever)
                _collisionHandler.HandleFeverCollision(col);
            else
                _collisionHandler.HandleCollision(col);
        }
        private SnakeTail _componentSnakeTail;
        private bool _hasFever;

        void Awake()
        {
            _instance = this;
            _collisionHandler = new SnakeCollisionHandler(this);
        }

        private void Start()
        {
            transform.position = GameController.GetInstance.SavedLevelData.LastCheckPointPosition;
            _componentSnakeTail = GetComponent<SnakeTail>();
            _componentSnakeTail.AddTailPart();
            _componentSnakeTail.AddTailPart();
            _componentSnakeTail.AddTailPart();
            _componentSnakeTail.AddTailPart();
            StartCoroutine(VacuumCollectablesChecker());
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
                _componentSnakeTail.AddTailPart();
            if (Input.GetKeyDown(KeyCode.D))
                _componentSnakeTail.RemoveTailPart();
        }

        private void FixedUpdate()
        {
            if (_hasFever)
                FeverMovementHandler();
            else
                ControlledMovementHandler();
        }

        public void FeverMovementHandler()
        {
            if (Mathf.Abs(transform.position.x - RoadCenter) < 0.1f) // Ползем по центру
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
            var renderers = GetComponentsInChildren<Renderer>();
            foreach (var renderer in renderers)
            {
                renderer.material.EnableKeyword("_EMISSION");
                renderer.material.SetColor("_EmissionColor", color);
            }
        }
        public void StartFever() => StartCoroutine(FeverTime());
        private IEnumerator FeverTime()
        {
            _hasFever = true;
            PlayerController.GetInstance.gameObject.SetActive(false);
            yield return new WaitForSeconds(3);
            PlayerController.GetInstance.gameObject.SetActive(true);
            _hasFever = false;
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
                    if(angle <= 45)
                        collectable.GetComponent<ICollectable>()?.Collect();
                }
                yield return null;
            }
        }
    }
}