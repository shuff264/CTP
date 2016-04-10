using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraMovement : MonoBehaviour {

	Rigidbody rb;
	float height;

	void Start () {
		rb = gameObject.GetComponent<Rigidbody> ();
		height = gameObject.transform.position.y;
	}

	void Update () {
		//Utilises the input manager to control the camera
		//Arrow keys to love it laterally
		//Scroll wheel to go up and down
		//Mouse axis used to aim camera - its a bit weird atm
		float forwardMovement = Input.GetAxis ("Vertical");
		float sidewaysMovement = Input.GetAxis ("Horizontal");
		Vector3 movement = new Vector3 (sidewaysMovement * 10, 0, forwardMovement * 10);

		rb.velocity = movement;
		
		if (Input.GetButton("Fire2")) {

			float h =  Input.GetAxis("Mouse X"); //TODO: Works but might be a bit slow and may need refining. A bit janky.
			float v =  Input.GetAxis("Mouse Y");
			v = -v;
			transform.Rotate(v, h, 0);

		}

		height += Input.GetAxis ("Mouse ScrollWheel");
		if (height >= 1) {
			gameObject.transform.position = new Vector3 (gameObject.transform.position.x, height, gameObject.transform.position.z);
		} else {
			height = 1;
		}
	}
}
