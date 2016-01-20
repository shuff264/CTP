﻿using UnityEngine;
using System.Collections;

public class RoadPlacement : MonoBehaviour {

	public int tileX;
	public int tileY;

	public TileMap map;

	void OnMouseOver(){
		//0 = LEFT CLICK = ROAD
		//1 = RIGHT CLICK = GRASS
		if(Input.GetMouseButton(0)){
			map.PlaceRoad (tileX, tileY);
		}
		else if(Input.GetMouseButton(1)){
			map.PlaceGrass(tileX, tileY);
		}
	}
}
