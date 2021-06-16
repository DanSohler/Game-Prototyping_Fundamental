using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    // Variables needed

    public Camera mainCam; // Uses main camera to get world position of mouse clicks later on
    public Rigidbody playerCharacter; // reference to player rigidbody
    public float power = 10f; // base number slings are calculated too
    public bool invertControls; // Allows to invert push values for playtesting
    bool canSling; // checks if its valid to sling
    bool slingshotFired = false; // short bool that initiates a GUI component
    public LineTrajectory lineTraj; // Calls other script here for line rendering

    float addedDirectionalForceX; // Value for storing a float 
    float addedDirectionalForceY; // Value for storing a float 
    float addedDirectionalForce; // Value for storing a float 

    public float minSlingDistance; // min required distance to draw line + allow the playr to launch themselves
    public float maxSlingDistance; // max required distance to draw line + allow the playr to launch themselves

    Vector3 currentMousePosition; // Actively tracked mouse position
    Vector3 playerPosition; // tracks player position
    Vector3 mouseEndPoint; //tracks where mouse is no longer held down
    Vector3 launchDirection; // Impulse added to player character

    // Energy meter variables
    public Slider energyMeter;

    private void Start()
    {
        // calls line trajectory script
        lineTraj = GetComponent<LineTrajectory>();
    }

    public void Update()
    {
        // Tracks current mouse position on every frame
        currentMousePosition = mainCam.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, 12));
        // Tracks players position, used for determining sling direction + line draw
        playerPosition = playerCharacter.gameObject.transform.position;


        // Min/Max launch distance code
        // Minimum distance player can launch themselves  too
        if (Vector3.Distance(currentMousePosition, playerPosition) < minSlingDistance)
        {
            // If cursor is within minimum sling distance (i.e inside the player), then disable canSling
            canSling = false;
        }
        else if (Vector3.Distance(currentMousePosition, playerPosition) > maxSlingDistance)
        {
            // If cursor is beyond the maximum sling distance, then disable canSling
            canSling = false;
        }
        else
        {
            // If its within both the maxSlingDistance and the minSlingDistance, then set canSling to true
            canSling = true;
        }


        // If its true, make the line blue
        // If its false, make the line red
        if (canSling == true)
        {
            lineTraj.wantedLineRenderer.startColor = new Color(255, 255, 255);
            lineTraj.wantedLineRenderer.endColor = new Color(0, 122, 255);
        }
        else
        {
            lineTraj.wantedLineRenderer.startColor = new Color(255, 255, 255);
            lineTraj.wantedLineRenderer.endColor = new Color(255, 10, 0);
        }

        // While mouse is held down, render line between player position and currentMousePositions
        if (Input.GetMouseButton(0))
        {
            lineTraj.RenderLine(playerPosition, currentMousePosition);
        }

        // Uses the players current position, and calculates the direction of the impulse 
        // by the position the mouse was before it was let go.
        if (Input.GetMouseButtonUp(0))
        {
            // takes the end point the mouse cursor was at, used for calculating angle of impulse and distance
            mouseEndPoint = mainCam.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, 12));

            // minuses the mouseEndPoint by the playerPosition, then normalizes to return a smooth number that isn't a bloated vector
            launchDirection = (mouseEndPoint - playerPosition).normalized;

            // multiplies the force that will be pushing the player by the x and y position of both the player and mouse position, by a flat value.
            addedDirectionalForceX = mouseEndPoint.x - playerPosition.x;

            if (addedDirectionalForceX <= 0)
            {
                addedDirectionalForceX = addedDirectionalForceX * -1;
            }

            addedDirectionalForceY = mouseEndPoint.y - playerPosition.y;
            if (addedDirectionalForceY <= 0)
            {
                addedDirectionalForceY = addedDirectionalForceY * -1;
            }

            addedDirectionalForce = (addedDirectionalForceX + addedDirectionalForceY) * power;

            if (canSling == true)
            {
                playerCharacter.AddForce(launchDirection * addedDirectionalForce, ForceMode.Impulse);

                Debug.Log("addedDirectionalForce = " + addedDirectionalForce);

                Debug.Log("launchDirection = " + launchDirection);
                slingshotFired = true;
            }
            // will end line even if canSling is set to false
            lineTraj.EndLine();
            slingshotFired = false;
        }
    }
}
