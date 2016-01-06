using UnityEngine;
using System.Collections;

public class TileMap : MonoBehaviour {

	public TileType[] tileTypes;

	int[,] tiles;

	int mapSizeX = 10;
	int mapSizeY = 10;

	void Start() {

		tiles = new int[mapSizeX, mapSizeY];

		for(int x = 0; x < mapSizeX; x++){
			for(int y = 0; y < mapSizeY; y++){
				tiles[x,y] = 1;
			}
		}

		for (int x = 0; x < mapSizeX; x++) {
			for (int y = 0; y < mapSizeY; y++) {
				TileType tt = tileTypes[tiles[x,y]];

				Instantiate(tt.tilePrefab, new Vector3(x, 0, y), Quaternion.identity);
			}
		}
	}

}