using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChangeMaterialColour : MonoBehaviour {

	//public float variables 
	public float Red = 1.0f;
	public float Green = 0.0f;
	public float Blue = 0.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//when an object enters this trigger object call theis function
	void OnTriggerEnter(Collider other) {

		//get the renderer compoenent of Other(The object that entered the trigger) and set its material colour to a new Color object using
		//the public floats Red, Green and Blue to set its RGB colour. (Values are 0 to 1)
		other.GetComponent<Renderer>().material.color = new Color(Red,Green,Blue);
	}
}
