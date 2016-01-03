using UnityEngine;
using System.Collections;

public class VehicleSpawn : MonoBehaviour {

	int randomValue;
	int randomX;
	int randomY;
	public GameObject car;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		randomValue = Random.Range (0, 100);
		randomX = Random.Range (0, 100);
		randomY = Random.Range (0, 100);
		

		if (randomValue < 50) {
		
			Instantiate(car, new Vector3(randomX, 0, randomY), Quaternion.identity);
		
		}


	}
}
