using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSpawner: MonoBehaviour {

	public bool createGrid;
	public float xDist, yDist, zDist;
	public string directory;
	public int height, width;
	public GameObject spritePrefab;
	public Vector3 rotation;
	public static GameObject[,] grid;

	protected List<Sprite> sprites;
	protected List<GameObject> children;
	protected Sprite[] spriteArray;

	// Use this for initialization

	public SpriteSpawner(){
		sprites = new List<Sprite> ();
		children =  new List<GameObject> ();
	}

	protected void LoadSprites(){
		spriteArray =  (Resources.LoadAll<Sprite> (directory));
		sprites.AddRange (spriteArray);
	}

	void Start () {
		LoadSprites ();
		if (createGrid) {
			SpawnGrid ();
		}
	}

	// Update is called once per frame
	void Update () {

	}

	public virtual void Spawn (){
		Vector3 newObjectPos = transform.position;

		GameObject newObject = (GameObject)Instantiate (spritePrefab, newObjectPos, Quaternion.identity);
		newObject.transform.rotation = Quaternion.Euler (rotation);
		newObject.transform.parent = transform;
		if (newObject.GetComponent<SpriteRenderer> () == null) {
			newObject.AddComponent<SpriteRenderer>();
		}

		SpriteRenderer r = newObject.GetComponent<SpriteRenderer> ();
		Sprite s = sprites [Random.Range (0, sprites.Count)];
		r.sprite = s;
		children.Add (newObject);
	}

  public virtual void SpawnGrid(){

		int index = 0;

		for (int i = 0; i < width; i++) {
			for (int j = 0; j < height; j++) {

				Vector3 newObjectPos = transform.position;
				newObjectPos.x += (i * xDist) - (width/2 * xDist);
				newObjectPos.z += (j * zDist);
				newObjectPos.y += (j * yDist);

				GameObject newObject = (GameObject)Instantiate (spritePrefab, newObjectPos, Quaternion.identity);
				newObject.transform.rotation = Quaternion.Euler (rotation);

				newObject.transform.parent = transform;
				if (newObject.GetComponent<SpriteRenderer> () == null) {
					newObject.gameObject.AddComponent<SpriteRenderer>();
				}

				SpriteRenderer r = newObject.GetComponent<SpriteRenderer> ();
				r.sprite = sprites [Random.Range(0, sprites.Count)];
				r.sortingOrder = index;
				newObject.transform.position += (transform.up * r.bounds.size.y)/2;
				children.Add (newObject);
//				grid [i, j].transform.localScale /=  r.sprite.bounds.size.y;
				index++;
			}
		}
	}
}
