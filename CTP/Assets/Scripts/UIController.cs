using UnityEngine;
using System.Collections;

public class UIController : Singleton<UIController> {

	public int placeType = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnClickGrass(){
		placeType = 0;
	}

	public void OnClickRoad(){
		placeType = 1;
	}

	public void OnClickLights(){
		placeType = 2;
	}
}
