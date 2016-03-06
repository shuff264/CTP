using UnityEngine;
using System.Collections;

public class RoadPlacement : MonoBehaviour {

	public int tileX;
	public int tileY;

	public TileMap map;

	void OnMouseOver(){
		if(Input.GetMouseButton(0)){
			if(UIController.Instance.placeType == 0){
				map.PlaceGrass (tileX, tileY);
			}else if(UIController.Instance.placeType == 1){
				map.PlaceRoad(tileX, tileY);
			}else if(UIController.Instance.placeType == 2){
				//map.PlaceRoad(tileX, tileY);
				Debug.Log("Lights not set up yet");
			}else{
				Debug.Log("ERROR: VALUE NOT RECOGNISED");
			}
		}
	}
}
