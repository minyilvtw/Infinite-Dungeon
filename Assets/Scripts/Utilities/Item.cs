using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "New Item")]
public class Item : ScriptableObject
{

    public string itemName;
    public string description;
    public Sprite artwork;

    public ItemBase itemEffect;

}
