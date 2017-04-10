using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bud : SpriteSpawner{

	// Use this for initialization
	void Start () {
		LoadSprites ();
	}
	
	// Update is called once per frame
	void Update () {

	}


	void SwapSprite(){
		GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Count)];
	}
}
