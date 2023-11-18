using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.InputSystem;
using Photon.Pun;


public class ResetPlayers : MonoBehaviour
{
    public Vector3 localStartPos;
    public GameObject xrOrigin;

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
        List<bool> bools = new List<bool>();
        foreach (GameObject player in playerGameObjects)
        {
            bools.Add(player.GetComponent<NetworkPlayerScript>().isReady);

            // get start position
            if (player.GetComponent<NetworkPlayerScript>().isLocal)
            {
                localStartPos = player.GetComponent<NetworkPlayerScript>().startPosition;
            }
        }

        // if all players isReady then reset position
        if (!bools.Contains(false)){
            resetXRrig();
            foreach (GameObject player in playerGameObjects)
            {
                player.GetComponent<NetworkPlayerScript>().isReady = false;
            }
        }
    }

    private void resetXRrig()
    {
        xrOrigin.transform.position = localStartPos;
        Debug.Log("´Reset XRRig.");
    }


}
