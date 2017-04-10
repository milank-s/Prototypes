using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantPart : SpriteSpawner {

	// Use this for initialization
	void Start () {
		children = new List<GameObject> ();
		sprites = new List<Sprite> ();
		LoadSprites ();
	}
	
	// Update is called once per frame
	void Update () {
	}

	void Wilt(){
		foreach (GameObject g in children) {
			g.GetComponent<Rigidbody2D> ().gravityScale = 0.1f;
			g.GetComponent<Rigidbody2D> ().AddTorque (15);
		}
	}

	void Night(){
		foreach (GameObject g in children) {
			g.GetComponent<SpriteRenderer> ().sprite = sprites [Random.Range (0, sprites.Count)];
		}
	}

	void Day(){
	}

	public override void Spawn (){
		Vector3 newObjectPos = transform.position;
		newObjectPos += Vector3.right * Random.Range (-1.00f, 1.00f) * children.Count/xDist;
		newObjectPos += Vector3.up * yDist;
		GameObject newObject = (GameObject)Instantiate (spritePrefab, newObjectPos, Quaternion.identity);
		newObject.transform.rotation = Quaternion.Euler (rotation);
		newObject.transform.Rotate (0,0,Random.Range(-180, 180) * Mathf.Sign(Random.Range (-1.00f, 1.00f)));
		newObject.transform.parent = transform;
		if (newObject.GetComponent<SpriteRenderer> () == null) {
			newObject.AddComponent<SpriteRenderer>();
		}

		SpriteRenderer r = newObject.GetComponent<SpriteRenderer> ();
		newObject.transform.position += (transform.up * r.bounds.size.y)/2;
		Sprite s = sprites [Random.Range (0, sprites.Count-1)];
		r.sprite = s;
//		sprites.Remove (s);
		children.Add (newObject);
	}
}
