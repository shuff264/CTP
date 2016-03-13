using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GlobalVehicleControl : MonoBehaviour {

	public static GlobalVehicleControl instance;

	public List<Vehicle> cars = new List<Vehicle>();
	public bool drawRoute = false;

	// Use this for initialization
	void Start () {
		instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space)){
			drawRoute = !drawRoute;
		}
	}
}
