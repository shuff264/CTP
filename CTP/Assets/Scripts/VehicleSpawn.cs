using UnityEngine;
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
		randomX = Random.Range(0,24);
		randomY = Random.Range(0,24);

		if (randomValue < 20) {
			if(tm.MovementAllowed(randomX, randomY)){
				Instantiate(car, new Vector3(randomX, 1, randomY), Quaternion.identity);
			}
			else{
				randomX = Random.Range(0,24);
				randomY = Random.Range(0,24);
			}
		}
	}
}
