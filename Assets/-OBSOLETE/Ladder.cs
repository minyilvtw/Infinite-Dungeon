﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour {

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player" && collider.gameObject.GetComponent<PlayerMovement>().isGrounded)
        {
            //collider.gameObject.GetComponent<PlayerMovement>().canClimb = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
           // collider.gameObject.GetComponent<PlayerMovement>().canClimb = false;
        }
    }
}
