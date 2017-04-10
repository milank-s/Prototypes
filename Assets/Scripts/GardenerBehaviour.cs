using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardenerBehaviour : MonoBehaviour {

	public Sprite[] sprites;
	SpriteRenderer s;

	public int seedCount;
	private int totalSeedCount;
	// Use this for initialization
	void Start () {
		s = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void OnTriggerEnter(Collider col){
		if (col.tag == "seed") {
			col.transform.parent = transform;
			col.transform.localScale *= 0.5f;
			col.transform.localPosition = Vector3.up;
			Destroy (col.GetComponent<BoxCollider> ());
			seedCount++;
			GetComponentInChildren<TextMesh>().text = "they promise me \n" +  (LevelManager.Instance.curLevel.GetComponent<LevelTileManager> ().GetManagedObjectCount () - (seedCount + 5)) + " more seeds \n for my troubles :)";
			if (LevelManager.Instance.curLevel.GetComponent<LevelTileManager> ().GetManagedObjectCount () - seedCount < 6) {
				seedCount = 0;
				LevelManager.Instance.Create ();

			}
//			s.sprite = sprites [0];
		}
		if (col.tag == "plant") {
//			s.sprite = sprites [2];
		}
		if (col.tag == "Untagged") {
//			col.GetComponent<SpriteRenderer> ().flipX = !col.GetComponent<SpriteRenderer> ().flipX;
		}
	}

	public void OnTriggerExit(Collider col){
		if (col.tag == "plant") {
			s.sprite = sprites [1];
		}
	}
}
