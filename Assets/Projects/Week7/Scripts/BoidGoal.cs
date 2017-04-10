using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidGoal : MonoBehaviour {

	public int team;
	public Scoring scoring;
	public Spawner boidSpawner;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnCollisionEnter2D(Collision2D col){
		if (col.gameObject.tag == gameObject.tag) {
			scoring.updateScore (team, 1);
			boidSpawner.delete (col.gameObject);
			GameObject newText = Instantiate (PrefabManager.Instance.TEXT, Vector3.zero, Quaternion.identity) as GameObject;
			newText.GetComponent<TextMesh> ().text = "+1";
			newText.GetComponent<TextMesh> ().color = GetComponent<SpriteRenderer> ().color;
			newText.GetComponent<TextStyling> ().fade = true;
			newText.GetComponent<TextStyling> ().delete = true;
			newText.GetComponent<TextStyling> ().speed = 0.33f;
			newText.transform.position = transform.position + ((Vector3.one) * 3);
			newText.transform.localScale *= 2;
		}
	}
}
