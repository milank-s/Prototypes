using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer3D : MonoBehaviour {

	public string movementAxis;
	public float speed;
	public Vector3 angularVelocity;
	public float turnRadius;

	private Vector3 lastInput, newInput;
	Rigidbody r;



	// Use this for initialization
	void Start () {
		r = GetComponent < Rigidbody> ();
		lastInput = transform.up;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (Input.GetAxis ("Horizontal" + movementAxis) != 0 || Input.GetAxis ("Vertical" + movementAxis) != 0) {
			float ratio = Mathf.Clamp01 (turnRadius * Time.fixedDeltaTime);
			newInput = new Vector3 (Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical"), 0).normalized;
			Vector3 force = (newInput * ratio) + (lastInput * (1-ratio));
			r.velocity = force * speed;
			lastInput = force;
		}
	}
}
	
