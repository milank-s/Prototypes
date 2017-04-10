using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
	public float speed;
	Rigidbody r;

	Vector3 pos;

	// Use this for initialization
	void Start () {
		pos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		move ();
//		Vector2 moveDir = new Vector3 (Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical"));
//		r.AddForce (new Vector3(moveDir.x, 0, moveDir.y) * speed);
	}


	void move () {
		if(Input.GetKey(KeyCode.A)) {        // Left
			pos += Vector3.left * Time.deltaTime * speed;
		}
		if(Input.GetKey(KeyCode.D)) {        // Right
			pos += Vector3.right * Time.deltaTime * speed;
		}
		if(Input.GetKey(KeyCode.W)) {        // Up
			pos += Vector3.up * Time.deltaTime * speed;
		}
		if(Input.GetKey(KeyCode.S)) {        // Down
			pos -= Vector3.up * Time.deltaTime * speed;
		}
		transform.position = pos;
//		transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime * speed);
	}
}
