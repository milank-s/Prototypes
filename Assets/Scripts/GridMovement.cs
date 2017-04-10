using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMovement : MonoBehaviour {
	public bool zMovement;
	public float distance; 
	public Sprite[] sprites;
	public AudioClip[] sounds;
	SpriteRenderer s;

	public float animResetInterval = 0.25f;
	private float interval;

	// Use this for initialization
	void Start () {
		s = GetComponentInChildren<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		Move ();
	}

	void Move(){
		Vector3 move;
		if(interval <= 0){
			if (Input.GetKey (KeyCode.D)) {
				s.flipX = false;
				Step (Vector3.right);
			}

			if (Input.GetKey (KeyCode.A)) {
				s.flipX = true;
				Step (-Vector3.right);
				interval = animResetInterval;

			}
			if (Input.GetKey (KeyCode.W)) {
				Step (Vector3.up);
				interval = animResetInterval;
			}
			if (Input.GetKey (KeyCode.S)) {
				Step (-Vector3.up);
				interval = animResetInterval;
			}
		}
		interval -= Time.deltaTime;
	}

	void Step(Vector3 direction){

//		GetComponent<AudioSource> ().PlayOneShot (sounds [0]);
		transform.position = LevelManager.Instance.curLevel.IsMoveOpen ((Vector2)((transform.position / distance) + direction - LevelManager.Instance.curLevel.transform.position));
		GetComponent<SpriteRenderer> ().sortingOrder = -(int)transform.position.y;
		LevelManager.Instance.curLevel.GeneratePieceWise (10, 10);

		if (LevelManager.Instance.curLevel.GetTile ((Vector2)(transform.position)/distance) == null) {
//			LevelTileManager.Instance.ObjectFactory(6, (Vector2)transform.position);
		}
		interval = animResetInterval;
	}
}
