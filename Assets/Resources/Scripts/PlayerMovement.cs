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

    private PhotonView photonView;

    public GameObject xrOrigin;
    public GameObject xrCamera;
    public GameObject xrLeftHand;
    public GameObject xrRightHand;

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
        handSpeed = (leftHandDistanceMoved + rightHandDistanceMoved) / 2;

        if (Time.timeSinceLevelLoad > 1f)
        {
            Vector3 moveAhead = forwardDirection.transform.forward * handSpeed * speed * Time.deltaTime;
            //transform.position += moveAhead;
            //Debug.Log(moveAhead);
            xrOrigin.transform.position += moveAhead;
        }

        // set previous position for next frame
        positionPreviousLeftHand = positionCurrentLeftHand;
        positionPreviousRightHand = positionCurrentRightHand;
        positionPreviousPlayer = positionCurrentPlayer;
       
    }

}


