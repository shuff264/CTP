using UnityEngine;
using System.Collections;

public class VehicleManager : MonoBehaviour {

	Vector3 startPosition; //Where the vehicle starts
	Vector3 currentPosition; //The intermidiary positon it is currently moving to
	public Vector3 endPosition; //The end goal
	float speed = 1.0f;
		
	public RoadFinder roadFinder;

	public TileMap map;

	GameObject currentTile;
	
	// Use this for initialization
	void Start () {
		
		roadFinder = GameObject.Find ("Controller").GetComponent<RoadFinder> ();
		map = GameObject.Find ("Map").GetComponent<TileMap> ();
		
		
		int randX = Random.Range (0, map.mapSizeX - 1);
		int randY = Random.Range (0, map.mapSizeY - 1);
	
		startPosition = gameObject.transform.position;
		endPosition = map.TileReturn(randX, randY).transform.position;
		endPosition.y++;

		currentTile = map.TileReturn((int)gameObject.transform.position.x, (int)gameObject.transform.position.y);
		currentPosition = currentTile.transform.position;
		currentPosition.y++;
		
	}
	
	// Update is called once per frame
	void Update () {

		//Check four options on tile
		//choose best
		//set it as currentPosition
		//Repeat till at goal

		if(gameObject.transform.position == currentPosition){
			//Gets the correct tiledata for the tile
			TileData td = map.tilesGrid[(int)gameObject.transform.position.x, (int)gameObject.transform.position.y].GetComponent<TileData>();

			if(td.tileNorthType == 1){
				currentTile = map.TileReturn((int)currentTile.transform.position.x, (int)currentTile.transform.position.z+1);
				currentPosition = currentTile.transform.position;
				currentPosition.y++;
			}

		}



		gameObject.transform.position = currentPosition;



//		td.tileX = x;
//		td.tileY = y;
//		if(y < mapSizeY - 1){
//			td.tileNorthType =  tiles[x,y+1];
//		}
//		if(y > 0){
//			td.tileSouthType = tiles[x,y-1];
//		}
//		if(x < mapSizeX - 1){
//			td.tileEastType = tiles[x+1,y];
//		}
//		if(x > 0){
//			td.tileWestType = tiles[x-1,y];
//		}
//		td.map = this;

	


		if (gameObject.transform.position == endPosition) {
			
			DestroyObject(gameObject);
			
		}
		
	}
}

