using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using Photon.Pun;
using Unity.XR.CoreUtils;
using UnityEngine.XR.Interaction.Toolkit;

public class NetworkPlayer : MonoBehaviour
{

    public Transform head;
    public Transform leftHand;
    public Transform rightHand;
    private PhotonView photonView;
    
    // for locomotion
    public Transform forwardDirection;
    // positions prev and current
    private Vector3 positionPreviousPlayer;
    private Vector3 positionPreviousLeftHand;
    private Vector3 positionPreviousRightHand;
    private Vector3 positionCurrentPlayer;
    private Vector3 positionCurrentLeftHand;
    private Vector3 positionCurrentRightHand;

    private XROrigin xrRig;

    // speed
    public float speed;
    private float handSpeed = 1;


    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();

        //set previous positions
        positionPreviousPlayer = head.transform.position;
        positionPreviousLeftHand = leftHand.transform.position;
        positionPreviousRightHand = rightHand.transform.position;

        xrRig = FindObjectOfType<XROrigin>();
        if (xrRig == null)
        {
            Debug.LogError("XR Rig not found. Make sure it is present in the scene.");
        }

    }

    // Update is called once per frame
    void Update()
    {
        // don't show spawned game objects stuff it's yourself
        if (photonView.IsMine)
        {
            rightHand.gameObject.SetActive(false);
            leftHand.gameObject.SetActive(false);
            head.gameObject.SetActive(false);
            MapPosition(head, XRNode.Head);
            MapPosition(leftHand, XRNode.LeftHand);
            MapPosition(rightHand, XRNode.RightHand);
        }


        // movement stuff
        if (photonView.IsMine)
        {
            // get forward direction
            // currently from camera
            float yRotation = head.transform.eulerAngles.y;
            forwardDirection.transform.eulerAngles = new Vector3(0, yRotation, 0);

            // get positons of hands
            positionCurrentLeftHand = leftHand.transform.position;
            positionCurrentRightHand = rightHand.transform.position;

            // position of player
            positionCurrentPlayer = head.transform.position;

            // get distance the hands and player has moved from last frame
            // only movement in y direction
            var leftHandDistanceMoved = Mathf.Abs(positionPreviousLeftHand.y - positionCurrentLeftHand.y);
            var rightHandDistanceMoved = Mathf.Abs(positionPreviousRightHand.y - positionCurrentRightHand.y);

            //Debug.Log("left:" + leftHandDistanceMoved);
            //Debug.Log("right:" + rightHandDistanceMoved);

            // hand speed
            handSpeed = (leftHandDistanceMoved + rightHandDistanceMoved) / 2;

            if (Time.timeSinceLevelLoad > 1f)
            {
                Vector3 moveAhead = forwardDirection.transform.forward * handSpeed * speed * Time.deltaTime;
                transform.position += moveAhead;
                //Debug.Log(moveAhead);
                xrRig.transform.position += moveAhead;

            }

            // set previous position for next frame
            positionPreviousLeftHand = positionCurrentLeftHand;
            positionPreviousRightHand = positionCurrentRightHand;
            positionPreviousPlayer = positionCurrentPlayer;
        }

    }

    void MapPosition(Transform target, XRNode node)
    {
        InputDevices.GetDeviceAtXRNode(node).TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 position);
        InputDevices.GetDeviceAtXRNode(node).TryGetFeatureValue(CommonUsages.deviceRotation, out Quaternion rotation);

        target.position = position;
        target.rotation = rotation;

    }

}
