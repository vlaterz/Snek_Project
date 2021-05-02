using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using Assets.Scripts.Data;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController GetInstance => _instance;
    private static GameController _instance;
    
    public PlayerLevelData SavedLevelData;
    public PlayerLevelData CurrentLevelData;
    public Color[] GameColors = new[]
    {
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

    public void OnDiamondPickedUp()
    {
        CurrentLevelData.CollectedDiamonds += 1;
        if(CurrentLevelData.CollectedDiamonds % 3 == 0)
            SnakeController.GetInstance.StartFever();
    }

    public void SavePlayerLevelData() => SavedLevelData = CurrentLevelData;
    public void LoadPlayerGame()
    {
        CurrentLevelData = SavedLevelData;
        SceneManager.LoadScene("GameScene");
    }
    

}
