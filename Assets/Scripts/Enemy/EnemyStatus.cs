using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStatus : MonoBehaviour {

    public bool isAlive = true;
    public bool isBoss;
    [Space]
    public int level;
    [HideInInspector]
    public float health;
    public float maxHealth;
    public float maxHealthPerLevel;
    public float moveSpeed;
    public float moveSpeedPerLevel;
    public float attackDamage;
    public float attackDamagePerLevel;

    [Tooltip("Chances to freeze after damage")]
    public float resilience;
    public float resiliencePerLevel;

    public int minLoot;
    public int maxLoot;

    public GameObject lootPrefab;
    public GameObject impactPrefab;

    public Animator animator;
    public Image healthBar;


	void Start () {
        maxHealth += maxHealthPerLevel * (level - 1);
        moveSpeed += moveSpeedPerLevel * (level - 1);
        attackDamage += attackDamagePerLevel * (level - 1);
        resilience += resiliencePerLevel * (level - 1);

        health = maxHealth;
	}
	
	// Update is called once per frame
	void Update () {
        healthBar.fillAmount = health / maxHealth;
	}

    void Dead()
    {
        isAlive = false;
        animator.SetBool("Die", true);

        AudioSource.PlayClipAtPoint(GetComponent<EnemySFX>().dead, this.transform.position);

        if (isBoss)
        {
            GameObject.Find("GameManager").GetComponent<LevelManager>().bossDefeated = true;
        }

        Destroy(this.gameObject, 1f);

        for (int i = 0; i < Random.Range(minLoot, maxLoot); i++)
        {
            GameObject loot = GameObject.Instantiate(lootPrefab, this.transform.position, Quaternion.identity, null);
            var randomDirection = new Vector2(Mathf.Abs(Random.insideUnitCircle.x), Mathf.Abs(Random.insideUnitCircle.y) * 2f);
            loot.GetComponent<Rigidbody2D>().AddForce(randomDirection * 200f);
        }
    }

    public void TakeDamage(float damageAmount, float impact, bool directionLeft, GameObject source) {

        if (isAlive) {
            health -= damageAmount;


            //AudioSource.PlayClipAtPoint(GetComponent<EnemySFX>().hurt, this.transform.position);

            if (Random.Range(0f, 100f) >= resilience * 100) {
                animator.SetTrigger("Hurt");
                this.GetComponent<EnemyMovement>().nextUpdate = Time.time;  
            }

            //Vector2 dir = source.gameObject.transform.position - this.gameObject.transform.position;
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);

            if (directionLeft)
            {
                GetComponent<Rigidbody2D>().AddForce(Vector2.left * impact);
            }
            else
            {
                GetComponent<Rigidbody2D>().AddForce(Vector2.right * impact);
            }

            GameObject impactEffect = Instantiate(impactPrefab, transform.position, Quaternion.identity, null);
            impactEffect.GetComponent<DamageNumber>().damageAmount = damageAmount;
            Destroy(impactEffect, 1f);

            source.GetComponentInParent<PlayerAnimation>().ShakeCamera();

            if (health <= 0)
            {
                Dead();
            }
        }
    }
}
