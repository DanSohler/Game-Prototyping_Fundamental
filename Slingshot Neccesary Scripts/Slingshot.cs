using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot : MonoBehaviour
{
    public float power = 10f;
    public Rigidbody2D rb;

    public bool invertControls;

    public Vector2 minPower;
    public Vector2 maxPower;

    public LineTrajectory tl;

    Camera cam;
    Vector2 force;
    Vector3 mouseStartPoint;
    Vector3 mouseEndPoint;

    private void Start()
    {
        cam = Camera.main;
        tl = GetComponent<LineTrajectory>();

        if (invertControls == true)
        {
            minPower = minPower * -1;
            maxPower = maxPower * -1;

        }

    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mouseStartPoint = cam.ScreenToWorldPoint(Input.mousePosition);
            mouseStartPoint.z = 15;
        }

        if (Input.GetMouseButton(0))
        {
            // Trying to attach the start point to where the object currently is, not working as intended
            // Vector3 currentPoint = rb.transform.position;
            if (invertControls == true)
            {
                Vector3 currentPoint = cam.ScreenToWorldPoint(Input.mousePosition);
                currentPoint.z = 15;
                tl.RenderLine(mouseStartPoint, currentPoint);
            }

            else
            {
                Vector3 currentPoint = cam.ScreenToWorldPoint(Input.mousePosition);
                currentPoint.z = 15;
                tl.RenderLine(currentPoint, mouseStartPoint);
            }
        }


        if (Input.GetMouseButtonUp(0))
        {
            mouseEndPoint = cam.ScreenToWorldPoint(Input.mousePosition);
            mouseEndPoint.z = 15;

            force = new Vector2(Mathf.Clamp(mouseStartPoint.x - mouseEndPoint.x, minPower.x, maxPower.x), Mathf.Clamp(mouseStartPoint.y - mouseEndPoint.y, minPower.y, maxPower.y));
            rb.AddForce(force * power, ForceMode2D.Impulse);
            tl.EndLine();
        }
    }
}
