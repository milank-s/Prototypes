using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {

	public bool zAxis = false;
	public float distance, speed, noiseScale;
	public string directory;
	public int height, width;
	public GameObject tilePrefab;
	public Vector3 rotation;
	public static GameObject[,] grid;

	float time1, time2;

	private Sprite[] sprites;
	// Use this for initialization

	void Start () {
		sprites = Resources.LoadAll<Sprite>(directory);
		grid = new GameObject[width,height];
	
		SpawnGrid ();

//		StartCoroutine (Animate ());
	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < width; i++) {
			for (int j = 0; j < height; j++) {
				float perlin = Mathf.PerlinNoise ((i + Time.time * speed) * noiseScale, (j + Time.time * speed) * noiseScale);
				SpriteRenderer r = grid [i, j].GetComponent<SpriteRenderer> ();
				r.sprite = sprites[(int)Mathf.Clamp(perlin * (sprites.Length-1), 0, sprites.Length-1)];
//				r.color = new Color(perlin, perlin, perlin);
				r.color = Color.black;
			}
		}
	}

	IEnumerator Animate(){
		for (int i = 0; i < width; i++) {
			for (int j = 0; j < height; j++) {
				float perlin = Mathf.PerlinNoise (i + Time.time * speed, j + Time.time * speed);
//				grid [i, j].transform.position = new Vector3(grid [i, j].transform.position.x, grid [i, j].transform.position.y, distance);
				SpriteRenderer r = grid [i, j].GetComponent<SpriteRenderer> ();
				r.sprite = sprites[(int)Mathf.Clamp(Mathf.PerlinNoise ((i + Time.time) * speed, (j + Time.time) * speed) * (sprites.Length-1), 0, sprites.Length-1)];
			}
			yield return null;
		}
		StartCoroutine (Animate ());
	}

	void SpawnGrid(){

		int index = 0;

		for (int i = 0; i < width; i++) {
			for (int j = 0; j < height; j++) {
				Vector3 newObjectPos = Random.insideUnitCircle.normalized * distance * index;
				//				newObjectPos += transform.position;

				newObjectPos.x = (i * distance) - (width/2 * distance) + transform.position.x;
				if (zAxis) {
					newObjectPos.y = transform.position.y;
					newObjectPos.z = (j * distance) - (height / 2 * distance) + transform.position.z;
				} else {
					newObjectPos.z = (j) - (height / 2) + transform.position.z;
					newObjectPos.y = (j * distance) - (height / 2 * distance) + transform.position.y;
				}

				grid[i,j] = (GameObject)Instantiate (tilePrefab, newObjectPos, Quaternion.identity);
				grid [i, j].transform.rotation = Quaternion.Euler (rotation);

				grid [i, j].transform.parent = transform;
				if (grid [i, j].GetComponent<SpriteRenderer> () == null) {
					grid [i, j].gameObject.AddComponent<SpriteRenderer>();
				}

				SpriteRenderer r = grid [i, j].GetComponent<SpriteRenderer> ();
				r.sprite = sprites [Random.Range(0, sprites.Length)];
				r.sortingOrder = index;
				//				grid [i, j].transform.localScale /=  r.sprite.bounds.size.yp;
				index++;
			}
		}
	}
}
