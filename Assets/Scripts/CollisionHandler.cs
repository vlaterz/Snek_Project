using Assets.Scripts.interfaces;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class SnakeCollisionHandler : ISnakeCollisionHandler
    {
        private SnakeController _snakeController;

        public SnakeCollisionHandler(SnakeController snakeController)
        {
            _snakeController = snakeController;
        }

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
                   // HandleHuman(col);
                    break;
            }
        }

        private void HandleCheckPoint(Collider col)
        {
            var checkPoint = col.GetComponent<CheckPoint>();
            GameController.GetInstance.CurrentLevelData.LastCheckPointPosition = col.transform.position;
            GameController.GetInstance.SavePlayerLevelData();
            _snakeController.ChangeSnakeColor(checkPoint.Color);
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
            Debug.Log("Human col!");
        }
    }
}
