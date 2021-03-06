﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PoolingScript : MonoBehaviour {
	//Script controls the object pooling for the vehicles

	//Singleton
	public static PoolingScript instance;

	public GameObject car;

	//List of all vehicles not current active
	List<GameObject> poolList = new List<GameObject>();

	// Use this for initialization
	void Start () {
		instance = this;

		//Fills the poollist with 20 inactive vehicles to start with
		for(int i = 0; i < 20; i++){
			GameObject obj = (GameObject)Instantiate(car, new Vector3(0, 0, 0), Quaternion.identity);
			obj.transform.SetParent(gameObject.transform);
			obj.SetActive(false);
			poolList.Add(obj);
		}
	}

	//When called, removes the first vehicle in a list and returns it
	//Might be more effiecnt to not remove from list but by doing this it saves time checking whether the vehicle is already active
	//If the list is empty it creates a new vehicle which will be added to the list later
	public GameObject GetCar(){
		if(poolList.Count > 0){
			GameObject obj = poolList[0];
			poolList.RemoveAt(0);
			return obj;
		} else {
			GameObject obj = (GameObject)Instantiate(car, new Vector3(-100, -100, -100), Quaternion.identity);
			obj.transform.SetParent(gameObject.transform);
			return obj;
		}

	}
	//When a vehicle is at the end of its life it is return to the list and disabled
	//It can be used again later
	public void ReturnCar(GameObject obj){
		poolList.Add(obj);
		obj.SetActive(false);

	}

}
