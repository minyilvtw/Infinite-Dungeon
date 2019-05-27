using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulCollider : MonoBehaviour {

    public bool triggerCheck;
    public BoxCollider2D bc2d;

    void Update() {
        if (GetComponent<Rigidbody2D>().velocity.magnitude <= 0.1f) {
            triggerCheck = true;
            bc2d.enabled = true;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && triggerCheck == true)
        {
            collision.gameObject.GetComponent<PlayerStatus>().money++;
            collision.gameObject.GetComponent<PlayerStatus>().health += 2;
            Destroy(this.gameObject);
        }

    }
}
