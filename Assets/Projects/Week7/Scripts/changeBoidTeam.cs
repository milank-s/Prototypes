using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeBoidTeam : MonoBehaviour {

	public string boidChar;
	public string tag;
	public Color c;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.GetComponentInChildren<BoidAgent> () != null) {
			if (col.tag == tag) {
				return;
			}
			col.gameObject.GetComponentInChildren<TextMesh> ().color = c;
			col.gameObject.GetComponentInChildren<TextMesh> ().text = boidChar;

			if (tag == "team1") {
				if (col.gameObject.tag == "team2") {
					Spawner.Instance.team2.Remove (col.gameObject);
//					GameObject.Find ("UI").GetComponent<Scoring> ().updateScore (2, -1);
					col.GetComponent<Rigidbody2D> ().velocity *= -2f;
				}
//				GameObject.Find ("UI").GetComponent<Scoring> ().updateScore (1, 1);
				Spawner.Instance.team1.Add (col.gameObject);

			} else {
				if (col.gameObject.tag == "team1") {

					Spawner.Instance.team1.Remove (col.gameObject);
//					GameObject.Find ("UI").GetComponent<Scoring> ().updateScore (1, -1);
					col.GetComponent<Rigidbody2D> ().velocity *= -2f;

				}
				Spawner.Instance.team2.Add (col.gameObject);
//				GameObject.Find ("UI").GetComponent<Scoring> ().updateScore (2, 1);
			}
			col.gameObject.tag = tag;
		}
	}

	public void OnTriggerExit2D(Collider2D col){
		if (col.gameObject.GetComponentInChildren<BoidAgent> () != null) {
			col.gameObject.tag = "neutral";
			col.gameObject.GetComponentInChildren<TextMesh> ().color = new Color(0.9f, 0.9f, 0.9f);
			col.gameObject.GetComponentInChildren<TextMesh> ().text = "^";
		}
	}

	public void OnTriggerStay2D(Collider2D col){
		if (col.gameObject.GetComponentInChildren<BoidAgent> () != null) {
			col.gameObject.tag = tag;
			col.gameObject.GetComponentInChildren<TextMesh> ().color = c;
			col.gameObject.GetComponentInChildren<TextMesh> ().text = boidChar;
			col.GetComponent<Rigidbody2D> ().velocity *= 1.25f;
		}
	}
}
