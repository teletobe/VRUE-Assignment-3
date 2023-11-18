using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using Photon.Pun;
using Unity.XR.CoreUtils;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class NetworkPlayerScript : MonoBehaviour, IPunObservable
{
    // clone
    public Transform head;
    public Transform body;
    public Transform leftHand;
    public Transform rightHand;
    private PhotonView photonView;

    // original xr gameobjct
    private GameObject xrCamera;
    private GameObject xrLeftHand;
    private GameObject xrRightHand;


    public Vector3 startPosition;
    public bool isLocal;
    public bool isReady;

    public InputActionReference startReference = null;

    private void Awake()
    {
       startReference.action.started += StartGame;
    }


    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();

        // get xr game objects
        xrCamera = GameObject.Find("XR Origin/Camera Offset/Main Camera");
        xrLeftHand = GameObject.Find("XR Origin/Camera Offset/Left Controller");
        xrRightHand = GameObject.Find("XR Origin/Camera Offset/Right Controller");
        isReady = false;
        startPosition = xrCamera.transform.position;
        /*
        if (photonView.IsMine)
        {
            startPosition = xrCamera.transform.position;
        }
        else
        {
            startPosition = transform.position;
        }*/

    }

    // Update is called once per frame
    void Update()
    {
        // synchronize xr objects to clones
        if (photonView.IsMine)
        {
            // don't show spawned game objects stuff it's yourself
            // rightHand.gameObject.SetActive(false);
            // leftHand.gameObject.SetActive(false);
            // head.gameObject.SetActive(false);

            isLocal = true;

            MapXRPosition(head, xrCamera);
            MapXRPosition(body, xrCamera);
            MapXRPosition(leftHand, xrLeftHand);
            MapXRPosition(rightHand, xrRightHand);   
        }
    }

    void MapXRPosition(Transform target, GameObject gameObject)
    {
        target.position = gameObject.transform.position;

        if (target != body)
        {
            target.rotation = gameObject.transform.rotation;
        }
        else
        {
            target.rotation = Quaternion.identity;
        }
    }

    
    public void StartGame(InputAction.CallbackContext ctx)
    {
        /*
        if (photonView.IsMine)
        {
            isReady = true;
        }*/
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsReading)
        {
            isReady = (bool)stream.ReceiveNext();
        }
        else if (stream.IsWriting)
        {
            stream.SendNext(isReady);
        }
    }


}
