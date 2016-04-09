using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraMovement : MonoBehaviour {

	Rigidbody rb;

	// Use this for initialization
	void Start () {
		rb = gameObject.GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		float forwardMovement = Input.GetAxis ("Vertical");
		Vector3 movement = new Vector3 (0, 0, forwardMovement * 10);

		//TODO:Fix all this so its nicer
		//TODO:Maybe use the scroll wheel to move over the arrow keys
		//TODO:Or arrow keys to move left and right forward and back with mouse to zoom and look
		if (forwardMovement > 0f) {
			rb.velocity = gameObject.transform.rotation * movement;
		} else if (forwardMovement < 0f) {
			rb.velocity = gameObject.transform.rotation * movement; 
		}
	
		if (Input.GetButton("Fire2")) {

			float h =  Input.GetAxis("Mouse X"); //TODO: Works but might be a bit slow and may need refining. A bit janky.
			float v =  Input.GetAxis("Mouse Y");
	
			transform.Rotate(v, h, 0);

		}
	}
}
