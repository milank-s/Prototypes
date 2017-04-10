using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour {

	public string movementAxis;
	public float speed, maxSpeed;
	Rigidbody2D r;

	// Use this for initialization
	void Start () {
		r = GetComponent < Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetAxis ("Horizontal" + movementAxis) != 0 || Input.GetAxis ("Vertical" + movementAxis) != 0) {
			r.AddForce (new Vector2(speed * Input.GetAxis ("Horizontal" + movementAxis) * Time.deltaTime, 0));
			r.AddForce (new Vector2(0, speed * Input.GetAxis ("Vertical" + movementAxis) * Time.deltaTime));
			r.velocity = new Vector2 (Mathf.Clamp (r.velocity.x, -maxSpeed, maxSpeed), Mathf.Clamp (r.velocity.y, -maxSpeed, maxSpeed));
		}
	}
}
