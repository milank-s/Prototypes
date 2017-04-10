	using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour, SimpleManager.IManaged {

	public bool usePerlin;
	public static float noiseScale = 0.1f;
	public int minSize, maxSize;
	public float spriteSize = 3f;
	public int _width, _height;
	public float xOrigin, yOrigin;
	public string directory; 

	public Texture2D bitmap;
	public GameObject[,] _tiles;


	private bool playerPlaced;
	private Texture2D _bitmap;
	private List<GameObject> toDestroy;
	public Dictionary<Vector2, GameObject> mapIndices;

	public void OnCreated(){

		_bitmap = new Texture2D (_width, _height);
		_tiles = new GameObject[_width, _height];
		mapIndices = new Dictionary<Vector2, GameObject>();
		toDestroy = new List<GameObject> ();
		GenerateChunk (transform.position);
//		OutputBitmap ();
	}

	public void wipeMap(){
				foreach (GameObject g in mapIndices.Values) {
					toDestroy.Add (g);
				}
				foreach (GameObject g in toDestroy) {
					mapIndices.Remove ((Vector2)g.transform.position);
					Destroy (g);
				}
				toDestroy.Clear ();
	}

	public void GeneratePieceWise(int width, int height){
		Vector2 playerPos = (Vector2)PrefabManager.Instance.PLAYER.transform.position;

		float xCoord = (xOrigin + playerPos.x) * noiseScale;
		float yCoord = (yOrigin + playerPos.y) * noiseScale;

		for (int y = -height; y < height + 1; y++) {

			xCoord = (xOrigin + playerPos.x) * noiseScale;

			int yPos = (int)playerPos.y + y;

			for (int x = -width; x < width + 1; x++) {

				int xPos = (int)playerPos.x + x;

				float luminosity = 0;

				if (usePerlin) {
					luminosity = OctavePerlin (xCoord + (x*noiseScale), yCoord + (y * noiseScale), 3, 0.5f);
				} else {
					Color c = _bitmap.GetPixel (xPos, yPos);
					luminosity = ((0.21f * (float)c.r) + (0.72f * (float)c.g) + (0.07f * (float)c.b));
				}
				GameObject newTile;

				if (!mapIndices.TryGetValue (new Vector2 (xPos, yPos), out newTile)) {
					if (Vector2.Distance (new Vector2 (xPos, yPos), playerPos) <= width) {
						newTile = CreateTile (xPos, yPos, luminosity);
						mapIndices.Add (new Vector2 (xPos, yPos), newTile);
					}
				} else {
					mapIndices[new Vector2 (xPos, yPos)].GetComponent<TextMesh>().color += new Color(0.25f, 0.25f, 0.25f);
				}
			}
		}
	}

	public GameObject ObjectFactory(int x, Vector2 pos){

		GameObject newObject = null;

		Sprite s = null;
		bool passable;

//		if (_tiles [(int)pos.x, (int)pos.y] != null) {
//			newObject = _tiles [(int)pos.x, (int)pos.y];
//			newObject.SetActive (true);
//		} else {
			newObject = Instantiate (PrefabManager.Instance.TILE, pos, Quaternion.identity) as GameObject;
//		}

		switch (x) {

		case 2:
			newObject.GetComponent<TextMesh> ().text = "%";
			newObject.GetComponent<Tile> ().isPassable = true;
			newObject.tag = "seed";
			newObject.GetComponent<TextMesh> ().color = Color.green;
			break;

		case 3:
			newObject.GetComponent<TextMesh> ().text = "!";

			newObject.GetComponent<Tile> ().isPassable = true;
			break;

//		case 5:
//			newObject.GetComponent<TextMesh> ().text = ".";
//			newObject.GetComponent<Tile> ().isPassable = true;
//			break;
//
//		case 6:
//			newObject.GetComponent<TextMesh> ().text = "_";
//			newObject.GetComponent<Tile> ().isPassable = true;
//			break;
//
//		case 7:
//			newObject.GetComponent<TextMesh> ().text = ",";
//			newObject.GetComponent<Tile> ().isPassable = true;
//			break;

		case 6:
			newObject.GetComponent<TextMesh> ().text = "^";
			newObject.GetComponent<Tile> ().isPassable = false;
			break;

		case 7:
			newObject.GetComponent<TextMesh> ().text = "†";
			newObject.GetComponent<Tile> ().isPassable = false;
			break;

		case 8:
			newObject.GetComponent<TextMesh> ().text = "T";
			newObject.GetComponent<Tile> ().isPassable = false;
			break;

		default:	
			newObject.SetActive (false);
			newObject.GetComponent<TextMesh> ().text = ".";
			newObject.GetComponent<Tile> ().isPassable = true;
			newObject.GetComponent<TextMesh> ().color = new Color(0.2f, 0.2f, 0.2f);
			break;
		}
		return newObject;
	}


	GameObject CreateTile(int x, int y, float perlinVal){
		

		int objectType = Mathf.CeilToInt (perlinVal * 10);

		GameObject tile = ObjectFactory (objectType, new Vector2 (x, y));

		if (tile != null) {
			if (tile.GetComponent<Tile> ().isPassable && x > _width / 2 - 1 && y > _height / 2 - 1 && !playerPlaced && x < _width / 2 + 2 && y < _height / 2 + 2) {
				playerPlaced = true;
				PrefabManager.Instance.PLAYER.transform.position = new Vector2 (x, y) + (Vector2)transform.position;
			}

			tile.transform.position += transform.position;

			if (tile.GetComponent<SpriteRenderer> () != null) {
//				tile.transform.localScale /= tile.GetComponent<SpriteRenderer> ().sprite.bounds.size.y/3;
				tile.transform.position += Vector3.up * tile.GetComponent<SpriteRenderer> ().bounds.extents.y;
				tile.GetComponent<SpriteRenderer> ().sortingOrder = -y;
			}

//			tile.transform.localScale *= (float)objectType / 10;
			BoxCollider2D col = tile.GetComponent<BoxCollider2D> ();
			col.size = new Vector2 (1, 1) / tile.transform.localScale.x;
			col = col;
			tile.transform.parent = transform;
			tile.name = "Tile " + x + ", " + y;
		} 
		return tile;
	}

	public void GenerateChunk(Vector3 position){

		float xCoord = xOrigin + position.x;
		float yCoord = yOrigin + position.y;

		for (int y = 0; y < _height; y++, yCoord++) {

			xCoord = xOrigin + position.x;

			for (int x = 0; x < _width; x++, xCoord++){


				float luminosity = 0;

				if (usePerlin) {
					luminosity = OctavePerlin (xCoord * noiseScale, yCoord * noiseScale, 3, 0.75f);
					_bitmap.SetPixel (x, y, new Color (luminosity, luminosity, luminosity));
				} else {
					Color c = _bitmap.GetPixel (x, y);
					luminosity = ((0.21f * (float)c.r) + (0.72f * (float)c.g) + (0.07f * (float)c.b));
				}

				GameObject newTile;
				if(!mapIndices.TryGetValue(new Vector2(x, y), out newTile)){
					if (Vector2.Distance (new Vector2 (x, y), (Vector2)position) <= _width) {
						newTile = CreateTile (x, y, luminosity);
						mapIndices.Add (new Vector2 (x, y), newTile);
					}
				}
			}
		}
	}

	public bool IsTilePassable(Vector2 pos){
		if (IsTileInArray (pos)) {
			if (GetTile (pos) == null) {
				return false;
			}
			if (GetTile (pos).GetComponent<Tile> ().isPassable) {
				return true;
			} else {
				return false;
			}
		}else{
			return false;
		}
	}


	public Vector2 IsMoveOpen(Vector2 pos){
		if(IsTileInArray(pos)){
			if (!GetTile (pos)) {
				return (Vector2)PrefabManager.Instance.PLAYER.transform.position;
			}
			if (GetTile (pos).GetComponent<Tile> ().isPassable) {
				return pos  + (Vector2)transform.position;
			} else {
				return (Vector2)PrefabManager.Instance.PLAYER.transform.position;
			}
		}else{
			if (pos.x >= _width && IsTilePassable (new Vector2 (0, pos.y))) {
				return new Vector2 (0, pos.y)  + (Vector2)transform.position;
			} else if (pos.x < 0 && IsTilePassable (new Vector2 (_width -1, pos.y))) {
				return new Vector2 (_width-1, pos.y) + (Vector2)transform.position;
			} else if (pos.y < 0 && IsTilePassable (new Vector2 (pos.x, _height -1))) {
				return new Vector2 (pos.x, _height-1) + (Vector2)transform.position;
			} else if (pos.y >= _height && IsTilePassable (new Vector2 (pos.x, 0))) {
				return new Vector2 (pos.x, 0) + (Vector2)transform.position;
			} else {
				return (Vector2)PrefabManager.Instance.PLAYER.transform.position;
			}
		}
	}

	public bool IsTileInArray(Vector2 pos){
		if (pos.x >= _width || pos.y >= _height || pos.x < 0 || pos.y < 0) {
			return false;
		} else {
			return true;
		}
	}

	public GameObject GetTile(Vector2 pos){
		if (!IsTileInArray(pos)) {
			return null;
		}else{ 
			return _tiles [(int)pos.x, (int)pos.y];
		}
	}

	float OctavePerlin(float x, float y, int octaves, float persistence) {
		float total = 0;
		float frequency = 1;
		float amplitude = 1;
		float maxValue = 0;  // Used for normalizing result to 0.0 - 1.0

		for(int i=0;i<octaves;i++) {
			total += Mathf.PerlinNoise(x * frequency, y * frequency) * amplitude;

			maxValue += amplitude;

			amplitude *= persistence;
			frequency *= 2;
		}

		return total/maxValue;
	}


	public void OutputBitmap(){
		GameObject floor = Instantiate (PrefabManager.Instance.SPRITE, Vector3.zero, Quaternion.identity) as GameObject;
		_bitmap.Apply ();
		_bitmap.filterMode = FilterMode.Point;
		floor.transform.eulerAngles = new Vector3 (0, 0, 0);
		floor.GetComponent<SpriteRenderer> ().sprite = Sprite.Create(_bitmap, new Rect(0,0, _width, _height), new Vector2(0.5f, 0.5f), _width);
		floor.GetComponent<SpriteRenderer> ().sortingOrder = -100;
		floor.transform.localScale *= _width;
//		floor.transform.localScale += ((Vector3.up) * _width);
		floor.transform.position = new Vector3 (_width/2 , _height/2 -1, 0) + transform.position;
		floor.transform.parent = transform;
		floor.name = "ground";
		floor.layer = 8;
	}

	public void OnDestroyed(){
		
	}
}

