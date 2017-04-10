using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMovement : MonoBehaviour {
	public bool zMovement;
	public float distance; 
	public Sprite[] sprites;
	public AudioClip[] sounds;
	SpriteRenderer s;
	float targetDistance = 20;
	public int food = 100;
	public int tilesCreated = 0;
	public float animResetInterval = 0.25f;
	private float interval;
	private bool gameOn = false;
	public TextMesh steps;

	private GameObject h, b;
	// Use this for initialization
	void Start () { 
		b = Instantiate (PrefabManager.Instance.TILE, transform.position, Quaternion.identity) as GameObject;
		b.GetComponent<TextMesh> ().text = "LOST?";
		b.GetComponent<TextMesh> ().fontSize = 96;
		b.GetComponent<TextMesh> ().color = Color.gray;
		b.GetComponent<Tile> ().isPassable = true;
		b.tag = "base";
		b.GetComponent<BoxCollider2D> ().size = new Vector2 (100, 100);

		h = Instantiate (PrefabManager.Instance.TILE, (Random.insideUnitCircle.normalized * 80) + (Vector2) transform.position, Quaternion.identity) as GameObject;
		h.GetComponent<TextMesh> ().text = "HOME";
		h.GetComponent<TextMesh> ().fontSize = 200;
		h.GetComponent<TextMesh> ().color = Color.white;
		h.GetComponent<Tile> ().isPassable = true;
		h.GetComponent<BoxCollider2D> ().size = new Vector2 (100, 100);
		h.tag = "home";

		StartCoroutine(ResetGame ());
//		s = GetComponentInChildren<SpriteRenderer> ();
	}

	IEnumerator EndGame(){

		float t = 0;
		while (t < 1) {
			t += Time.deltaTime/2;
			Camera.main.orthographicSize = Mathf.Lerp (Camera.main.orthographicSize, 80, Mathf.Pow (t, 2));
			yield return null;
		}

		t = 0;
		LevelManager.Instance.curLevel.GeneratePieceWise (10, 10);
		h.transform.position = (Random.insideUnitCircle.normalized * 80) + (Vector2)transform.position;
		b.transform.position = transform.position;

		while (t < 1) {
			t += Time.deltaTime;
			Camera.main.orthographicSize = Mathf.Lerp (Camera.main.orthographicSize, 20, Mathf.Pow (t, 2));
			yield return null;
		}
	}

	IEnumerator ResetGame(){
		
		float t = 0;

		while (t < 1) {
			t += Time.deltaTime/2;
			Camera.main.orthographicSize = Mathf.Lerp (Camera.main.orthographicSize, 80, Mathf.Pow (t, 2));
			yield return null;
		}

		t = 0;
		LevelManager.Instance.curLevel.wipeMap ();

		while (t < 1) {
			t += Time.deltaTime;
			transform.position = Vector3.Lerp (transform.position, Vector3.zero, Mathf.Pow(t, 2));
			yield return null;
		}

		LevelManager.Instance.curLevel.GeneratePieceWise (15, 15);

		StartCoroutine(StartGame ());
	}

	IEnumerator StartGame(){
		float t = 0;

		LevelManager.Instance.curLevel.GenerateChunk (transform.position);


		t = 0;

		while (t < 1) {
			t += Time.deltaTime;
			Camera.main.orthographicSize = Mathf.Lerp (Camera.main.orthographicSize, 20, Mathf.Pow (t, 2));
			yield return null;
		}

		gameOn = true;
		food = 100;
		steps.text = food.ToString () + " steps";	
	}
	// Update is called once per frame
	void Update () {
		if (gameOn){
			if (food <= 1) {
				gameOn = false;
				StartCoroutine (ResetGame ());
			} else {
				Move ();
			}
		}


	}

	void Move(){

		if (interval <= 0) {
			if (Input.GetKey (KeyCode.D)) {
//				s.flipX = false;
				if (Step (Vector3.right)) {
//					LevelManager.Instance.curLevel.xOrigin ++;
//					LevelManager.Instance.curLevel.GenerateChunk ();
				}
			}

			if (Input.GetKey (KeyCode.A)) {
//				s.flipX = true;
				if (Step (-Vector3.right)) {
//					LevelManager.Instance.curLevel.xOrigin--;
//					LevelManager.Instance.curLevel.GenerateChunk ();
				}
			}
			if (Input.GetKey (KeyCode.W)) {
				if (Step (Vector3.up)) {
//					LevelManager.Instance.curLevel.yOrigin++;	
//					LevelManager.Instance.curLevel.GenerateChunk ();
				}
			}
			if (Input.GetKey (KeyCode.S)) {
				if (Step (-Vector3.up)) {
//					LevelManager.Instance.curLevel.yOrigin--;	
//					LevelManager.Instance.curLevel.GenerateChunk ();
				}
			}
		}
		interval -= Time.deltaTime;
	}

	public void OnTriggerEnter2D(Collider2D col){
		if (col.tag == "seed") {
			food += 50;
			col.GetComponent<TextMesh> ().color = Color.gray;
			col.enabled = false;
			col.gameObject.SetActive (false);
		}
		if (col.tag == "home") {
			
			targetDistance = 100;
			StartCoroutine(EndGame ());
		}

		if (col.tag == "base") {
			if (food < 100) {
				food = 100;
			}
		}
	}	

	bool Step(Vector3 direction){
		Vector2 newPos = transform.position + direction;

		if(!LevelManager.Instance.curLevel.mapIndices.ContainsKey(newPos)){
			LevelManager.Instance.curLevel.GeneratePieceWise (3, 3);
			tilesCreated++;
		}

		GameObject currentTile = LevelManager.Instance.curLevel.mapIndices [newPos];
		if (currentTile.GetComponent<Tile> ().isPassable) {
			
			transform.position += direction;
			currentTile.SetActive (true);
			interval = animResetInterval;
			food--;
			steps.text = food.ToString () + " steps";	
			return true;

		}
//		GetComponent<AudioSource> ().PlayOneShot (sounds [0]);
//		 if (LevelManager.Instance.curLevel.IsTilePassable (transform.position + direction)) {
////			GetComponent<SpriteRenderer> ().sortingOrder = -(int)transform.position.y;
//			LevelManager.Instance.curLevel._tiles[(int)transform.position.x, (int)transform.position.y].GetComponent<TextMesh>().color = Color.red;
//			interval = animResetInterval;
////			transform.position += direction;
//			return true;
//		}
		return false;
//		transform.position = LevelManager.Instance.curLevel.IsMoveOpen ((Vector2)((transform.position / distance) + direction - LevelManager.Instance.curLevel.transform.position));
	}
}
