using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_JumpForce : ItemBase {

    public override void UseItem(GameObject player)
    {
        player.GetComponent<PlayerStatus>().buff[(int)buffType.JumpForce, (int)buffAttribute.Stacks] += itemLevel;
        player.GetComponent<PlayerStatus>().buff[(int)buffType.JumpForce, (int)buffAttribute.Duration] = (int)duration;
    }
}
