using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Vehicle : MonoBehaviour {

	public int tileX;
	public int tileY;

	int randomX;
	int randomY;


	public List<Node> currentPath = null;

	Vector3 startPosition;
	public Vector3 endPosition;
	public float speed = 0.1f;
	
	float startTime;
	float journeyLength;

	public TileMap tm;

	float maxSpeed = 0;
	float acceleration = 0.2f;
	Vector3 modelOffset;
	float lastRot = 0;
	float thisRot;

	Rigidbody rb;
	LineRenderer lr;
	// Use this for initialization
	void Start () {

		GlobalVehicleControl.Instance.cars.Add(this);

		tm = GameObject.Find("Map").GetComponent<TileMap>();
		rb = gameObject.GetComponent<Rigidbody> ();
		lr = gameObject.GetComponent<LineRenderer> ();

		randomX = Random.Range(0,24);
		randomY = Random.Range(0,24);

		modelOffset = new Vector3(0.3f, 0.6f, 0f);
		
		startTime = Time.time;
		startPosition = gameObject.transform.position;
		tileX = (int)startPosition.x;
		tileY = (int)startPosition.z;
		
		//Need to ditch this
		GenerateEndPosition();

		currentPath = tm.FindPathToGoal(tileX, tileY, (int)endPosition.x, (int)endPosition.z);
		
		if(currentPath == null){
			DestroyVehicle();
		}

		SetUpLineRender();

	}

	void FixedUpdate(){
		maxSpeed = tm.tileTypes [tm.tiles[currentPath [0].x, currentPath [0].y]].maxSpeed;

		Ray distanceRay = new Ray(gameObject.transform.GetChild(0).transform.position, gameObject.transform.GetChild(0).transform.forward);
		
		Debug.DrawRay(gameObject.transform.GetChild(0).transform.position, gameObject.transform.GetChild(0).transform.forward * 2);

		AdjustSpeed(distanceRay);
		

		MoveNextTile();
		lr.enabled = GlobalVehicleControl.Instance.drawRoute;

	}

	public void MoveNextTile(){
		if(currentPath == null){
			DestroyVehicle();
		}

		//startPosition = new Vector3 (currentPath[0].x, 1, currentPath[0].y); 
		//endPosition = new Vector3 (currentPath[1].x, 1, currentPath[1].y); 

		//THIS SHOULD DO THE BETTER MOVEMENT
		//rb.AddForce (transform.forward * speed);
		
		//
		//		rb.AddForce(transform.forward * Time.deltaTime, ForceMode.Acceleration);
		//		//rb.AddTorque(targetDir);

		journeyLength = Vector3.Distance (new Vector3(currentPath[0].x, 0, currentPath[0].y), new Vector3 (currentPath[1].x, 0, currentPath[1].y));

		float distCovered = (Time.time - startTime) * speed;
		float fracJourney = distCovered / journeyLength;
		gameObject.transform.position = Vector3.Lerp (new Vector3(currentPath[0].x, 0, currentPath[0].y), new Vector3 (currentPath[1].x, 0, currentPath[1].y), fracJourney);

		//ROTATION STUFF
		Quaternion targetRotation;
		targetRotation = Quaternion.LookRotation(new Vector3(currentPath[0].x, 0, currentPath[0].y) - transform.position);
		float str = Mathf.Min ((speed * 10) * Time.deltaTime, 1);



		transform.rotation = Quaternion.Lerp (transform.rotation, targetRotation, str);	

		if(gameObject.transform.position == new Vector3(currentPath[1].x, 0, currentPath[1].y)){
			startTime = Time.time;
			currentPath.RemoveAt(0);
		}
	
		if(currentPath.Count <= 1){
			DestroyVehicle();
		}
	}

	void GenerateEndPosition(){
		while(!tm.MovementAllowed(randomX, randomY)){
			randomX = Random.Range(0,24);
			randomY = Random.Range(0,24);
		}

		endPosition = new Vector3 (randomX, 0, randomY);
	}

	void SetUpLineRender(){
		lr.enabled = GlobalVehicleControl.Instance.drawRoute;
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
		GlobalVehicleControl.Instance.cars.Remove(this);
		Destroy(gameObject);
	}

	void AdjustSpeed(Ray distanceRay){
		//Speed is influenced by; max speed, acceleration, whether they are turning, traffic lights, vehicles that are in front

		if((speed += (Accelerate() + Turning() + Distance(distanceRay))) <0){
			speed = 0;
		}else{
			speed += (Accelerate() + Turning() + Distance(distanceRay));
		}

//		speed += Accelerate();
//		if(speed >= 0){
//			 speed += (Turning() + Distance( distanceRay));
//		}
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
//		if we ar turning
//			return -0.2
//				else
//					return 0


//		thisRot = gameObject.transform.rotation.y;
//
//		if(thisRot != lastRot){
//			return -0.5f;
//		}
//
//		lastRot = thisRot;
//
		return 0;

	}

	float Distance(Ray distanceRay){
//		if object is in front
//			return negative value scaled to distance and there velocity away
//				else
//					return 0	

//		use a raycast a certain distance in front scale speed based on that
		float maxReduce = 1f;
		RaycastHit hit;
		

		if(Physics.Raycast(distanceRay, out hit, 2)){
			if(hit.collider.tag == "car" || hit.collider.tag == "lights"){
				return -(maxReduce - hit.distance /2 );
			}else{
				return 0;
			}
		}else{
			return 0;
		}
	}

	void OnCollisionEnter(Collision coll){

	}


}	
