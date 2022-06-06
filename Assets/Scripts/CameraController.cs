using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player; // Mario's Transform
    public Transform endLimit; // GameObject that indicates end of map
    public Transform startLimit; // GameObject that indicates end of map
    private float offset; // initial x-offset between camera and Mario
    private float startX; // smallest x-coordinate of the Camera
    private float endX; // largest x-coordinate of the camera
    private float viewportHalfWidth;
    // Start is called before the first frame update
    void Start()
    {
        // get coordinate of the bottomleft of the viewport
        // z doesn't matter since the camera is orthographic
        Vector3 bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        viewportHalfWidth = Mathf.Abs(bottomLeft.x - this.transform.position.x);

        offset = this.transform.position.x - player.position.x;
        startX = startLimit.transform.position.x + viewportHalfWidth;
        endX = endLimit.transform.position.x - viewportHalfWidth;
        // Debug.Log("bottom left: " + bottomLeft);
        // Debug.Log("viewportHalfWidth: " + viewportHalfWidth);
        // Debug.Log("offset: " + offset);
        // Debug.Log("startX: " + startX);
        // Debug.Log("endX: " + endX);
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log("Player x is " + player.position.x);
        // Debug.Log("Camera position is " + this.transform.position.x);
        float desiredX = player.position.x + offset;
        // Debug.Log("Desired x: " + desiredX);
        // Debug.Log("end x: " + endX);
        // check if desiredX is within startX and endX
        if (desiredX > startX && desiredX < endX)
        {
            this.transform.position = new Vector3(desiredX, this.transform.position.y, this.transform.position.z);
        }
    }
}
