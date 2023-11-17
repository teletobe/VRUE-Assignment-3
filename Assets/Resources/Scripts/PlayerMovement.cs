using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.XR;
using Unity.XR.CoreUtils;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerMovement : MonoBehaviour
{
    // for locomotion
    public Transform forwardDirection;
    // positions prev and current
    private Vector3 positionPreviousPlayer;
    private Vector3 positionPreviousLeftHand;
    private Vector3 positionPreviousRightHand;
    private Vector3 positionCurrentPlayer;
    private Vector3 positionCurrentLeftHand;
    private Vector3 positionCurrentRightHand;

    // speed
    public float speed;
    private float handSpeed = 1;
    public float handSpeedThreshold = 1f;  // Adjust this value based on your requirements
    public float smoothingFactor = 1f;


    private PhotonView photonView;

    public GameObject xrOrigin;
    public GameObject xrCamera;
    public GameObject xrLeftHand;
    public GameObject xrRightHand;

    public LayerMask obstacleLayer;  // Layer mask for obstacles

    // Bounce-back parameters
    public float bounceForce = 1f;

    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();

        //set previous positions
        positionPreviousPlayer = xrCamera.transform.position;
        positionPreviousLeftHand = xrLeftHand.transform.position;
        positionPreviousRightHand = xrRightHand.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // movement stuff

        // Check for obstacles using Physics.CheckBox
        // Adjust the size and position based on your XR rig's collider size and position
        bool isCollidingWithObstacle = Physics.CheckBox(
            xrOrigin.transform.position,  // Center of the checkbox
            new Vector3(0.35f, 1f, 0.35f),   // Half extents of the checkbox (adjust based on collider size)
            forwardDirection.transform.rotation,  // Rotation of the checkbox
            obstacleLayer  // Layer mask for obstacles
        );

        // If colliding with an obstacle, apply a bounce-back force
        if (isCollidingWithObstacle)
        {
            BounceBack();
            return;
        }

        // get forward direction
        // currently from camera
        float yRotation = xrCamera.transform.eulerAngles.y;
        forwardDirection.transform.eulerAngles = new Vector3(0, yRotation, 0);

        // get positons of hands
        positionCurrentLeftHand = xrLeftHand.transform.position;
        positionCurrentRightHand = xrRightHand.transform.position;

        // position of player
        positionCurrentPlayer = xrCamera.transform.position;

        // get distance the hands and player has moved from last frame
        // only movement in y direction
        var leftHandDistanceMoved = Mathf.Abs(positionPreviousLeftHand.y - positionCurrentLeftHand.y);
        var rightHandDistanceMoved = Mathf.Abs(positionPreviousRightHand.y - positionCurrentRightHand.y);

        // hand speed
        handSpeed = (leftHandDistanceMoved + rightHandDistanceMoved) * 1000;
        Debug.Log("hand:" + handSpeed);
        if (Time.timeSinceLevelLoad > 0.01f && leftHandDistanceMoved > 0.025 && rightHandDistanceMoved > 0.025 )
        {
            Vector3 moveAhead = forwardDirection.transform.forward * handSpeed / 1000 * speed * Time.deltaTime;
            Vector3 targetPosition = xrOrigin.transform.position + moveAhead;

            // Smoothly interpolate between current and target positions
            xrOrigin.transform.position = Vector3.Lerp(xrOrigin.transform.position, targetPosition, smoothingFactor);
        }

        // set previous position for next frame
        positionPreviousLeftHand = positionCurrentLeftHand;
        positionPreviousRightHand = positionCurrentRightHand;
        positionPreviousPlayer = positionCurrentPlayer;
       
    }

    // Function to apply a bounce-back force
    private void BounceBack()
    {
        Vector3 bounceDirection = -forwardDirection.transform.forward;  // Bounce in the opposite direction
        Vector3 bounceForceVector = bounceDirection * bounceForce;
        xrOrigin.transform.Translate(bounceForceVector * Time.deltaTime, Space.World);
    }


}


