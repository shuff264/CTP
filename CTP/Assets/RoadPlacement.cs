using UnityEngine;
using System.Collections;

public class RoadPlacement : MonoBehaviour {

	public int tileX;
	public int tileY;
	public TileMap map;

	void OnMouseDown(){

		map.PlaceRoad (tileX, tileY);

	}
}
