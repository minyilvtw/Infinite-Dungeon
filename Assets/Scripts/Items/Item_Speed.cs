using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Speed : ItemBase
{

    public override void UseItem(GameObject player)
    {
        player.GetComponent<PlayerStatus>().buff[(int)buffType.MoveSpeed, (int)buffAttribute.Stacks] += itemLevel;
        player.GetComponent<PlayerStatus>().buff[(int)buffType.MoveSpeed, (int)buffAttribute.Duration] = (int)duration;
    }
}
