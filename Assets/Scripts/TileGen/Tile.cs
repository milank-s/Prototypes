using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour, SimpleManager.IManaged {

	public bool isPassable = false;
	public int tileType;

	public void OnCreated(){
		
	}

	public void OnDestroyed(){

	}		
}
