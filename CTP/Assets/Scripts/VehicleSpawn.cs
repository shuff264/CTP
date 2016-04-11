using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class VehicleSpawn : MonoBehaviour {

	int randomValue;
	int randomX;
	int randomY;

	public TileMap tm;

	void Start () {
		tm = GameObject.Find("Map").GetComponent<TileMap>();
	}

	//Uses a random value to control whether a vehicle is to be spawned
	//The cut off value is decided by the spawn rate slider
	//If it is less than the spawn rate, it spawns a vehicle
	//The vehicle checks if movement is allowed at the spawn tile which has been randomly chosen
	//Then calls the poolingScript to spawn the vehicle
	void Update () {
		randomValue = Random.Range (0, 1000);

		randomX = Random.Range(0,tm.mapSizeX-1);
		randomY = Random.Range(0,tm.mapSizeY-1);

		if (randomValue < UIController.instance.spawnRateSlider.value) {
			if(tm.MovementAllowed(randomX, randomY)){
				GameObject obj = PoolingScript.instance.GetCar();
				obj.SetActive(true);
				obj.GetComponent<Vehicle>().VehicleStart();
			}
			else{
				randomX = Random.Range(0,24);
				randomY = Random.Range(0,24);
			}
		}
	}
}
