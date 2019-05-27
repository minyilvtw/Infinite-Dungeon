using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Defense : ItemBase {

    public override void UseItem(GameObject player)
    {
        player.GetComponent<PlayerStatus>().buff[(int)buffType.Defense, (int)buffAttribute.Stacks] += itemLevel;
        player.GetComponent<PlayerStatus>().buff[(int)buffType.Defense, (int)buffAttribute.Duration] = (int)duration;

    }
}
