using Assets.Scripts.Data;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class GameController : MonoBehaviour
    {
        public static GameController GetInstance => _instance;
        private static GameController _instance;

        public TextMeshProUGUI ScoreTextRef;
        public PlayerLevelData SavedLevelData;
        public PlayerLevelData CurrentLevelData;
        public Color[] GameColors = {
            Color.blue,
            Color.cyan,
            Color.magenta,
            Color.red,
            Color.green,
            Color.yellow
        };


        void Start()
        {
            SavedLevelData.LastCheckPointPosition = transform.position;
            if (_instance == null)
                _instance = this;
            else 
                Destroy(gameObject);
            DontDestroyOnLoad(gameObject);
            SnakeController.GetInstance.transform.position = SavedLevelData.LastCheckPointPosition;
        }

        public void OnDiamondPickedUp()
        {
            
            if (SnakeController.GetInstance.HasFever) return;
            CurrentLevelData.CollectedDiamonds += 1;
            if (CurrentLevelData.CollectedDiamonds % 3 == 0)
            {
                SnakeController.GetInstance.StartFever();
                CurrentLevelData.CollectedDiamonds = 0;
            }
            UpdateUI();
            Debug.Log($"DiamondPICKUP: {CurrentLevelData.CollectedDiamonds}");
        }

        public void OnHumanEat()
        {
            CurrentLevelData.HumansEaten += 1;
            if(CurrentLevelData.HumansEaten % 6 == 0)
                SnakeController.GetInstance.ComponentSnakeTail.AddTailPart();
        }

        public void UpdateUI()
        {
            if (ScoreTextRef == null)
                ScoreTextRef = GameObject.FindGameObjectWithTag("UI_DiamondsCounter").GetComponent<TextMeshProUGUI>();
            ScoreTextRef.SetText(CurrentLevelData.CollectedDiamonds.ToString());
        } 
        public void SavePlayerLevelData() => SavedLevelData = CurrentLevelData;
        public void LoadPlayerGame()
        {
            //CurrentLevelData = SavedLevelData;
            CurrentLevelData.ResetParamsToZero();
            UpdateUI();
            SceneManager.LoadScene("GameScene");
        }
    }
}
