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
    public GameObject xrOriginCamera;


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
            bools.Add(player.GetComponent<NetworkPlayer>().isReady);
            if (player.GetComponent<NetworkPlayer>().isLocal)
            {
                localStartPos = player.GetComponent<NetworkPlayer>().startPosition;
            }
        }

        if (!bools.Contains(false)){
            resetXRrig();
            resetGame = true;
            foreach (GameObject player in gos)
            {
                player.GetComponent<NetworkPlayer>().isReady = false;
            }
        }

       
        resetGame = false;
        // reset all network players isReady bool to false
    }

    private void resetXRrig()
    {
        xrOriginCamera.transform.position = localStartPos;
    }


}
