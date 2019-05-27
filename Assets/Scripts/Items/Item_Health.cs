using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Health : ItemBase
{
    public override void UseItem(GameObject player) {
        player.GetComponent<PlayerStatus>().health += itemLevel * 25;

    }
}
