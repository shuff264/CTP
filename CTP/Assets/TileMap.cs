﻿using UnityEngine;
using System.Collections;

public class TileMap : MonoBehaviour {

	public TileType[] tileTypes;

	int[,] tiles;
	GameObject[,] tilesGrid;

	int mapSizeX = 10;
	int mapSizeY = 10;

	void Start() {

		tiles = new int[mapSizeX, mapSizeY];
		tilesGrid = new GameObject[mapSizeX, mapSizeY];
		

		for(int x = 0; x < mapSizeX; x++){
			for(int y = 0; y < mapSizeY; y++){
				tiles[x,y] = 0;
			}
		}

		for (int x = 0; x < mapSizeX; x++) {
			for (int y = 0; y < mapSizeY; y++) {
				TileType tt = tileTypes[tiles[x,y]];

				tilesGrid[x,y] = (GameObject) Instantiate(tt.tilePrefab, new Vector3(x, 0, y), Quaternion.identity);

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

	}
	
	public void PlaceGrass(int x, int y){
		
		tiles [x, y] = 0;
		TileType tt = tileTypes[tiles[x,y]];
		Destroy (tilesGrid [x, y]);
		tilesGrid [x, y] = (GameObject)Instantiate (tt.tilePrefab, new Vector3 (x, 0, y), Quaternion.identity);

		
	}

}