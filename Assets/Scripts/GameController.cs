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

        void Awake()
        {
            SavedLevelData.LastCheckPointPosition = transform.position;
            if (_instance == null)
                _instance = this;
            else if(_instance == this)
                Destroy(gameObject);
            DontDestroyOnLoad(gameObject);
        }

        void Start()
        {
            SnakeController.GetInstance.transform.position = SavedLevelData.LastCheckPointPosition;
        }

        public void OnDiamondPickedUp()
        {
            if (SnakeController.GetInstance.HasFever) return;
            CurrentLevelData.CollectedDiamonds += 1;
            if(CurrentLevelData.CollectedDiamonds % 3 == 0)
                SnakeController.GetInstance.StartFever();
            UpdateUI();
        }

        public void OnHumanEat()
        {
            //Debug.Log("Human was eaten");
        }

        public void UpdateUI()=> ScoreTextRef.SetText(CurrentLevelData.CollectedDiamonds.ToString());
        public void SavePlayerLevelData() => SavedLevelData = CurrentLevelData;
        public void LoadPlayerGame()
        {
            //CurrentLevelData = SavedLevelData;
            SceneManager.LoadScene("GameScene");
        }
    }
}
