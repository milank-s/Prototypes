using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour {

	public float speed, x, y, z, xOffset, yOffset, zOffset;
	public GameObject target;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate () {
		Vector3 t = target.transform.position;
		t.x += xOffset;
		t.z += zOffset;
		t.y += yOffset;
		transform.position = Vector3.Lerp(transform.position, t, speed* Time.deltaTime);
	}
}
