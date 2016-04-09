﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class VehicleSpawn : MonoBehaviour {

	int randomValue;
	int randomX;
	int randomY;

	public GameObject car;
	public TileMap tm;

	void Start () {
		tm = GameObject.Find("Map").GetComponent<TileMap>();
	}

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
