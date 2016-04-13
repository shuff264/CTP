using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Vehicle : MonoBehaviour {

	public int tileX; //Tile position
	public int tileY; //Tile position
	public GameObject arrow; //Arrow pointer object
	public List<Node> currentPath = null; //Vehicle path
	public Vector3 endPosition; //Target destination
	public float speed = 0.0f; //Speed of vehicle
	public LineRenderer lr; //Line renderer component

	//TODO: REMOVE THESE
	public float turningReturn;
	public float accelerationReturn;
	public float rayCastReturn;
	public float totalSpeed;

	int randomX; //Random value to decide goal
	int randomY; //Random value to decide goal
	float lastRot; //The rotation of the vehicle during the last frame
	float startTime; //Starting time of the lerp
	float journeyLength; //Total length for the vehicle to travel
	float maxSpeed = 0; //Max speed decided by the tile it is on
	float acceleration = 0.4f; //Rate of change of speed
	float timeNotMoved = 0f; //Total time since the vehicle last moved
	float respawnTime = 10f; //Max amount of time a vehicle can not move for before it is despawned
	Vector3 lastPosition; //Position of the vehicle last frame
	Vector3 startPosition; //Vehicles starting positon

	//Start up function for the vehicle
	//Called each time the vehicle is pulled out of the pool
	public void VehicleStart(){
		GlobalVehicleControl.instance.cars.Add(this); //Adds it to the global vehicle list

		//Random values used to set end position
		randomX = Random.Range(0,24);
		randomY = Random.Range(0,24);

		//Setting start time for the lerp
		startTime = Time.time;

		//Setting start positon to a random value so that it is always different when it comes out of the pool
		startPosition = new Vector3(Random.Range(0, TileMap.instance.mapSizeX), 0.8f, Random.Range(0, TileMap.instance.mapSizeY));

		//If statement to make sure the start position is drivable
		if(TileMap.instance.MovementAllowed((int)startPosition.x, (int)startPosition.z) == false){
			DestroyVehicle();
		} else{
			//Sets the tile x and y to the start position
			tileX = (int)startPosition.x;
			tileY = (int)startPosition.z;

			//Function to generate the goal node
			GenerateEndPosition();

			//Bulk of the pathfinding takes place here
			currentPath = TileMap.instance.PathFinder(tileX, tileY, (int)endPosition.x, (int)endPosition.z);

			//If a path could not be created, destory the vehicle or carry on
			if (currentPath == null) {
				DestroyVehicle ();
			} else {
				SetUpLineRender (); //Carries out the initialisation of the line renderer
			}
		}
	}

	void Update(){
		//Sets the max speed based on the tile the vehicle is on - this allows for the expansion to multiple road types
		maxSpeed = TileMap.instance.tileTypes [TileMap.instance.tiles[currentPath [0].x, currentPath [0].y]].maxSpeed;

		//Setting up ray cast to detect vehicles ahead
		Ray distanceRay = new Ray(gameObject.transform.GetChild(0).transform.position, gameObject.transform.GetChild(0).transform.forward);
		//Debug comand to make it visible in scene
		Debug.DrawRay(gameObject.transform.GetChild(0).transform.position, gameObject.transform.GetChild(0).transform.forward * 2);

		//Function to change the speed of the vehicle based on a number of variables
		AdjustSpeed(distanceRay);

		//Handles the movement of the vehicle
		VehicleMovement ();

		//Final helper function to check if the vehicle has moved recently. If it has not it is despawned to prevent glitches or vehicles being eternally stuck.
		CheckIfMoved ();
	}

	//Handles the movement of the vehicle
	public void VehicleMovement(){
		//TODO: Add summary about how force movement was going to be a thing but it didnt
		//TODO: Add overall summaries to each major area to describe what is happening
		//Catches any vehicles that havent been destoryed when they should have been
		if(currentPath == null){
			DestroyVehicle();
		}
		//Calculates the journey lengeth of the vehicle. Used for LERP.
		journeyLength = Vector3.Distance (new Vector3(currentPath[0].x, 0, currentPath[0].y), new Vector3 (currentPath[1].x, 0, currentPath[1].y));

		//Calculates further values needed for LERP
		float distCovered = (Time.time - startTime) * speed;
		float fracJourney = distCovered / journeyLength;

		//Carries out the lerp for this frame
		gameObject.transform.position = Vector3.Lerp (new Vector3(currentPath[0].x, 0, currentPath[0].y), new Vector3 (currentPath[1].x, 0, currentPath[1].y), fracJourney);

		//If a vehicle is moving, carrying out rotation. Prevents problems when look rotation is 0
		if(speed > 0.1){
			RotateVehicle();
		}
		//If the vehicle is at the next node in the path, reset the start time and remove the node from the path
		if(gameObject.transform.position == new Vector3(currentPath[1].x, 0, currentPath[1].y)){
			startTime = Time.time;
			currentPath.RemoveAt(0);
		}

		//If the vehicle is at the final node, destory it
		if(currentPath.Count <= 1){
			DestroyVehicle();
		}
	}

	void GenerateEndPosition(){
		//Generates an end position that is always movable for vehicles
		while(!TileMap.instance.MovementAllowed(randomX, randomY)){
			randomX = Random.Range(0,24);
			randomY = Random.Range(0,24);
		}

		//Sets end position bsaed on random values
		endPosition = new Vector3 (randomX, 0, randomY);
	}

	void CheckIfMoved(){
		//Checks the current position against the last position, if they are the same add to the timer if not set it to 0
//		if(lastPosition != null){
			if(gameObject.transform.position == lastPosition){
				timeNotMoved += Time.deltaTime;
			}
			else {
				timeNotMoved = 0f;
			}
		//}

		//If its not moved for too long, destroy it
		if (timeNotMoved > respawnTime) {
			DestroyVehicle ();
		}

		//Set last positon to this frames position
		lastPosition = gameObject.transform.position;
	}

	void AdjustSpeed(Ray distanceRay){
		//Speed is influenced by; max speed, acceleration, whether they are turning, traffic lights, vehicles that are in front

		accelerationReturn = Accelerate ();
		turningReturn = Turning ();
		rayCastReturn = Distance (distanceRay);

		totalSpeed += accelerationReturn + turningReturn + rayCastReturn;


		//Calculates a new speed value based on many different factors. If its below 0, set it to 0
		if((speed += (Accelerate() + Turning() + Distance(distanceRay))) <0f){
			speed = 0.0f;
		}else{
			speed += (Accelerate() + Turning() + Distance(distanceRay));
		}
		
	}

	//Applies a base acceleration to the vehicles at al ltimes
	float Accelerate(){
		if (speed <= maxSpeed) {
			return acceleration;
		}
		else{
			return 0;
		}
	}

	//If the vehicle is turning, slow it down
	float Turning(){
		if(gameObject.transform.rotation.y != lastRot){
			lastRot = gameObject.transform.rotation.y;
			return -0.3f;

		} else {
			lastRot = gameObject.transform.rotation.y;
			return 0;
		}
	
	}

	//Utilises a raycast to calculate how far away vehicles in front are and adjusts speed accordingly
	float Distance(Ray distanceRay){

		float maxReduce = 1f;
		RaycastHit hit;

		//Raycat calculations
		if(Physics.Raycast(distanceRay, out hit, 1)){
			//If a car is infront apply a basic reduce based on distance away
			if(hit.collider.tag == "car"){
				return -(maxReduce - hit.distance );
			}else if(hit.collider.tag == "lights"){
				//If it is traffic lights, first must check if they are on green and reduce speed
				if(gameObject.transform.position.x == hit.transform.position.x){
					if(hit.transform.GetComponent<TrafficLights>().xGreen == true){
						return -(maxReduce - hit.distance );
					} else if(hit.transform.GetComponent<TrafficLights>().zGreen == true){
						return 0;	
					}
					else{
						return 0;
					}
				}else if(gameObject.transform.position.z == hit.transform.position.z){
					if(hit.transform.GetComponent<TrafficLights>().xGreen == true){
						return 0;
					} else if(hit.transform.GetComponent<TrafficLights>().zGreen == true){
						return -(maxReduce - hit.distance );
					}
					else{
						return 0;
					}
				} else {
					return 0;
				}
			}else{
				return 0;
			}
		}else{
			return 0;
		}
	}

	//Applys rotation to the vehicle when a turn is being made
	void RotateVehicle(){
		Quaternion targetRotation;

		//Look rotation turns the vehicle to face the next node
		targetRotation = Quaternion.LookRotation(new Vector3(currentPath[0].x, 0, currentPath[0].y) - transform.position);
		float str = Mathf.Min ((speed * 2) * Time.deltaTime, 1);

		//Setting the rotation of the vehicle
		transform.rotation = Quaternion.Lerp (transform.rotation, targetRotation, str);	
	}

	void SetUpLineRender(){
		//Tied to the global vehicle control
		lr.enabled = GlobalVehicleControl.instance.drawRoute; 
		//Colour set to red, did dabble with random colours but looked wrong
		lr.SetColors(Color.red, Color.red);
		lr.SetWidth(0.2F, 0.2F);
		//Sets the vertex count to the length of the path
		lr.SetVertexCount(currentPath.Count);

		//Line render has positions set based on the current path positions
		for(int i=0; i<=currentPath.Count; i++){
			if(i != currentPath.Count){
				lr.SetPosition(i, new Vector3(currentPath[i].x, 1, currentPath[i].y));
			}
		}
	}

	//Clean up function to return the vehicle to pool
	void DestroyVehicle(){

		currentPath = null;

		//Removes it from global vehicles
		GlobalVehicleControl.instance.cars.Remove(this);

		//Calls pooling script to return it to the pool
		PoolingScript.instance.ReturnCar(gameObject);

		//Disables objects which show it as clicked to tidy up
		lr.enabled = false;
		arrow.SetActive (false);
	}

	//Little function to handle mouse clicks
	void OnMouseDown(){
		Selected ();
	}

	//Alternates the activeness based on clicks
	void Selected(){
		lr.enabled = !lr.enabled;
		arrow.SetActive (!arrow.activeSelf);
	}



}	
