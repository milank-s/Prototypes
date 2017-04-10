using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTileManager : SimpleManager.Manager<Tile> {
	
	protected Sprite[] terrain;
	protected Sprite[] flowers;
	protected Sprite[] stems;
	protected Sprite[] seeds;
	protected Sprite[] npcs;
	protected Sprite[] maps;
	protected Sprite[] abstracts;

	void Awake(){
		terrain = Resources.LoadAll<Sprite> ("plants");
		flowers = Resources.LoadAll<Sprite> ("flowers");
		stems = Resources.LoadAll<Sprite> ("stems");
		seeds = Resources.LoadAll<Sprite> ("seeds");
		maps = Resources.LoadAll<Sprite> ("ground");
		abstracts = Resources.LoadAll<Sprite> ("drawings");
	}

	public override Tile Create(){
		return null;
	}

	void Update(){
		ClearDestroyedObjects ();
	}

	public override void Destroy(Tile g){
		ObjectsAwaitingDestruction.Add (g);
	}

	public GameObject ObjectFactory(int x, Vector2 pos){

		GameObject newObject = null;

		Sprite s = null;
		bool passable;

		switch (x) {
	
		case 3:
			newObject = Instantiate (PrefabManager.Instance.SEED, pos, Quaternion.identity) as GameObject;
			s = seeds [Random.Range (0, seeds.Length)];
			newObject.GetComponent<SpriteRenderer> ().sprite = s;
			newObject.transform.localScale /= newObject.GetComponent<SpriteRenderer> ().sprite.bounds.size.y/2;
			ManagedObjects.Add (newObject.GetComponent<Tile>());
			break;

		case 4:	
			newObject = Instantiate (PrefabManager.Instance.TEXT, pos, Quaternion.identity) as GameObject;
			newObject.GetComponent<TextMesh> ().text = "-";
//			newObject = Instantiate (PrefabManager.Instance.TEXT, pos, Quaternion.identity) as GameObject;
//			newObject.GetComponent<TextMesh> ().text = "=";	
			break;

		case 5:
			newObject = Instantiate (PrefabManager.Instance.TEXT, pos, Quaternion.identity) as GameObject;
			newObject.GetComponent<TextMesh> ().text = "+";
			break;

		case 6:
			newObject = Instantiate (PrefabManager.Instance.TEXT, pos, Quaternion.identity) as GameObject;
			newObject.GetComponent<TextMesh> ().text = "†";
			break;

		case 7:
			return null;
//			newObject.GetComponent<BoxCollider> ().isTrigger = false;
			break;

		case 8:
//			newObject = Instantiate (PrefabManager.Instance.TILE, pos, Quaternion.identity) as GameObject;
//			s = terrain[Random.Range (0, terrain.Length)];
//			newObject.GetComponent<SpriteRenderer> ().sprite = s;
			return null;
//			newObject.GetComponent<BoxCollider> ().isTrigger = false;
			break;

		case 9:
			newObject = Instantiate (PrefabManager.Instance.TILE, pos, Quaternion.identity) as GameObject;
			s = terrain[Random.Range (0, terrain.Length)];
			newObject.GetComponent<SpriteRenderer> ().sprite = s;
			break;

		default:	
			newObject = Instantiate (PrefabManager.Instance.TILE, pos, Quaternion.identity) as GameObject;
			s = abstracts [Random.Range (0, abstracts.Length)];
			newObject.GetComponent<SpriteRenderer> ().sprite = s;

			break;
		}

//		switch (x) {
//
//
//		case 4:
//			newObject = Instantiate (PrefabManager.Instance.SPRITE, pos, Quaternion.identity) as GameObject;
//			s = seeds[Random.Range (0, seeds.Length)];
//			newObject.GetComponent<SpriteRenderer> ().sprite = s;
//			break;
//
//		case 5:
//			newObject = Instantiate (PrefabManager.Instance.SPRITE, pos, Quaternion.identity) as GameObject;
//			s = seeds[Random.Range (0, seeds.Length)];
//			newObject.GetComponent<SpriteRenderer> ().sprite = s;
//			break;
//
//		case 6:
//			newObject = Instantiate (PrefabManager.Instance.SPRITE, pos, Quaternion.identity) as GameObject;
//			s = seeds[Random.Range (0, seeds.Length)];
//			newObject.GetComponent<SpriteRenderer> ().sprite = s;
//			break;
//
//		case 7:
//			newObject = Instantiate (PrefabManager.Instance.TILE, pos, Quaternion.identity) as GameObject;
//			s = stems[Random.Range (0, stems.Length)];
//			newObject.GetComponent<SpriteRenderer> ().sprite = s;
//			break;
//
//		case 8:
//			newObject = Instantiate (PrefabManager.Instance.TILE, pos, Quaternion.identity) as GameObject;
//			s = abstracts[Random.Range (0, abstracts.Length)];
//			newObject.GetComponent<SpriteRenderer> ().sprite = s;
//			break;
//
//		case 9:
//			newObject = Instantiate (PrefabManager.Instance.TILE, pos, Quaternion.identity) as GameObject;
//			s = terrain[Random.Range (0, terrain.Length)];
//			newObject.GetComponent<SpriteRenderer> ().sprite = s;
//			break;
//
//		default:	
//			newObject = Instantiate (PrefabManager.Instance.TILE, pos, Quaternion.identity) as GameObject;
//			s = terrain[Random.Range (0, abstracts.Length)];
//			newObject.GetComponent<SpriteRenderer> ().sprite = s;
//			return null;
//			break;
//		}

		newObject.AddComponent<Tile> ().isPassable = newObject.GetComponent<BoxCollider> ().isTrigger;

		return newObject;
	}

	public int GetManagedObjectCount(){
		return ManagedObjects.Count;
	}
}
