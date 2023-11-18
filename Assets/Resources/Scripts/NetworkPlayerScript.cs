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
    public GameObject head;
    public GameObject body;
    public GameObject leftHand;
    public GameObject rightHand;
    private PhotonView photonView;

    // original xr gameobjct
    private GameObject xrCamera;
    private GameObject xrLeftHand;
    private GameObject xrRightHand;

    public Vector3 startPosition;
    public bool isLocal;
    public bool isReady;
    List<Renderer> rendererList = new List<Renderer>();

    Color originalColor = new Color(0.0f, 0.8f, 1.0f, 0.7f);

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

        // get the Renderer components
        rendererList.Add(head.GetComponentInChildren<Renderer>());
        rendererList.Add(body.GetComponentInChildren<Renderer>());

        isReady = false;

        startPosition = transform.position; // should work like this

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
        if (isReady)
        {
            foreach(Renderer r in rendererList)
            {
                r.material.SetColor("_Color", Color.yellow);
            }
        }
        else
        {
            foreach (Renderer r in rendererList)
            {
                r.material.SetColor("_Color", originalColor);
            }
        }

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

    void MapXRPosition(GameObject target, GameObject gameObject)
    {
        target.transform.position = gameObject.transform.position;

        if (target != body)
        {
            target.transform.rotation = gameObject.transform.rotation;
        }
        else
        {
            target.transform.rotation = Quaternion.identity;
        }
    }

    
    public void StartGame(InputAction.CallbackContext ctx)
    {
        if (photonView.IsMine)
        {
            Debug.Log("trigger");
            isReady = true;
        }
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
