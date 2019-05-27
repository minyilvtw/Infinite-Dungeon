using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollider : MonoBehaviour {

    
    public int price;
    public GameObject itemInfoCanvas;

    public Image itemIcon;
    public Text itemNameText;
    public Text itemLevelText;
    public Text itemDescriptionText;
    public Text itemCostText;

    private Item item;

    void Start() {
        item = new Item();
        item.itemEffect = GetComponent<ItemBase>();

        item.itemName = GetComponent<ItemBase>().itemName;
        item.name = GetComponent<ItemBase>().itemName; // attached asset name

        item.artwork = GetComponent<SpriteRenderer>().sprite;

        itemIcon.sprite = item.artwork;
        itemNameText.text = "" + item.itemEffect.itemName;
        itemDescriptionText.text = item.itemEffect.itemDescription;
        itemLevelText.text = "Lv. " + item.itemEffect.itemLevel;
        itemCostText.text = "COST: "+ price;

    }



    private void OnTriggerStay2D(Collider2D collision)
    {

        
        if (collision.tag == "Player")
        {
            itemInfoCanvas.SetActive(true);

            if (Input.GetKeyDown(KeyCode.B)) {
                if (collision.gameObject.GetComponent<InventoryController>().CheckInventorySpace() 
                    && collision.gameObject.GetComponent<PlayerStatus>().money >= price)
                {
                    collision.gameObject.GetComponent<PlayerStatus>().money -= price;
                    
                    collision.gameObject.GetComponent<InventoryController>().AddItem(item);
                    
                    Destroy(this.gameObject);
                }
            }
            
           
        }

    }

    private void OnTriggerExit2D(Collider2D collision) {
        itemInfoCanvas.SetActive(false);
    }

}
