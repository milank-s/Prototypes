using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour {

	GameObject sun;
	public float speed  = 2;
	public float maxDistance  = 2;
	public float propulsion = 1;

	float panout = 0;
	bool endGame = false;
	int index;
	// Use this for initialization
	void Start () {
		sun = GameObject.Find ("Sun");

		index = 0;
	}

	// Update is called once per frame
	void Update () {
		AddSpeed ();
//		ChangePlanetSpeed ();

		if (endGame) {
			GetComponent<Collider2D> ().enabled = false;
			Camera.main.fieldOfView += Time.deltaTime * 2;
			sun.GetComponent<Rotate> ().z -= Time.deltaTime / 100;
			Vector3 cameraPos = sun.transform.position;
			cameraPos.z = -10;
			Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, cameraPos, Time.deltaTime);
			if (Camera.main.fieldOfView > 170) {
				SceneManager.LoadScene("Title1") ;
			}

		}

		transform.RotateAround (transform.parent.position, transform.forward, speed);
	}

	void AddSpeed(){
		if (Input.GetKey (KeyCode.Space)) {
			Vector3 target = (transform.position - transform.parent.position).normalized * (transform.parent.localScale.x + maxDistance);
			transform.position = Vector3.MoveTowards (transform.position, transform.parent.position + target, Time.deltaTime * propulsion);
		} else if (Vector3.Distance (transform.parent.position, transform.position) > transform.parent.localScale.x * 2) {
			transform.position = Vector3.MoveTowards (transform.position, transform.parent.position, Time.deltaTime * propulsion);
		}
	}


	void ChangePlanetSpeed(){
		if(Input.GetKey (KeyCode.RightArrow)){
			speed -= Time.deltaTime *10;
		}
		if (Input.GetKey (KeyCode.LeftArrow)) {
			speed += Time.deltaTime *10;
		}
		speed = Mathf.Clamp (speed, -3, 3);
	}

	public void OnTriggerEnter2D(Collider2D col){
		transform.parent = col.transform;
		col.gameObject.transform.parent = transform;

		if (col.name == "Sun") {
			endGame = true;
			Camera.main.GetComponent<CameraControl> ().enabled = false;
			col.GetComponent<Rotate> ().enabled = true;

			for (int i = 1; i < sun.transform.childCount; i+=2) {
				sun.transform.GetChild (i).parent = sun.transform.GetChild (i - 1).transform;
			}
		}

			
	}
}
