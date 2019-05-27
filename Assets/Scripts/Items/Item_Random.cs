using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRandom : ItemBase {

    public ItemBase[] itemTypeList;

    
    public override void UseItem(GameObject player)
    {
        for (int i = 0; i < 2; ++i)
        {
            itemTypeList[Random.Range(0, 4)].UseItem(player);
        }   
    }
}
