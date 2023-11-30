using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.InputSystem;
public class GameLogic : MonoBehaviour, IPunObservable
{
    // game variables
    public bool startGame = false;
    public bool gameEnded = false;
    public bool positionRestarted = false;

    // custom input
    public InputActionReference restartReference = null;
    public InputActionReference startReference = null;

    private GameObject xrOrigin;
    private GameObject xrCamera;

    public GameObject finishPlate = null; // will be assigned when player is spawned

    public Vector3 startPosition;
    private PhotonView photonView;

    private void Awake()
    {
        restartReference.action.started += RestartPosition;
        startReference.action.started += StartGame;
    }

    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();

        xrOrigin = GameObject.Find("XR Origin");
        xrCamera = GameObject.Find("XR Origin/Camera Offset/Main Camera");
    }

    // Update is called once per frameahja
    void Update()
    {
        if (gameEnded || positionRestarted)
        {
            startGame = false;
        }

        // rough estimate does not quite work yet
        // maybe use finishplate?
        if (xrOrigin.transform.position.z >= 30)
        {
            gameEnded = true;
        }

        if (positionRestarted)
        {
            // to preserve y value of camera
            Vector3 newPosition = new Vector3(startPosition.x, xrOrigin.transform.position.y, startPosition.z);
            xrOrigin.transform.position = newPosition;

            positionRestarted = false;
        }

    }

    public void StartGame(InputAction.CallbackContext ctx)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            startGame = true;
            gameEnded = false;
        }
    }


    private void RestartPosition(InputAction.CallbackContext ctx)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            positionRestarted = true;
            gameEnded = false;
            startGame = false;
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        // only the masterclient can start and restart the game
        if (PhotonNetwork.IsMasterClient)
        {
            if (stream.IsReading)
            {
                startGame = (bool)stream.ReceiveNext();
                positionRestarted = (bool)stream.ReceiveNext();
            }
            else if (stream.IsWriting)
            {
                stream.SendNext(startGame);
                stream.SendNext(positionRestarted);
            }
        }

        // everyone can win the game
        if (stream.IsReading)
        {
            gameEnded = (bool)stream.ReceiveNext();
        }
        else if (stream.IsWriting)
        {
            stream.SendNext(gameEnded);
        }
    }
}
