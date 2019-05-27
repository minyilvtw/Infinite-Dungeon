using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum buffType { MoveSpeed, JumpForce, Defense, AttackDamage, ManaRegen}
public enum buffAttribute { Stacks, Duration }

public class PlayerStatus : MonoBehaviour {

    public int money;

    [Header("Stats")]
    public float attackDamage = 1f;

    public float moveSpeed;
    public float jumpForce;
    public float health;
    public float maxHealth;

    public float mana;
    public float maxMana;
    public float manaRegen;

    public float defense;

    public bool isInvincible;


    [Header("Item Stack Effect")]
    public float perStackSpeed;
    public float perStackJumpForce;
    public float perStackManaRegen;
    public float perStackDefense;
    public float perStackAttackDamage;

    [HideInInspector]
    public float currentMoveSpeed;
    [HideInInspector]
    public float currentJumpForce;
    [HideInInspector]
    public float currentManaRegen;
    [HideInInspector]
    public float currentDefense;
    [HideInInspector]
    public float currentAttackDamage;


    public Text moneyText;
    public Image healthBar;
    public Text healthText;
    public Image manaBar;
    public Text manaText;

    public Text buffText;
    public GameObject impactPrefab;

    private PlayerBase pb;
    public int[,] buff = new int[5, 2];
    // [STACKS, DURATION], STACKS - 0 Speed, ,1 JumpForce, 2 Defense, 3 AttackDamage, 4 Bleed

    void Start() {
        pb = GetComponent<PlayerBase>();

        if (FindObjectOfType<LevelManager>().level > 1)
        {
           FindObjectOfType<SaveManager>().LoadPlayerStatus();
        }
        health = maxHealth;
    }


    private float lastUpdate;

    private void Update()
    {

        health = Mathf.Clamp(health, 0f, maxHealth);
        mana = Mathf.Clamp(mana, 0f, maxMana);

        if (health <= 0f)
        {
            pb.PlayerAnimation.playerAnimator.SetBool("Die", true);
            pb.LevelManager.Defeat();
        }

        

        currentMoveSpeed = moveSpeed + buff[(int)buffType.MoveSpeed, (int)buffAttribute.Stacks] * perStackSpeed;
        currentJumpForce = jumpForce + buff[(int)buffType.JumpForce, (int)buffAttribute.Stacks] * perStackJumpForce;
        currentManaRegen = manaRegen - buff[(int)buffType.ManaRegen, (int)buffAttribute.Stacks] * perStackManaRegen;
        currentDefense = defense + buff[(int)buffType.Defense, (int)buffAttribute.Stacks] * perStackDefense;
        currentAttackDamage = attackDamage + buff[(int)buffType.AttackDamage, (int)buffAttribute.Stacks] * perStackAttackDamage;


        currentManaRegen = Mathf.Clamp(currentManaRegen, 0f, 100f);
        currentDefense = Mathf.Clamp(currentDefense, -0.5f, 0.5f);

        moneyText.text = "X " + money;
        healthBar.fillAmount = health / maxHealth;
        healthText.text = (int)health + "/" + maxHealth;

        manaBar.fillAmount = mana / maxMana;
        manaText.text = mana + "/" + maxMana;

        if (Time.time >= lastUpdate)
        {
            lastUpdate = Time.time + 1f;
            mana += currentManaRegen;
            UpdateBuffTick();

        }


    }



    void UpdateBuffTick() { 
        for (int i = 0; i < buff.GetLength(0); i++)
            {
                if (buff[i, (int)buffAttribute.Duration] == 0)
                {
                    buff[i, (int)buffAttribute.Stacks] = 0;
                }
                else
                {
                    buff[i, (int)buffAttribute.Duration]--;
                }
            }

        string buffPrintResult = "";
        string[] buffInfo = new string[buff.GetLength(0)];

        buffInfo[(int)buffType.MoveSpeed] = "Speed +";
        buffInfo[(int)buffType.JumpForce] = "Jump +";
        buffInfo[(int)buffType.AttackDamage] = "Attack +";
        buffInfo[(int)buffType.Defense] = "Defense +";


        for (int i = 0; i < buff.GetLength(0); i++)
        {
            if (buff[i, (int)buffAttribute.Stacks] != 0)
            {
                buffPrintResult += buffInfo[i] + buff[i, (int)buffAttribute.Stacks] + " (" + buff[i, (int)buffAttribute.Duration] + ")\n";
            }

        }

        buffText.text = buffPrintResult;
        // Debug.Log("JUMP LV:"+buff[(int)buffType.JumpForce,(int)buffAttribute.Stacks] +" DURATION:"+ buff[(int)buffType.JumpForce,(int)buffAttribute.Duration]);
    }

    public void TakeDamage(float amount) {
        if (isInvincible) {
            return;
        }
        float damage = (amount * (1 - currentDefense));


        GameObject impactEffect = GameObject.Instantiate(impactPrefab, this.transform.position, Quaternion.identity, null);
        Destroy(impactEffect, 1f);

        pb.PlayerAnimation.ShakeCamera();
        health -= damage;

        if (damage >= 1f && !pb.PlayerAnimation.playerAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Action")) {
            pb.PlayerAnimation.setAnimation("Hurt");
        }
        
    }



}
