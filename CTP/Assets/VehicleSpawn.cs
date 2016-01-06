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
	
		randomValue = Random.Range (0, 1000);		

		if (randomValue < 1) {
		
			Instantiate(car, new Vector3(gameObject.transform.position.x, 1, gameObject.transform.position.z), Quaternion.identity);
		
		}


	}
}
