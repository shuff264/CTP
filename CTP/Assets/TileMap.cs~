using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class TileMap : MonoBehaviour {

	public TileType[] tileTypes;

	public int[,] tiles;
	public GameObject[,] tilesGrid;
	public List<Node> currentPath = null;

	public int mapSizeX = 25;
	public int mapSizeY = 25;

	void Start() {

		GenerateMapData();
		GeneratePathFindingGraph();
		GenerateMapVisual();


	}

	void GenerateMapData(){

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

	}

	public class Node {
		public List<Node> neighbours;
		public int x;
		public int y;

		public Node(){
			neighbours = new List<Node>();
		}

		public float DistanceTo(Node n){
			return Vector2.Distance(new Vector2(x,y), new Vector2(n.x,n.y));
		}
	}

	Node[,] graph;

	void GeneratePathFindingGraph(){

		graph = new Node[mapSizeX, mapSizeY];

		for(int x = 0; x < mapSizeX; x++){
			for(int y = 0; y < mapSizeY; y++){
				graph[x,y] = new Node();

				graph[x,y].x = x;
				graph[x,y].y = y;
			}
		}

		for(int x = 0; x < mapSizeX; x++){
			for(int y = 0; y < mapSizeY; y++){

				if(x > 0){
					graph[x,y].neighbours.Add(graph[x-1,y]);
				}

				if(x < mapSizeX - 1){
					graph[x,y].neighbours.Add(graph[x+1,y]);
				}

				if(y > 0){
					graph[x,y].neighbours.Add(graph[x,y-1]);
				}
				
				if(y < mapSizeY - 1){
					graph[x,y].neighbours.Add(graph[x,y+1]);
				}
			}
		}

	}

	void GenerateMapVisual(){
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

	public List<Node> GeneratePathTo(int startX, int startY, int endX, int endY){
		//selectedUnit.GetComponent<Unit>().currentPath = null;

		Dictionary<Node, float> dist = new Dictionary<Node, float>();
		Dictionary<Node, Node> prev = new Dictionary<Node, Node>();

		List<Node> unvisited = new List<Node>();

		Node source = graph[startX, startY];
		Node target = graph[endX,endY];

		dist[source] = 0;
		prev[source] = null;

		foreach(Node v in graph){
			if(v != source){
				dist[v] = Mathf.Infinity;
				prev[v] = null;
			}
			unvisited.Add(v);
		}

		while(unvisited.Count > 0){
			Node u = null;

			foreach(Node possibleU in unvisited){
				if(u == null || dist[possibleU] < dist[u]){
					u = possibleU;
				}
			}

			if(u == target){
				break;
			}

			unvisited.Remove(u);

			foreach(Node v in u.neighbours){
				float temp = dist[u] + u.DistanceTo(v);
				if(temp < dist[v]){
					dist[v] = temp;
					prev[v] = u;
				}
			}
		}

//		if(prev[target] == null){
//			//No route
//			return;
//		}

		currentPath = new List<Node>();

		Node current = target;

		while(current != null){
			currentPath.Add(current);
			current = prev[current];
		}

		currentPath.Reverse();

		return currentPath;


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


		//Debug.Log(tilesGrid[x,y]);

		return tilesGrid[x,y];

	}

	public int TileReturnInt(int x, int y){
		
		
		//Debug.Log(tilesGrid[x,y]);
		
		return tiles[x,y];
		
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