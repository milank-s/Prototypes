using UnityEngine;
using System.Collections;

public class CursorMovement : MonoBehaviour {

	Transform[] planets;
	GameObject curPlanet;
	GameObject sun;
	float speed;

	int index;
	// Use this for initialization
	void Start () {
		speed = 1;
		sun = GameObject.Find ("Sun");
		planets = new Transform[sun.transform.childCount];
		for (int i = 0; i < sun.transform.childCount; i++){
			planets[i] = sun.transform. GetChild(i);
		}

		index = planets.Length-1;
		curPlanet = planets [index].gameObject;
		transform.parent = curPlanet.transform;
	}

	// Update is called once per frame
	void Update () {
		AddSpeed ();
		ChangePlanetSpeed ();

		transform.RotateAround (transform.parent.position, transform.forward, speed);
	}

	void AddSpeed(){
		if (Vector3.Distance (transform.parent.position, transform.position) > 0.25f) {
			if (Input.GetKey (KeyCode.DownArrow)) {
				transform.position = Vector3.MoveTowards (transform.position, transform.parent.position, Time.deltaTime * 2);
			}
		}
		if (Vector3.Distance (transform.position, transform.parent.position) < 3f) {
			if (Input.GetKey (KeyCode.UpArrow)) {
				transform.position = Vector3.MoveTowards (transform.position, transform.parent.position, -Time.deltaTime * 2);
			}
		}
	}

	void ChangePlanetSpeed(){
		if(Input.GetKey (KeyCode.RightArrow)){
			speed -= Time.deltaTime *10;
		}
		if (Input.GetKey (KeyCode.LeftArrow)) {
			speed += Time.deltaTime *10;
		}
		speed = Mathf.Clamp (speed, -10, 10);
	}

	public void OnTriggerEnter2D(Collider2D col){
		transform.parent = col.transform;
		Debug.Log (transform.parent + ", " + col.name);
	}
}
