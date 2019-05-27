using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour {

    public Sprite emptyItemSprite;

    public Image[] itemsImage;
    public Item[] inventoryItems = new Item[3];

    void Update() {
        for (int i = 0; i < inventoryItems.Length; i++) {
            if (inventoryItems[i] != null)
            {
                itemsImage[i].sprite = inventoryItems[i].artwork;

            }
            else {
                itemsImage[i].sprite = emptyItemSprite;
            }

        }
    }

    public bool CheckInventorySpace()
    {

        foreach (Item x in inventoryItems)
        {
            if (x == null)
            {
                return true;
            }
        }
        return false;
    }

    public void UseItem(int position) {
        if (inventoryItems[position] != null) {
            inventoryItems[position].itemEffect.UseItem(this.gameObject);
            inventoryItems[position] = null;
            DestroyImmediate(inventoryItems[position]);
        }
      
    }

    public void AddItem(Item item) {
        if (CheckInventorySpace())
        {
            for (int i = 0; i < inventoryItems.Length; i++) {
                if (inventoryItems[i] == null) {
                    inventoryItems[i] = item;
                    break;
                }
            }

        }
    }


}
