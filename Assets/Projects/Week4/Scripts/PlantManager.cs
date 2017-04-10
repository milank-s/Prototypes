using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantManager : SpriteSpawner {

	private SeedManager seedManager;
	public GameObject seed;
	private List<GameObject> objectsToDelete;
	// Use this for initialization

	void Start () {
		sprites = new List<Sprite> ();
		children =  new List<GameObject> ();
		LoadSprites ();
		Sun.listeners.Add (gameObject);
//		InvokeRepeating ("Wither", 0, 1);
	}

	void Update(){
		if (Camera.main.orthographicSize < children.Count) {
			Camera.main.orthographicSize = children.Count;
			Camera.main.transform.position += Vector3.up;
		}
	}

	public void Day(){
		CancelInvoke ();
	}

	void Wither(){
		if (children.Count > 0) {
			GameObject prune = children [children.Count - 1];
			children.Remove (prune);
			prune.AddComponent<FadeSprite> ();
			foreach (Transform g in prune.GetComponentsInChildren<Transform>()) {
				g.gameObject.AddComponent<FadeSprite> ();
			}
		}
	}

	void Night(){
		InvokeRepeating ("Wither", 0, 0.1f);
	}

	public override void Spawn (){
		Vector3 newObjectPos = transform.position;
		newObjectPos += transform.up * children.Count * yDist;
		newObjectPos += Vector3.right * Random.Range (-1.00f, 1.00f) * xDist;
		GameObject newObject = (GameObject)Instantiate (spritePrefab, newObjectPos, Quaternion.identity);
		newObject.transform.rotation = Quaternion.Euler (rotation);
		newObject.transform.parent = transform;
		if (newObject.GetComponent<SpriteRenderer> () == null) {
			newObject.AddComponent<SpriteRenderer>();
		}

		SpriteRenderer r = newObject.GetComponent<SpriteRenderer> ();
		newObject.transform.position += (transform.up * r.bounds.size.y)/2;
		Sprite s = sprites [Random.Range (0, sprites.Count)];

		r.sprite = s;
		children.Add (newObject);
	}

	public void OnTriggerEnter(Collider col){
		if(col.tag == "Player" && Sun.day){
			foreach (GameObject g in children) {
				
				if (g.transform.childCount < 25) {
					g.SendMessage ("Spawn");
					g.GetComponent<SpriteRenderer> ().sprite = sprites [Random.Range (0, sprites.Count-1)];
				}
			}
			Spawn();
		}
	}
}
