using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : SimpleManager.Manager<Level> {

	public bool usePerlin;
	private float xOffset, yOffset;
	private Texture2D[] maps;
	public int width, height;
	private int index;
	public Level curLevel;

	private static LevelManager instance = null;

	public static LevelManager Instance {
		get { 
			return instance;
		}
	}

	void Awake(){
		if (instance != null && instance != this) {
			Destroy(this.gameObject);
		} else {
			instance = this;
		}
			
		maps = Resources.LoadAll<Texture2D> ("maps") as Texture2D[];
		Create ();
	}

	void Start(){
	}
		
	public override Level Create(){

//		if (curLevel != null) {
//			Destroy (curLevel.gameObject);
//		}
		GameObject newLevel = new GameObject();
		Level l = newLevel.AddComponent <Level> ();
		LevelTileManager m = newLevel.AddComponent<LevelTileManager> ();

		newLevel.name = "Level" + ManagedObjects.Count;

		l.usePerlin = usePerlin;
		l.xOrigin = Random.Range (0, 10000);
		l.yOrigin = Random.Range (0, 10000);
		l._width = width;
		l._height = height;

		// if you want chunks to connect use this
//		l.xOrigin = xOffset*l.stepSize;
//		l.yOrigin = yOffset;

		if (ManagedObjects.Count == 5) {
			Camera.main.gameObject.AddComponent<CameraDolly> ();
		}

//		Writer.Instance.CreateWord (PrefabManager.Instance.PLAYER.transform.position).transform.localScale *= 3;

		newLevel.transform.position += (Vector3.up) * yOffset;
		l.OnCreated ();

//		Level.transform.position += (Vector3.up) * yOffset;
//		PrefabManager.Instance.PLAYER.transform.position += ((Vector3.right) * l._width/2) + ((Vector3.up) * l._height/2);

		xOffset += l._width;
		yOffset += l._height;

		index++;
//		Level.noiseScale += 0.025f;

//		Level.transform.position -= (Vector3.right) * xOffset/2;


		ManagedObjects.Add (l);
		curLevel = l;
		return l;
	}

	public override void Destroy(Level l){
		ManagedObjects.Remove (l);
		Destroy (l);
	}
}
