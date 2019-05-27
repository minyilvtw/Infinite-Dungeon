using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTile : MonoBehaviour {

    public Sprite[] sprite;

	// Use this for initialization
	void Start () {
        int randomTile = Random.Range(0, sprite.Length);
        GetComponent<SpriteRenderer>().sprite = sprite[randomTile];

	}
	

}
