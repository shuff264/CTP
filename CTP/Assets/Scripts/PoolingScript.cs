using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PoolingScript : MonoBehaviour {

	public static PoolingScript instance;
	public GameObject car;

	List<GameObject> poolList = new List<GameObject>();

	// Use this for initialization
	void Start () {
		instance = this;

		for(int i = 0; i < 20; i++){
			GameObject obj = (GameObject)Instantiate(car, new Vector3(0, 0, 0), Quaternion.identity);
			obj.SetActive(false);
			poolList.Add(obj);
		}
	}

	public GameObject GetCar(){
		if(poolList.Count > 0){
			GameObject obj = poolList[0];
			poolList.RemoveAt(0);
			return obj;
		} else {
			GameObject obj = (GameObject)Instantiate(car, new Vector3(-100, -100, -100), Quaternion.identity);
			return obj;
		}

	}

	public void ReturnCar(GameObject obj){
		poolList.Add(obj);
		obj.SetActive(false);

	}


	// Update is called once per frame
	void Update () {
		Debug.Log(poolList.Count);
	}
}
