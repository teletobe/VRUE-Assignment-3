using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.InputSystem;
using Photon.Pun;
using System.Linq;

public class ResetPlayers : MonoBehaviour
{
    public Vector3 localStartPos;
    public GameObject xrOrigin;

    private bool gameStarted = false;
    private bool gameEnded = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // find all players
        GameObject[] playerGameObjects;
        playerGameObjects = GameObject.FindGameObjectsWithTag("Player");

        //check all network players if isReady is true
        List<PlayerStatus> statusList = new List<PlayerStatus>();
        foreach (GameObject player in playerGameObjects)
        {
            statusList.Add(player.GetComponent<NetworkPlayerScript>().status);

            // get start position
            if (player.GetComponent<NetworkPlayerScript>().isLocal)
            {
                localStartPos = player.GetComponent<NetworkPlayerScript>().startPosition;
            }

            // win condition
            if (!gameEnded && gameStarted)
            {
                GameObject head = player.transform.GetChild(0).gameObject;
                if (head.transform.position.z >= 30)
                {
                    player.GetComponent<NetworkPlayerScript>().status = PlayerStatus.hasWon;
                    gameEnded = true;
                }
                
            }
            

        }

        // if all players isReady then reset position
        if (statusList.All(x => x == PlayerStatus.isReady) && !gameStarted){
            resetXRrig();
            gameStarted = true;
            gameEnded = false;
        }



        if (gameEnded)
        {
            gameStarted = false;
            gameEnded = false;

            // find all players and set them to lost if they lost
            playerGameObjects = GameObject.FindGameObjectsWithTag("Player"); foreach (GameObject player in playerGameObjects)
            {
                GameObject head = player.transform.GetChild(0).gameObject;

                if (head.transform.position.z < 30 && !((player.GetComponent<NetworkPlayerScript>().status) == PlayerStatus.hasWon))
                {
                    player.GetComponent<NetworkPlayerScript>().status = PlayerStatus.hasLost;
                }
            }
        }
    }

    private void resetXRrig()
    {
        xrOrigin.transform.position = localStartPos;
        Debug.Log("Reset XRRig.");
    }



}
