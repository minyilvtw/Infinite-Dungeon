using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackZoneCheck : MonoBehaviour {

    public List<GameObject> objects;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        objects.Add(collision.gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        objects.Remove(collision.gameObject);
    }

}
