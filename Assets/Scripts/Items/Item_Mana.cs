
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Mana : ItemBase {

    public override void UseItem(GameObject player)
    {
        player.GetComponent<PlayerStatus>().buff[(int)buffType.ManaRegen, (int)buffAttribute.Stacks] += itemLevel;
        player.GetComponent<PlayerStatus>().buff[(int)buffType.ManaRegen, (int)buffAttribute.Duration] = (int)duration;

    }
}
