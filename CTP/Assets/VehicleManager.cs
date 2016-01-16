using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VehicleManager : MonoBehaviour {

	Vector3 startPosition; //Where the vehicle starts
	Vector3 currentPosition; //The intermidiary positon it is currently moving to
	public Vector3 endPosition; //The end goal
	float speed = 1.0f;
		
	public RoadFinder roadFinder;

	public TileMap map;

	public GameObject currentTile;
	
	// Use this for initialization
	void Start () {
		
		roadFinder = GameObject.Find ("Controller").GetComponent<RoadFinder> ();
		map = GameObject.Find ("Map").GetComponent<TileMap> ();
		
		
		int randX = Random.Range (0, map.mapSizeX - 1);
		int randY = Random.Range (0, map.mapSizeY - 1);
	
		startPosition = gameObject.transform.position;
		endPosition = roadFinder.roadPieces[randX].transform.position;
		endPosition.y++;

		currentTile = map.TileReturn((int)gameObject.transform.position.x, (int)gameObject.transform.position.z);
		currentPosition = currentTile.transform.position;
		currentPosition.y++;
		
	}



	// Update is called once per frame
	void Update () {

//		List<int> list = new List<int> { 2, 5, 7, 10 };
//		int number = 9;
//		for(int i =0; i<list.Count;i++){
//			Debug.Log(list[i]);
//		}
//		Debug.Log("ENDLIST");
		// find closest to number

//		list.Sort(delegate(int a, int b) {
//			Debug.Log(Mathf.Abs(a-number.CompareTo(b-number)));
//			return (Mathf.Abs(a-number.CompareTo(b-number)));
//		});
//		for(int i =0; i<list.Count;i++){
//			Debug.Log(list[i]);
//		}

		//TEST COMPARATOR	
		int[] w = { 1000, 2000, 3000, 4000, 5000 }; //List to choose from
		
		int searchValue = 6000; //Value to be found in list
		int currentNearest = w[0]; //The current nearest value reflected as a position in list
		int currentDifference = Mathf.Abs(currentNearest - searchValue); //Works out the current different between the closest and the value
		
		for (int i = 1; i < w.Length; i++)
		{
			int diff = Mathf.Abs(w[i] - searchValue); //value from array - search amount to get the difference
			if (diff < currentDifference) //if the difference is less than current diffrence - if its closer than current closests
			{
				currentDifference = diff; //change current difference to this difference
				currentNearest = w[i]; //change current nearest to this value
			}
		}

		Debug.Log(currentNearest);
			
			//Check four options on tile
		//choose best
		//set it as currentPosition
		//Repeat till at goal

		if(gameObject.transform.position == currentPosition){

			//Gets the correct tiledata for the tile
			TileData td = map.tilesGrid[(int)gameObject.transform.position.x, (int)gameObject.transform.position.z].GetComponent<TileData>();

			Debug.Log(td.tileNorthType);

			if(td.tileNorthType == 1){
				currentTile = map.TileReturn((int)currentTile.transform.position.x, (int)currentTile.transform.position.z+1);
				currentPosition = currentTile.transform.position;
				currentPosition.y++;
			}

			if(td.tileEastType == 1){
				currentTile = map.TileReturn((int)currentTile.transform.position.x+1, (int)currentTile.transform.position.z);
				currentPosition = currentTile.transform.position;
				currentPosition.y++;
			}

			if(td.tileSouthType == 1){
				currentTile = map.TileReturn((int)currentTile.transform.position.x, (int)currentTile.transform.position.z-1);
				currentPosition = currentTile.transform.position;
				currentPosition.y++;
			}

			if(td.tileWestType == 1){
				currentTile = map.TileReturn((int)currentTile.transform.position.x-1, (int)currentTile.transform.position.z);
				currentPosition = currentTile.transform.position;
				currentPosition.y++;
			}
		}
	

		//Debug.Log(currentPosition);

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

