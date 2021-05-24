using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MouseAimCamera : MonoBehaviour {

	public GameObject target; //public variable of the GameObject that will rotate based on mouse horizontal movement
	public float rotateSpeed = 5; //the speed multiplied that you want the rotate to occur on
	
	void Start() {

	}

	//called every frame (defaults is 50 frames per second)
	void LateUpdate() {
		//calculate the amount of rotation based on Mouse x (mouses horizontal movement) multiplied by public variable rotateSpeed
		float horizontal = Input.GetAxis("Mouse X") * rotateSpeed;


		//apply the rotation via the y axis onto the public GameObject target.
		target.transform.Rotate(0, horizontal, 0);

	}
}