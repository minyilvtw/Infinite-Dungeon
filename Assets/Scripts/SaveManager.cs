using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;


public class SaveManager : MonoSingleton<SaveManager> {


    [Header("Level")]
    public int currentLevel;
    public int difficulty;
    public bool willAdvance;
    [Header("Player")]
    public int money;
    public Item[] inventoryItems = new Item[3];

    public float attackDamage;
    public float thirdAttackDamage;

    public float moveSpeed;
    public float jumpForce;

    public float health;
    public float maxHealth;
    public float defense;
    public float manaRegen;

    private int startLevel;
    private int startDifficulty;
    private bool startWillAdvance;

    private float startMoveSpeed;
    private float startJumpForce;
    private float startAttackDamage;
    private float startThirdAttackDamage;
    private float startMaxHealth;
    private float startDamageFactor;
    private float startManaRegen;

    void Awake() {

        DontDestroyOnLoad(this.gameObject);

        startLevel = currentLevel;
        startDifficulty = difficulty;
        startWillAdvance = willAdvance;
        startMoveSpeed = moveSpeed;
        startJumpForce = jumpForce;
        startMaxHealth = maxHealth;
        startDamageFactor = defense;
        startManaRegen = manaRegen;
    }

    public void ResetAllStatus() {
        currentLevel = startLevel;
        difficulty = startDifficulty;
        willAdvance = startWillAdvance;
    
    }

    public void LoadPlayerStatus()
    {
        
        PlayerStatus playerStatus = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>();

        playerStatus.money = money;

        playerStatus.moveSpeed = moveSpeed;
        playerStatus.jumpForce = jumpForce;

        playerStatus.attackDamage = attackDamage;

        playerStatus.maxHealth = maxHealth;
        playerStatus.defense = defense;
        playerStatus.manaRegen = manaRegen;


        InventoryController other = GameObject.FindGameObjectWithTag("Player").GetComponent<InventoryController>();
        for (int i = 0; i < inventoryItems.Length; i++)
        {
           other.inventoryItems[i] = new Item();

            if (inventoryItems[i] != null)
            {
                other.inventoryItems[i].itemEffect = inventoryItems[i].itemEffect;
                other.inventoryItems[i].artwork = inventoryItems[i].artwork;
                other.inventoryItems[i].name = inventoryItems[i].name;
            }
            else {
                other.inventoryItems[i] = null;
            }
           
        }


    }

    public void SavePlayerStatus()
    {
        PlayerStatus playerStatus = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>();
        moveSpeed = playerStatus.moveSpeed;
        jumpForce = playerStatus.jumpForce;

        money = playerStatus.money;

        attackDamage = playerStatus.attackDamage;

        maxHealth = playerStatus.maxHealth;
        defense = playerStatus.defense;
        manaRegen = playerStatus.manaRegen;

        InventoryController other = GameObject.FindGameObjectWithTag("Player").GetComponent<InventoryController>();
        for (int i = 0; i < inventoryItems.Length; i++)
        {
            inventoryItems[i] = new Item();
            if (other.inventoryItems[i] != null)
            {
                inventoryItems[i].itemEffect = other.inventoryItems[i].itemEffect;
                inventoryItems[i].artwork = other.inventoryItems[i].artwork;
                inventoryItems[i].name = other.inventoryItems[i].name;
            }
            else
            {
                inventoryItems[i] = null;
            }
        }
    }




}
