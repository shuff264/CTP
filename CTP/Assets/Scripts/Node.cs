using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Node {

	//Node class used to form the graph for pathfinding

	//List of the four neighbours
	public List<Node> neighbours;

	//Nodes position in the map
	public int x;
	public int y;

	//Contrsuctor
	public Node(){
		neighbours = new List<Node>();
	}

	//Returns distance from a node to another
	public float DistanceTo(Node n){
		return Vector2.Distance(new Vector2(x,y), new Vector2(n.x,n.y));
	}
}
