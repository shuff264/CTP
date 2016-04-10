using UnityEngine;
using System.Collections;

public class RoadPlacement : MonoBehaviour {

	//Script to help handle the placement of tiles in the world

	public int tileX;
	public int tileY;

	public TileMap map;

	//On mouse over and get button down used to allow for dragging of placement
	void OnMouseOver(){
		if(Input.GetButton("Fire1")){ 
			map.PlaceTile(tileX, tileY, UIController.instance.placeType);
		}
	}
}
