using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderPlatform : MonoBehaviour {

    void Update() {

        if (Input.GetKeyDown(KeyCode.DownArrow)) {
            GetComponent<PlatformEffector2D>().rotationalOffset = 180;
        }

        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            GetComponent<PlatformEffector2D>().rotationalOffset = 0;
        }
    }

}
