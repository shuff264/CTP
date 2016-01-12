using UnityEngine;
using System.Collections;

public class TileData : MonoBehaviour {

	// Needs info of the four tiles it is next to
	// This can then be used to create a map
	// For a search algorithm to use
	// Might just need the number, might need the actual game object?
	// Will have to know about the tile map info

	//	if i know the current tiles xy
	//	and i know the tile aboves type
	//	if the tile above is the right type
	//	and if the tile above is the best option
	//	then i just need to change the current tile x up 1
	//	to get the new tile data
	//	the tile just needs to know the type above it
	//	the location is known because its x +1 x-1 y+1 y-1

	public int tileX;
	public int tileY;
	public int tileNorthType;
	public int tileSouthType;
	public int tileEastType;
	public int tileWestType;
	
	public TileMap map;


//	void Update(){
//
//		if(tileX == 1 && tileY == 1){
//
//			Debug.Log(tileNorthType);
//
//		}
//	
//	
//	}

}
