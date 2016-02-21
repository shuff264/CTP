using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GlobalVehicleControl : Singleton<GlobalVehicleControl> {

	protected GlobalVehicleControl () {}

	public List<Vehicle> cars = new List<Vehicle>();
	public bool drawRoute = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space)){
			drawRoute = !drawRoute;
		}
	}
}
