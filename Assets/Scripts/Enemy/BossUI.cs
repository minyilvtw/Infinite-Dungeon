using UnityEngine;
using UnityEngine.UI;


public class BossUI : MonoBehaviour {

    public GameObject canvas;
    public Image healthBar;

    private EnemyStatus bossStatus;

	void Start () {
        bossStatus = GetComponent<EnemyStatus>();
	}
	
	void Update () {
        healthBar.fillAmount = bossStatus.health / bossStatus.maxHealth;

        if (bossStatus.health <= 0) {
            Destroy(canvas, 1f);
        }
	}
}
