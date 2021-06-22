using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControllerScript : MonoBehaviour
{
    // Variables needed

    public Camera mainCam; // Uses main camera to get world position of mouse clicks later on
    public Rigidbody playerCharacter; // reference to player rigidbody
    public Text inputTrackingText; // refernce for where the 'inputTracker' int applies to on screen
    public float power = 10f; // base number slings are calculated too
    public bool canSling; // checks if its valid to sling
    public LineTrajectoryScript lineTraj; // Calls other script here for line rendering

    public float addedDirectionalForce; // Stores overall force of push 
    float addedDirectionalForceX; // Stores X value of mouseEndPoint and playerposition
    float addedDirectionalForceY; // Stores Y value of mouseEndPoint and playerposition 

    public float minSlingDistance; // min required distance to draw line + allow the playr to launch themselves
    public float maxSlingDistance; // max required distance to draw line + allow the playr to launch themselves

    public bool requiredPower = true; // Connects to the 'EnergyMeterScript' to disable firing when not enough energy is present
    public int inputTracker = 0; // Used for UI tracking of player inputs.

    Vector3 currentMousePosition; // Actively tracked mouse position
    Vector3 playerPosition; // tracks player position
    Vector3 mouseEndPoint; //tracks where mouse is no longer held down
    Vector3 launchDirection; // Impulse added to player character

    public static PlayerControllerScript instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        // calls line trajectory script
        lineTraj = GetComponent<LineTrajectoryScript>();
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
        // If else, make the line red
        if (canSling == true)
        {
            lineTraj.wantedLineRenderer.startColor = new Color(255, 255, 255);
            lineTraj.wantedLineRenderer.endColor = new Color(0, 122, 255);
        }
        else
        {
            lineTraj.wantedLineRenderer.startColor = new Color(255, 255, 255);
            lineTraj.wantedLineRenderer.endColor = new Color(255, 0, 0);
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

            // minuses the mouseEndPoint by the playerPosition, then normalizes to return a smooth number that isn't a bloated vector (x and y seperate)
            launchDirection = (mouseEndPoint - playerPosition).normalized;

            addedDirectionalForceX = mouseEndPoint.x - playerPosition.x;
            addedDirectionalForceY = mouseEndPoint.y - playerPosition.y;


            // Removes negatives from force in X axis
            if (addedDirectionalForceX <= 0)
            {
                addedDirectionalForceX = addedDirectionalForceX * -1;
            }
            // Removes negatives from force in Y axis
            if (addedDirectionalForceY <= 0)
            {
                addedDirectionalForceY = addedDirectionalForceY * -1;
            }

            addedDirectionalForce = (addedDirectionalForceX + addedDirectionalForceY) * power;

            if (canSling == true && requiredPower == true)
            {
                playerCharacter.AddForce(launchDirection * addedDirectionalForce, ForceMode.Impulse);
                addedDirectionalForce = addedDirectionalForce * 2;
                EnergyMeterScript.instance.DrainPower(addedDirectionalForce);
                inputTracker++;
                TimerScript.instance.StartTimer();
            }
            // will end line even if canSling is set to false
            lineTraj.EndLine();

            // updates inputTracker UI element with amount of 'valid' inputs
            inputTrackingText.text = ""+inputTracker;
        }
    }

}
