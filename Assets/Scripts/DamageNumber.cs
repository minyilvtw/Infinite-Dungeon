using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageNumber : MonoBehaviour {

    public float damageAmount;

	// Use this for initialization
	void Start () {
        GetComponentInChildren<Text>().text = ""+(int)damageAmount;
	}
	
	// Update is called once per frame
    void Update()
    {

        this.transform.position += new Vector3((Random.insideUnitSphere.x * 0.02f), 0.02f, 0);
    }
}
