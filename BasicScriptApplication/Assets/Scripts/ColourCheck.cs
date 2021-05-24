using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourCheck : MonoBehaviour
{

	public GameObject thingToRemove;

	//public float variables 

	public float Red = 1.0f;
	public float Green = 0.0f;
	public float Blue = 0.0f;


	//when an object enters this trigger object call theis function
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			if (other.GetComponent<Renderer>().material.color.b == Blue)
			{
				if (other.GetComponent<Renderer>().material.color.r == Red)
				{
					if (other.GetComponent<Renderer>().material.color.g == Green)
					{
						thingToRemove.gameObject.SetActive(false);
					}
				}
			}
		}
	}

}