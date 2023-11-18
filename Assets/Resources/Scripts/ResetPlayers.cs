using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.InputSystem;
using Photon.Pun;


public class ResetPlayers : MonoBehaviour
{

    public bool resetGame;
    public Vector3 localStartPos;
    public GameObject xrOrigin;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Player");
        //check all network players if all are true
        //then resetGame = true;
        List<bool> bools = new List<bool>();
        foreach (GameObject player in gos)
        {
            bools.Add(player.GetComponent<NetworkPlayerScript>().isReady);
            if (player.GetComponent<NetworkPlayerScript>().isLocal)
            {
                localStartPos = player.GetComponent<NetworkPlayerScript>().startPosition;
            }
        }

        if (!bools.Contains(false)){
            resetXRrig();
            resetGame = true;
            foreach (GameObject player in gos)
            {
                player.GetComponent<NetworkPlayerScript>().isReady = false;
            }
        }

       
        resetGame = false;
        // reset all network players isReady bool to false
    }

    private void resetXRrig()
    {
        xrOrigin.transform.position = localStartPos;
        Debug.Log("´Reset XRRig.");
    }


}
