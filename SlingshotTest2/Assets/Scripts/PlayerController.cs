using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Variables needed

    public Camera mainCam; // Uses main camera to get world position of mouse clicks later on
    public Rigidbody playerCharacter; // reference to player rigidbody
    public float power = 10f; // base number slings are calculated too
    bool canSling; // checks if its valid to sling
    bool slingshotFired; // short bool that initiates a GUI component

    public Vector2 minPush; // minimum push force
    public Vector2 maxPush; // maximum push force

    Vector3 initialMousePosition;
    Vector2 playerPosition;
    Vector2 worldPosition;
    


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // tracks mouse position 'relative to the world', used for sling direction + line draw
            initialMousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y);
            worldPosition = mainCam.ScreenToWorldPoint(initialMousePosition);
            Debug.Log("Mouse is at " + initialMousePosition);
        } 

        if (Input.GetMouseButton(0))
        {
            // tracks players position, used for determining sling direction + line draw
            Vector3 currentPlayerPosition = playerCharacter.gameObject.transform.position;
            currentPlayerPosition.z = 0;
            
        }

        if (Input.GetMouseButtonUp(0))
        {

        }
    }


}
