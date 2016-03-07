using UnityEngine;
using System.Collections;

public class test : MonoBehaviour {

	public GameObject tester;

	// Use this for initialization
	void Start () {

		Instantiate(tester, new Vector3(0,0,0), Quaternion.identity);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
