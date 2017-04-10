using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabManager : MonoBehaviour {

	private static PrefabManager instance = null;

	public static PrefabManager Instance {
		get { 
			return instance;
		}
	}
	public GameObject PLAYERPREFAB;
	public GameObject PLAYER;
	public GameObject TEXT;
	public GameObject NPC;
	public GameObject TILE;
	public GameObject PLANT;
	public GameObject SPRITE;
	public GameObject SEED;
	public Sprite[]  SPRITES;

	void Awake () {
		if (instance != null && instance != this) {
			Destroy(this.gameObject);
		} else {
			instance = this;
		}
//		SPRITES = Resources.LoadAll<Sprite> ("");
//		DontDestroyOnLoad (this);

//		Instance.PLAYER = Instantiate(Instance.PLAYERPREFAB, Vector3.zero, Quaternion.identity) as GameObject;
	}

}
