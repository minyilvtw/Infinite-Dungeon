using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Attack : ItemBase
{

    public override void UseItem(GameObject player)
    {
        player.GetComponent<PlayerStatus>().buff[(int)buffType.AttackDamage, (int)buffAttribute.Stacks] += itemLevel;
        player.GetComponent<PlayerStatus>().buff[(int)buffType.AttackDamage, (int)buffAttribute.Duration] = (int)duration;

    }
}
