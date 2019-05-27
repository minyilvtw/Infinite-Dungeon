using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBase : MonoBehaviour {

    public string itemName;
    public string itemDescription;

    public int itemLevel;
    public bool isStackBuff;
    public float duration;

    public virtual void UseItem(GameObject player) {}


}

