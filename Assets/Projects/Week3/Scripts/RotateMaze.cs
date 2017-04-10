using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateMaze : MonoBehaviour {
	public Transform[] impactPoints;
	public float maxAngle;
	public float force;
	public float turnSpeed;
	Rigidbody r;
	Vector2 angle;
	// Use this for initialization
	void Start () {
		r = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Rotate ();
	}

	void Rotate () {
		if(Input.GetKey(KeyCode.A)) {        // Left
			r.AddTorque(-Vector3.forward * force, ForceMode.Impulse);
		}
		if(Input.GetKey(KeyCode.D)) {        // Right
			r.AddTorque(Vector3.forward * force, ForceMode.Impulse);
//			r.AddForceAtPosition(transform.up * force, impactPoints[2].position);
		}
		if(Input.GetKey(KeyCode.W)) {        // Up
			r.AddTorque(Vector3.right * force, ForceMode.Impulse);
//			r.AddForceAtPosition(transform.up * force, impactPoints[0].position);
		}
		if(Input.GetKey(KeyCode.S)) {        // Down
//			r.AddTorque(Vector3.forward * force, ForceMode.Impulse);
			r.AddTorque(-Vector3.right* force, ForceMode.Impulse);
//			r.AddForceAtPosition(transform.up * force, impactPoints[1].position);
		}
		Mathf.Clamp (angle.x, -maxAngle, maxAngle);
		Mathf.Clamp (angle.y, -maxAngle, maxAngle);
//		r.MoveRotation(Quaternion.Euler(angle.x, 0, angle.y))
		//			Vector3.MoveTowards(transform.position, pos, Time.deltaTime * speed);

	}
}
