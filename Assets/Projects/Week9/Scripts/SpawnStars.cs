using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnStars : MonoBehaviour {

	public GameObject star;
	public int amount;
	public float distance;
	// Use this for initialization
	void Start () {
		for (int i = 0; i < amount; i++) {
			GameObject newStar = Instantiate (star, Random.insideUnitCircle * Camera.main.orthographicSize * 2, Quaternion.identity) as GameObject;
			newStar.transform.localScale *= Random.Range (0.5f, 2f);
		}
	}

	public void SpawnStar(){
		GameObject newStar = Instantiate (star, (Random.insideUnitCircle *  Camera.main.orthographicSize * 2) + (Vector2)transform.position, Quaternion.identity) as GameObject;
		newStar.transform.localScale *= Random.Range (0.5f, 1.5f);
	}
}
