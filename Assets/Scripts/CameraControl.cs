using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {

	private Transform player;

	Rigidbody2D r;
	public float shakeTimer = 0;
	public float intensity = 1000;
	public float followIntensity = 100;
	public float lookDistance = 10;
	public float zDistance;

	private Vector3 velocity = Vector3.zero;
	// Use this for initialization
	void Start () {
		r = GetComponent<Rigidbody2D> ();
//		transform.position = player.position;
		player = PrefabManager.Instance.PLAYER.transform;
	}

	// Update is called once per frame
	void Update () {
		ShakeScreen ();
		FollowPlayer ();

	}

	void FollowPlayer(){
		transform.position = Vector3.SmoothDamp (transform.position, new Vector3(player.position.x, player.position.y, zDistance), ref velocity, 0.5f);
	}

	public void ShakeScreen(){
		if (shakeTimer > 0) {
			shakeTimer -= Time.deltaTime;
			r.AddForce (Random.insideUnitSphere * intensity);
		}
	}
}
