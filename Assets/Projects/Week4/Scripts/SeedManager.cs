using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedManager : SpriteSpawner {
	// Use this for initialization

	void Awake () {
		sprites = new List<Sprite> ();
		children =  new List<GameObject> ();
		LoadSprites ();
		Spawn ();
	}

	
	// Update is called once per frame
	void Update () {
	}

	public void OnTriggerEnter(Collider col){
		if (col.tag == "Player") {
//			Spawn (transform.position);
		}
	}

	public void Spawn (Vector3 pos){
		Vector3 newObjectPos = pos;
//		newObjectPos += Vector3.right * Mathf.Sign (Random.Range (-1.00f, 1.00f)) * xDist;
		GameObject newObject = (GameObject)Instantiate (spritePrefab, pos, Quaternion.identity);
		newObject.transform.rotation = Quaternion.Euler (rotation);
		newObject.transform.parent = transform;

		if (newObject.GetComponent<SpriteRenderer> () == null) {
			newObject.AddComponent<SpriteRenderer>();
		}

		SpriteRenderer r = newObject.GetComponent<SpriteRenderer> ();
		Sprite s = sprites [Random.Range (0, sprites.Count-1)];
		r.sprite = s;
		children.Add (newObject);
	}
}
