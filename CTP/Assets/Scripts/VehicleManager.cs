using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VehicleManager : MonoBehaviour {

	Vector3 startPosition; //Where the vehicle starts
	Vector3 currentPosition; //The intermidiary positon it is currently moving to
	public Vector3 endPosition; //The end goal
	float speed = 1.0f;
		
	public int northType;
	public int eastType;
	public int southType;
	public int westType;

	public RoadFinder roadFinder;

	public TileMap map;

	public GameObject currentTile;

	List<int> tileOptions;
	


	Vector2[] vecArray;
	// Use this for initialization
	void Start () {
		
		roadFinder = GameObject.Find ("Controller").GetComponent<RoadFinder> ();
		map = GameObject.Find ("Map").GetComponent<TileMap> ();
		
		
		int randX = Random.Range (0, roadFinder.roadPieces.Length);
		int randY = Random.Range (0, roadFinder.roadPieces.Length);
	
		startPosition = gameObject.transform.position;
		endPosition = roadFinder.roadPieces[randX].transform.position;
		endPosition.y++;

		currentTile = map.TileReturn((int)gameObject.transform.position.x, (int)gameObject.transform.position.z);
		currentPosition = currentTile.transform.position;
		currentPosition.y++;


	}



	// Update is called once per frame
	void Update () {

		Dictionary<int, GameObject> tileDic = new Dictionary<int, GameObject>(); 
		List<int> tileOptions = new List<int>(){0,1,2,3};

		tileDic.Add(0, map.TileReturn((int)currentTile.transform.position.x, (int)currentTile.transform.position.z+1));
		tileDic.Add(1, map.TileReturn((int)currentTile.transform.position.x+1, (int)currentTile.transform.position.z));
		tileDic.Add(2, map.TileReturn((int)currentTile.transform.position.x, (int)currentTile.transform.position.z-1));
		tileDic.Add(3, map.TileReturn((int)currentTile.transform.position.x-1, (int)currentTile.transform.position.z));

		if(gameObject.transform.position == currentPosition){
			Debug.Log("ITS THE SAME");
			//Gets the correct tiledata for the tile
			TileData td = map.tilesGrid[(int)gameObject.transform.position.x, (int)gameObject.transform.position.z].GetComponent<TileData>();


			//vecArray = new Vector2[4];


			
			//Getting confused because it thinks the grass tiles are the best option
			//because they are 0,0

//			northType = td.tileNorthType;
//			eastType = td.tileEastType;
//			southType = td.tileSouthType;
//			westType = td.tileWestType;
//
//
//			if(td.tileNorthType == 1){
//				GameObject northTempTile = map.TileReturn((int)currentTile.transform.position.x, (int)currentTile.transform.position.z+1);
//				Vector3 northTempPosition = northTempTile.transform.position;
//
//				vecArray[0] = new Vector2(northTempTile.transform.position.x,northTempTile.transform.position.z);
//
//			}
//
//			if(td.tileEastType == 1){
//				GameObject eastTempTile = map.TileReturn((int)currentTile.transform.position.x+1, (int)currentTile.transform.position.z);
//				Vector3 eastTempPosition = eastTempTile.transform.position;
//				
//				vecArray[1] = new Vector2(eastTempTile.transform.position.x,eastTempTile.transform.position.z);
//				
//			}
//
//			if(td.tileSouthType == 1){
//				GameObject southTempTile = map.TileReturn((int)currentTile.transform.position.x, (int)currentTile.transform.position.z-1);
//				Vector3 southTempPosition = southTempTile.transform.position;
//				
//				vecArray[2] = new Vector2(southTempTile.transform.position.x,southTempTile.transform.position.z);
//				
//			}
//
//			if(td.tileWestType == 1){
//				GameObject westTempTile = map.TileReturn((int)currentTile.transform.position.x-1, (int)currentTile.transform.position.z);
//				Vector3 westTempPosition = westTempTile.transform.position;
//				
//				vecArray[3] = new Vector2(westTempTile.transform.position.x,westTempTile.transform.position.z);
//				
//			}

//			for(int i = 0; i < vecArray.Length; i++){
//
//				Debug.Log(vecArray[i]);
//
//			}

		Nearest:
			Vector2 searchValue = new Vector2(endPosition.x, endPosition.z); //Value to be found in list
			Vector2 currentNearest = new Vector2(tileDic[tileOptions[0]].transform.position.x, tileDic[tileOptions[0]].transform.position.z); //The current nearest value reflected as a position in list
			int dicNearest = 0;

			float currentDifferenceX = Mathf.Abs(currentNearest.x - searchValue.x);
			float currentDifferenceY = Mathf.Abs(currentNearest.y - searchValue.y);
			Vector2 currentDifference = new Vector2(currentDifferenceX, currentDifferenceY);
			//int currentDifference = Mathf.Abs(currentNearest - searchValue); //Works out the current different between the closest and the value
			
			for (int i = 0; i < 4; i++)
			{
				//int diff = Mathf.Abs(vecArray[i] - searchValue); //value from array - search amount to get the difference
				if(tileOptions.Contains(i)){
					float diffX = Mathf.Abs(tileDic[i].transform.position.x - searchValue.x);
					float diffY = Mathf.Abs(tileDic[i].transform.position.y - searchValue.y);

					Vector2 diff = new Vector2(diffX, diffY);

					if (diff.x < currentDifference.x){ //if the difference is less than current diffrence - if its closer than current closests
						if(diff.y < currentDifference.y){
							currentDifference = diff; //change current difference to this difference
							currentNearest = new Vector2(tileDic[i].transform.position.x, tileDic[i].transform.position.z); //change current nearest to this value
							dicNearest = i;
							
						}
					}
				}
			}

			if(map.TileReturnInt((int)currentNearest.x, (int)currentNearest.y) == 1){
				currentPosition = new Vector3(currentNearest.x, 1, currentNearest.y);
				gameObject.transform.position = currentPosition;
				currentTile = map.TileReturn((int)currentNearest.x, (int)currentNearest.y);
			}
			else{ 
				tileOptions.Remove(dicNearest);
				goto Nearest;
			}
//
////			Debug.Log(currentNearest);
//
//			currentPosition = new Vector3(currentNearest.x, 1, currentNearest.y);
//
//

		}
		else{Debug.Log("Fail");}

	
		tileDic.Clear();

		if (gameObject.transform.position == endPosition) {

			Debug.Log("DESTROY");
			DestroyObject(gameObject);
			
		}
	

	}
}

