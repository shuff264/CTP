using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Vehicle : MonoBehaviour {

	public int tileX;
	public int tileY;

	int randomX;
	int randomY;

	public GameObject arrow;


	public List<Node> currentPath = null;

	Vector3 startPosition;
	public Vector3 endPosition;
	public float speed = 0.1f;

	float lastRot;
	
	float startTime;
	float journeyLength;

	public TileMap tm;

	float maxSpeed = 0;
	float acceleration = 0.2f;

	float timeNotMoved = 0f;
	float respawnTime = 10f;
	Vector3 lastPosition;
	
	public LineRenderer lr;

	// Use this for initialization
	void Awake () {
		
		tm = GameObject.Find("Map").GetComponent<TileMap>();
		lr = gameObject.GetComponent<LineRenderer> ();
		
	}

	public void VehicleStart(){
		GlobalVehicleControl.instance.cars.Add(this);

		randomX = Random.Range(0,24);
		randomY = Random.Range(0,24);
		
		startTime = Time.time;
		startPosition = new Vector3(Random.Range(0, TileMap.instance.mapSizeX), 0.8f, Random.Range(0, TileMap.instance.mapSizeY));
		if(TileMap.instance.MovementAllowed((int)startPosition.x, (int)startPosition.z) == false){
			DestroyVehicle();
		} else{
			tileX = (int)startPosition.x;
			tileY = (int)startPosition.z;
			
			GenerateEndPosition();

			currentPath = tm.PathFinder(tileX, tileY, (int)endPosition.x, (int)endPosition.z);

			if (currentPath == null) {
				DestroyVehicle ();
			} else {
				SetUpLineRender ();
			}
		}
	}


	void FixedUpdate(){

		maxSpeed = tm.tileTypes [tm.tiles[currentPath [0].x, currentPath [0].y]].maxSpeed;

		Ray distanceRay = new Ray(gameObject.transform.GetChild(0).transform.position, gameObject.transform.GetChild(0).transform.forward);
		
		Debug.DrawRay(gameObject.transform.GetChild(0).transform.position, gameObject.transform.GetChild(0).transform.forward * 2);

		AdjustSpeed(distanceRay);
		
		MoveNextTile();
		//lr.enabled = GlobalVehicleControl.instance.drawRoute;
		if(lastPosition != null){
			if(gameObject.transform.position == lastPosition){
				timeNotMoved += Time.deltaTime;
			}
			else {
				timeNotMoved = 0f;
			}
		}

		if (timeNotMoved > respawnTime) {
			DestroyVehicle ();
		}

		lastPosition = gameObject.transform.position;
	}

	public void MoveNextTile(){
		if(currentPath == null){
			DestroyVehicle();
		}

		journeyLength = Vector3.Distance (new Vector3(currentPath[0].x, 0, currentPath[0].y), new Vector3 (currentPath[1].x, 0, currentPath[1].y));

		float distCovered = (Time.time - startTime) * speed;
		float fracJourney = distCovered / journeyLength;
		gameObject.transform.position = Vector3.Lerp (new Vector3(currentPath[0].x, 0, currentPath[0].y), new Vector3 (currentPath[1].x, 0, currentPath[1].y), fracJourney);

		if(speed > 0){
			RotateVehicle();
		}



		if(gameObject.transform.position == new Vector3(currentPath[1].x, 0, currentPath[1].y)){
			startTime = Time.time;
			currentPath.RemoveAt(0);
		}
	
		if(currentPath.Count <= 1){
			DestroyVehicle();
		}
	}

	void RotateVehicle(){
		Quaternion targetRotation;
		targetRotation = Quaternion.LookRotation(new Vector3(currentPath[0].x, 0, currentPath[0].y) - transform.position);
		float str = Mathf.Min ((speed * 5) * Time.deltaTime, 1);

		transform.rotation = Quaternion.Lerp (transform.rotation, targetRotation, str);	
	}

	void GenerateEndPosition(){
		while(!tm.MovementAllowed(randomX, randomY)){
			randomX = Random.Range(0,24);
			randomY = Random.Range(0,24);
		}

		endPosition = new Vector3 (randomX, 0, randomY);
	}

	void SetUpLineRender(){
		lr.enabled = GlobalVehicleControl.instance.drawRoute;
		lr.SetColors(Color.red, Color.red);
		lr.SetWidth(0.2F, 0.2F);
		lr.SetVertexCount(currentPath.Count);

		for(int i=0; i<=currentPath.Count; i++){
			if(i != currentPath.Count){
				lr.SetPosition(i, new Vector3(currentPath[i].x, 1, currentPath[i].y));
			}
		}
	}

	void DestroyVehicle(){
		currentPath = null;
		GlobalVehicleControl.instance.cars.Remove(this);
		PoolingScript.instance.ReturnCar(gameObject);
	}

	void AdjustSpeed(Ray distanceRay){
		//Speed is influenced by; max speed, acceleration, whether they are turning, traffic lights, vehicles that are in front

		if((speed += (Accelerate() + Turning() + Distance(distanceRay))) <0){
			speed = 0;
		}else{
			speed += (Accelerate() + Turning() + Distance(distanceRay));
		}
		
	}

	float Accelerate(){
		if (speed <= maxSpeed) {
			return acceleration;
		}
		else{
			return 0;
		}
	}

	float Turning(){
		if(gameObject.transform.rotation.y != lastRot){
			lastRot = gameObject.transform.rotation.y;
			return -0.3f;

		} else {
			lastRot = gameObject.transform.rotation.y;
			return 0;
		}
	
	}

	float Distance(Ray distanceRay){
		float maxReduce = 1f;
		RaycastHit hit;
		

		if(Physics.Raycast(distanceRay, out hit, 1)){
			if(hit.collider.tag == "car"){
				return -(maxReduce - hit.distance );
			}else if(hit.collider.tag == "lights"){
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

	void OnMouseDown(){
		lr.enabled = !lr.enabled;
		arrow.SetActive (!arrow.activeSelf); //TODO: Reset this after its destoryed
	}


}	
