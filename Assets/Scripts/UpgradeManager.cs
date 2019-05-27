using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour {

    public float perLevelMoveSpeed;
    public float perLevelJumpForce;
    public float perLevelMaxHealth;
    public float perLevelDamageFactor;
    public float perLevelAttack;
    public float perLevelManaRegen;
    
    [Space]
    public GameObject cardCanvas;
    public GameObject[] upgradeCardsButton;

    public List<Upgrade> upgradeList;
    [HideInInspector]
    public List<Upgrade> renewList;

	void Start () {
        renewList = new List<Upgrade>(upgradeList);
	}

    public void OpenUpgradeCanvas() {

        Debug.Log("Opening Upgrades");
 
        cardCanvas.SetActive(true);
        InitUpgrade();
    }

    void InitUpgrade() {

        if (upgradeList.Count == 0) {
            upgradeList = new List<Upgrade>(renewList);
        }

        for (int i = 0; i < upgradeCardsButton.Length; i++) {
            upgradeCardsButton[i].GetComponent<Button>().onClick.RemoveAllListeners();

            int random = Random.Range(0, upgradeList.Count);
            Upgrade upgradeCard = upgradeList[random];
            upgradeList.RemoveAt(random);

            upgradeCardsButton[i].GetComponentsInChildren<Image>()[1].sprite = upgradeCard.artwork;
            upgradeCardsButton[i].GetComponentsInChildren<Text>()[0].text = upgradeCard.upgradeName;
            upgradeCardsButton[i].GetComponentsInChildren<Text>()[1].text = upgradeCard.description;
            upgradeCardsButton[i].GetComponent<Button>().onClick.AddListener(() => Upgrade(upgradeCard.upgradeName));

            
        }

    }


    void Upgrade(string upgradeName) {

        cardCanvas.SetActive(false);

        Time.timeScale = 1f;

        PlayerStatus playerStatus = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>();
        switch (upgradeName) {
            case "UpMoveSpeed":
                playerStatus.moveSpeed += perLevelMoveSpeed; 
                break;
            case "UpMaxHealth":
                playerStatus.maxHealth += perLevelMaxHealth;
                playerStatus.health = playerStatus.maxHealth;
                break;
            case "UpAttack":
                playerStatus.attackDamage += perLevelAttack;
                break;
            case "UpJumpForce":
                playerStatus.jumpForce += perLevelJumpForce;
                break;
            case "UpDefense":
                playerStatus.defense += perLevelDamageFactor;
                break;
            case "UpManaRegen":
                playerStatus.manaRegen += perLevelManaRegen;
                break;
            default:
                break;

        }

    }
}
