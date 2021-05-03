using Assets.Scripts.GOScripts;
using Assets.Scripts.interfaces;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    /// <summary>
    /// Обработчик столкновений змеи
    /// </summary>
    public class SnakeCollisionHandler : ISnakeCollisionHandler
    {
        private SnakeController _snakeController;

        public SnakeCollisionHandler(SnakeController snakeController)
        {
            _snakeController = snakeController;
        }

        /// <summary>
        /// Обрабатываем столкновения в обычном режиме
        /// </summary>
        /// <param name="col"></param>
        public void HandleCollision(Collider col)
        {
            switch (col.tag)
            {
                case "CheckPoint":
                    HandleCheckPoint(col);
                    break;
                case "Obstacle":
                    HandleObstacle(col);
                    break;
                case "PickUp":
                    HandlePickUp(col);
                    break;
                case "Human":
                    HandleHuman(col);
                    break;
            }
        }

        /// <summary>
        /// Обрабаотываем столкновения в режиме Fever
        /// </summary>
        /// <param name="col"></param>
        public void HandleFeverCollision(Collider col)
        {
            switch (col.tag)
            {
                case "CheckPoint":
                    HandleCheckPoint(col);
                    break;
                case "Obstacle":
                    col.gameObject.SetActive(false);
                    break;
                case "PickUp":
                    HandlePickUp(col);
                    break;
                case "Human":
                    HandleHuman(col);
                    break;
            }
        }

        private void HandleCheckPoint(Collider col)
        {
            var checkPoint = col.GetComponent<CheckPoint>();
            _snakeController.ChangeSnakeColor(checkPoint.Color);
            //GameController.GetInstance.CurrentLevelData.LastCheckPointPosition = col.transform.position;
            //GameController.GetInstance.SavePlayerLevelData();
        }

        private void HandleObstacle(Collider col)
        {
            GameController.GetInstance.LoadPlayerGame();
        }

        private void HandlePickUp(Collider col)
        {
            col.gameObject.SetActive(false);
            GameController.GetInstance.OnDiamondPickedUp();
        }

        private void HandleHuman(Collider col)
        {
            var human = col.GetComponent<Human>();
            if (human.HumanColor != SnakeController.GetInstance.SnakeColor)
            {
                col.gameObject.SetActive(false);
                GameController.GetInstance.LoadPlayerGame();
            }
            
            col.gameObject.SetActive(false);
            GameController.GetInstance.OnHumanEat();
        }
    }
}
