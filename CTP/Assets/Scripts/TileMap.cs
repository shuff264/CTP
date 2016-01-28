using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class TileMap : MonoBehaviour {

	public TileType[] tileTypes;

	public int[,] tiles;
	public GameObject[,] tilesGrid;

	public List<Node> currentPath = null;
	public Node[,] graph;

	public int mapSizeX = 25;
	public int mapSizeY = 25;

	void Start() {
		GenerateMapData();
		CreatePathFindingGraph();
		CreateMapVisual();
	}

	void Update(){
		
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

	//Affects how likely a vehicle is to enter a tile
	//Not to important at the minute
	//But may be useful with the addition of more
	//Tile types
	//Maybe turning or traffic lights have a higher
	//Movement cost for the vehicle
	public float CostToEnterTile(int x, int y){
		TileType tt = tileTypes[tiles[x,y]];
		return tt.movementCost;
	}

	//Controls wether the vechicle can go into a tile
	public bool MovementAllowed(int x, int y){
		TileType tt = tileTypes[tiles[x,y]];
		return tt.movementAllowed;
	}

	//Creates the graph that the path finding is based off of
	void CreatePathFindingGraph(){

		graph = new Node[mapSizeX, mapSizeY];

		//Fills the graph with nodes
		for(int x = 0; x < mapSizeX; x++){
			for(int y = 0; y < mapSizeY; y++){
				graph[x,y] = new Node();

				graph[x,y].x = x;
				graph[x,y].y = y;
			}
		}

		//Fills in the nodes neighbours list
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

	void CreateMapVisual(){
		//Create the map
		for (int x = 0; x < mapSizeX; x++) {
			for (int y = 0; y < mapSizeY; y++) {
				TileType tt = tileTypes[tiles[x,y]];

				tilesGrid[x,y] = (GameObject) Instantiate(tt.tilePrefab, new Vector3(x, 0, y), Quaternion.identity);

				//tilesGrid[x,y] = (GameObject) Instantiate(tt.tilePrefab, new Vector3(x, 0, y), Quaternion.identity);
				//Setting variables for the road placement script
				RoadPlacement rp = tilesGrid[x,y].GetComponent<RoadPlacement>();
				rp.tileX = x;
				rp.tileY = y;
				rp.map = this;
			}
		}
	}

	//PATHFINDING CODE BASED OFF PSEUDO CODE FOUND ON WIKIPIDIA
	//https://en.wikipedia.org/wiki/Dijkstra%27s_algorithm

	public List<Node> FindPathToGoal(int startX, int startY, int endX, int endY){

		//Creating dictionaries to hold pathfinding data
		//dist holds the distance between nodes as a float
		//prev holds the node and the node before it
		Dictionary<Node, float> dist = new Dictionary<Node, float>();
		Dictionary<Node, Node> prev = new Dictionary<Node, Node>();

		//List of all nodes left to be visited and evaluated
		List<Node> unvisited = new List<Node>();

		//Setting the starting point and ending point
		Node startPosition = graph[startX, startY];
		Node endPosition = graph[endX,endY];

		//Setting initial values for the starting node
		//It is 0 distance from the start as it is the start
		//As it is the first there are no previous nodes
		dist[startPosition] = 0;
		prev[startPosition] = null;

		//Setting initial values for all other nodes
		//Tentatively setting the distance to infinity
		//Meaning that is impossbile for a node to be chosen
		//Ahead of those more suitable
		//As the paths have not yet been generated the previous
		//node is set to null
		//The nodes are then added to the unvisited list
		foreach(Node v in graph){
			if(v != startPosition){
				dist[v] = Mathf.Infinity;
				prev[v] = null;
			}
			unvisited.Add(v);
		}

		//While there are nodes still left to visit
		while(unvisited.Count > 0){
			Node u = null;

			//Chooses the node with the shortest possible distance
			//Or the source node if that is all that is available
			foreach(Node possibleU in unvisited){
				if(u == null || dist[possibleU] < dist[u]){
					u = possibleU;
				}
			}

			//If its at the end position the function can end
			if(u == endPosition){
				break;
			}

			//Removes the node from the list of possible nodes to be visited
			unvisited.Remove(u);

			//Checks each of the neighbours of the node
			//Checks if the vehicle can move into that neighbour
			//Compares noeighbours based on the distance and the movement cost
			//To choose the best possible option
			foreach(Node v in u.neighbours){
				if(MovementAllowed(v.x, v.y)){
					float temp = dist[u] + u.DistanceTo(v) + CostToEnterTile(v.x, v.y);
					
					if(temp < dist[v]){
						dist[v] = temp;
						prev[v] = u;
					}
				}
			}
		}

		//Actual filling out of path as a list begins
		currentPath = new List<Node>();

		//If there is no possible route, return null
		if(prev[endPosition] == null){
			return currentPath = null;
		}

		//Sets the current node to the end position to work backwards to the goal
		Node current = endPosition;

		//Path is filled out, adding a node and then the previous of that node
		//When previous is equal to null it is back at the startPosition
		while(current != null){
			currentPath.Add(current);
			current = prev[current];
		}

		//Reverses the path as currently it is running from end to start
		currentPath.Reverse();

		//Returns the path for the vehicle to use
		return currentPath;
	}

	//Function to allow for the placement of roads
	public void PlaceRoad(int x, int y){
		//Sets the tile type at the position to 1(ROAD)
		tiles [x, y] = 1;

		//Destroys tile in that position and creates a new one of the new type
		TileType tt = tileTypes[tiles[x,y]];
		Destroy (tilesGrid [x, y]);
		tilesGrid [x, y] = (GameObject)Instantiate (tt.tilePrefab, new Vector3 (x, 0, y), Quaternion.identity);

		//Sets values on the tile
		RoadPlacement rp = tilesGrid[x,y].GetComponent<RoadPlacement>();
		rp.tileX = x;
		rp.tileY = y;
		rp.map = this;

		//Sets values on the tile
		TileData td = tilesGrid[x,y].GetComponent<TileData>();
		td.tileX = x;
		td.tileY = y;
		td.map = this;
	}

	//Function to allow for the placement of grass
	public void PlaceGrass(int x, int y){
		//Sets the tile type at the position to 0(GRASS)
		tiles [x, y] = 0;

		//Destroys tile in that position and creates a new one of the new type
		TileType tt = tileTypes[tiles[x,y]];
		Destroy (tilesGrid [x, y]);
		tilesGrid [x, y] = (GameObject)Instantiate (tt.tilePrefab, new Vector3 (x, 0, y), Quaternion.identity);

		//Sets values on the tile
		RoadPlacement rp = tilesGrid[x,y].GetComponent<RoadPlacement>();
		rp.tileX = x;
		rp.tileY = y;
		rp.map = this;

		//Sets values on the tile
		TileData td = tilesGrid[x,y].GetComponent<TileData>();
		td.tileX = x;
		td.tileY = y;
		td.map = this;
	}
}