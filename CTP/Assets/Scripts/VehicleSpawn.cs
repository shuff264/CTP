using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class VehicleSpawn : MonoBehaviour {

	int randomValue;
	int randomX;
	int randomY;

	public GameObject car;
	public Slider spawnRateSlider;
	public TileMap tm;

	void Start () {
		tm = GameObject.Find("Map").GetComponent<TileMap>();
		spawnRateSlider.value = 100.0f;

	}

	void Update () {
		randomValue = Random.Range (0, 1000);

		randomX = Random.Range(0,tm.mapSizeX-1);
		randomY = Random.Range(0,tm.mapSizeY-1);

		if (randomValue < spawnRateSlider.value) {
			if(tm.MovementAllowed(randomX, randomY)){
				Instantiate(car, new Vector3(randomX, 1f, randomY), Quaternion.identity);
			}
			else{
				randomX = Random.Range(0,24);
				randomY = Random.Range(0,24);
			}
		}
	}
}
