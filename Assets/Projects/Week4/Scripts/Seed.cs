using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : SpriteSpawner{

	// Use this for initialization
	void Start () {
		LoadSprites ();
		InvokeRepeating ("SwapSprite", 0, 0.1f);
	}
	
	// Update is called once per frame
	void Update () {
		if (!Sun.day) {
			Sun.listeners.Remove (gameObject);
			Destroy (gameObject, 1f);
		}

	}


	void SwapSprite(){
		GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Count)];
	}
}
