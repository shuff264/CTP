﻿using UnityEngine;
using System.Collections;

public class RoadPlacement : MonoBehaviour {

	public int tileX;
	public int tileY;
	public TileMap map;

	void OnMouseOver(){

		if(Input.GetMouseButton(0)){
			map.PlaceRoad (tileX, tileY);
		}
		else if(Input.GetMouseButton(1)){
			map.PlaceGrass(tileX, tileY);
		}
	}
}
