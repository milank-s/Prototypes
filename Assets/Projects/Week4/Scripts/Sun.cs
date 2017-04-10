using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour {
	public float speed;
	public static bool day;
	public static List<GameObject> listeners;
	// Use this for initialization
	void Awake () {
		
		listeners = new List<GameObject> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
//		speed += Time.deltaTime/100;
		if (transform.position.y < 0) {
			if (day != false) {
				foreach (GameObject g in listeners) {
					g.SendMessage ("Night");
				}
			}
			day = false;
			transform.RotateAround (transform.parent.position, transform.forward, speed * 3);
		} else {
			if(day != true) {
				foreach (GameObject g in listeners) {
					g.SendMessage ("Day");
				}
			}
			day = true;
			transform.RotateAround (transform.parent.position, transform.forward, speed);
		}
//		transform.rotation = Quaternion.identity;

		float heightDist = Mathf.Clamp01(transform.position.y/Vector3.Distance(transform.position, transform.parent.position));

		Color skyColor = new Color (heightDist + 0.2f, heightDist + 0.2f, Mathf.Clamp01(heightDist) + 0.25f);
		Camera.main.backgroundColor = skyColor; 

		foreach (SpriteRenderer s in FindObjectsOfType<SpriteRenderer>()) {
			s.color = skyColor;
		}
	}
}
