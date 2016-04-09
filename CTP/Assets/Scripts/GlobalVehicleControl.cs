using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GlobalVehicleControl : MonoBehaviour {

	//Singleton to control some of the variables which effect all vehicles
	//Such as debug lines on all vehicles

	public static GlobalVehicleControl instance;

	public List<Vehicle> cars = new List<Vehicle>();
	public bool drawRoute = false;

	// Use this for initialization
	void Start () {
		instance = this;
	}
	
	// Update is called once per frame
	void Update () {

	}

	//On button press sets all the draw debug variables to true or false depending on where it is already enabled or not
	public void DrawDebug(){
		if(drawRoute == false){
			foreach(Vehicle v in cars){
				v.lr.enabled = true;
				drawRoute = true;
			}
		} else if(drawRoute == true){
			foreach(Vehicle v in cars){
				v.lr.enabled = false;
				drawRoute = false;
			}
		}
	}
}
