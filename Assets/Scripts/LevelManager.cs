using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    public bool isAlive;
    public bool willAdvance;

    public int level;

    public int difficulty;

    [Header("Amount * Level")]
    public int spawnCount;

    [Space]
    public GameObject scoreCanvas;
    public Text levelText;
    public Text highscoreText;

   public GameObject statusCanvas;

    public GameObject victoryText;
    public GameObject defeatedText;

    private SaveManager saveManager;

    public delegate void InitializeLevel();
    public InitializeLevel InitLevelData;

    public bool bossDefeated = false;

    void Awake() {
        InitializeGame();
    }

    void InitializeGame()
    {

        saveManager = FindObjectOfType<SaveManager>();
        LoadLevelStatus();

        if (InitLevelData != null) {
            Debug.Log("Level Initialize");
            InitLevelData();
        }

        isAlive = true;
        Time.timeScale = 1f;
        
        levelText.text = "Level: " + level;
        if (level == PlayerPrefs.GetInt("LastLevel")) {
            Debug.Log("Last death");
        }

        if (willAdvance)
        {
            this.GetComponent<UpgradeManager>().OpenUpgradeCanvas();
            Time.timeScale = 0f;
            willAdvance = false;
        }
    }

    void Update()
    {


        if (Input.GetKey(KeyCode.Q))
        {
           
            statusCanvas.SetActive(true);
        }
        else {
            statusCanvas.SetActive(false);
        }

        RefreshUI();
        /*
        GameObject[] remainingEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (remainingEnemies.Length == 0 && isAlive)
        {
            //Victory();
        }

        if (Input.GetKeyDown(KeyCode.Alpha9)) {
           // Victory();
        }
       */
        if (bossDefeated)
        {
            bossDefeated = false;
            Victory();
        }
    }

    void RefreshUI() {

        highscoreText.text = "Highscore: " + PlayerPrefs.GetInt("Highscore").ToString();
        
        PlayerStatus playerStatus = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>();
        statusCanvas.GetComponentInChildren<Text>().text = "Speed: " + playerStatus.currentMoveSpeed
                                                         + "\nJump: " + playerStatus.currentJumpForce
                                                         + "\nAttack: " + (playerStatus.currentAttackDamage)
                                                         + "\nDefense: " + (1f + playerStatus.currentDefense)
                                                         + "\nMana Regen: " + playerStatus.manaRegen;


    }
    
    public void ResetLevel()
    {
        saveManager.ResetAllStatus();
        SceneManager.LoadScene("RandomMap");
    }

    public void AdvanceLevel()
    {
        SaveLevelStatus();
        SceneManager.LoadScene("RandomMap");
    }

    public void Victory()
    {
        victoryText.SetActive(true);
        isAlive = false;
        willAdvance = true;

        level++;
    }

    public void Defeat() {
        defeatedText.SetActive(true);
        victoryText.SetActive(false);
        willAdvance = false;
        isAlive = false;
        Time.timeScale = 0f;

        PlayerPrefs.SetInt("LastLevel", level);
        if(level > PlayerPrefs.GetInt("Highscore")){
                PlayerPrefs.SetInt("Highscore", level);

        }
    }

    public void LoadLevelStatus() {
        level = saveManager.currentLevel;
        difficulty = saveManager.difficulty;
        willAdvance = saveManager.willAdvance;
        // Player Awake loads its own status, to maintain Awake order, and to keep load -> instantiate order
    }

    public void SaveLevelStatus() {
        saveManager = FindObjectOfType<SaveManager>();

        saveManager.currentLevel = level;
        saveManager.difficulty = difficulty;
        saveManager.willAdvance = willAdvance;

        saveManager.SavePlayerStatus();      
    }

  
    [ContextMenu("Reset Highscore")]
    void ResetHighscore() {
        PlayerPrefs.SetInt("Highscore", 0);
    }
}
