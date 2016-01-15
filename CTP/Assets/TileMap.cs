using UnityEngine;
using System.Collections;

public class TileMap : MonoBehaviour {

	public TileType[] tileTypes;

	public int[,] tiles;
	public GameObject[,] tilesGrid;

	public int mapSizeX = 25;
	public int mapSizeY = 25;

	void Start() {

		//Creating a 2D array for the map
		tiles = new int[mapSizeX, mapSizeY];
		tilesGrid = new GameObject[mapSizeX, mapSizeY];
		
		//Setting the tile type of all tiles
		//0 = GRASS
		//1 = ROAD
		for(int x = 0; x < mapSizeX; x++){
			for(int y = 0; y < mapSizeY; y++){
				tiles[x,y] = 0;
			}
		}

		//Create the map
		for (int x = 0; x < mapSizeX; x++) {
			for (int y = 0; y < mapSizeY; y++) {
				TileType tt = tileTypes[tiles[x,y]];

				tilesGrid[x,y] = (GameObject) Instantiate(tt.tilePrefab, new Vector3(x, 0, y), Quaternion.identity);
				//Setting variables for the road placement script
				RoadPlacement rp = tilesGrid[x,y].GetComponent<RoadPlacement>();
				rp.tileX = x;
				rp.tileY = y;
				rp.map = this;

				
			}
		}

	}

	public void PlaceRoad(int x, int y){
		tiles [x, y] = 1;
		TileType tt = tileTypes[tiles[x,y]];
		Destroy (tilesGrid [x, y]);
		tilesGrid [x, y] = (GameObject)Instantiate (tt.tilePrefab, new Vector3 (x, 0, y), Quaternion.identity);

		RoadPlacement rp = tilesGrid[x,y].GetComponent<RoadPlacement>();
		rp.tileX = x;
		rp.tileY = y;
		rp.map = this;

		TileData td = tilesGrid[x,y].GetComponent<TileData>();
		td.tileX = x;
		td.tileY = y;
		td.map = this;
	}
	
	public void PlaceGrass(int x, int y){
		
		tiles [x, y] = 0;
		TileType tt = tileTypes[tiles[x,y]];
		Destroy (tilesGrid [x, y]);
		tilesGrid [x, y] = (GameObject)Instantiate (tt.tilePrefab, new Vector3 (x, 0, y), Quaternion.identity);

		RoadPlacement rp = tilesGrid[x,y].GetComponent<RoadPlacement>();
		rp.tileX = x;
		rp.tileY = y;
		rp.map = this;

		TileData td = tilesGrid[x,y].GetComponent<TileData>();
		td.tileX = x;
		td.tileY = y;
		td.map = this;
	}

	public GameObject TileReturn(int x, int y){

		return tilesGrid[x,y];

	}

	void Update(){

		for (int x = 0; x < mapSizeX; x++) {
			for (int y = 0; y < mapSizeY; y++) {
				TileType tt = tileTypes[tiles[x,y]];
				
				//Setting variables for the tile data script
				TileData td = tilesGrid[x,y].GetComponent<TileData>();
				td.tileX = x;
				td.tileY = y;
				if(y < mapSizeY - 1){
					td.tileNorthType =  tiles[x,y+1];
				}
				if(y > 0){
					td.tileSouthType = tiles[x,y-1];
				}
				if(x < mapSizeX - 1){
					td.tileEastType = tiles[x+1,y];
				}
				if(x > 0){
					td.tileWestType = tiles[x-1,y];
				}
				td.map = this;
				
			}
			
		}

		//SWITCHES ALL ROADS AND GRASS AROUND
		//JUST HERE TO TEST SOME STUFF
		//PROVES THAT I CAN CHECK THE TILE TYPE AND MAKE A DECISION BASED ON IT
		if(Input.GetKeyDown("return")){

			for (int x = 0; x < mapSizeX; x++) {
				for (int y = 0; y < mapSizeY; y++) {

					if(tiles[x,y] == 0){
						tiles [x, y] = 1;
						TileType tt = tileTypes[tiles[x,y]];
						Destroy (tilesGrid [x, y]);
						tilesGrid [x, y] = (GameObject)Instantiate (tt.tilePrefab, new Vector3 (x, 0, y), Quaternion.identity);
						
						RoadPlacement rp = tilesGrid[x,y].GetComponent<RoadPlacement>();
						rp.tileX = x;
						rp.tileY = y;
						rp.map = this;

						TileData td = tilesGrid[x,y].GetComponent<TileData>();
						td.tileX = x;
						td.tileY = y;
						td.map = this;
					}
					else if(tiles[x,y] == 1){
						tiles [x, y] = 0;
						TileType tt = tileTypes[tiles[x,y]];
						Destroy (tilesGrid [x, y]);
						tilesGrid [x, y] = (GameObject)Instantiate (tt.tilePrefab, new Vector3 (x, 0, y), Quaternion.identity);
						
						RoadPlacement rp = tilesGrid[x,y].GetComponent<RoadPlacement>();
						rp.tileX = x;
						rp.tileY = y;
						rp.map = this;

						TileData td = tilesGrid[x,y].GetComponent<TileData>();
						td.tileX = x;
						td.tileY = y;
						td.map = this;
					}						
					
				}
			}

		}

	}

}