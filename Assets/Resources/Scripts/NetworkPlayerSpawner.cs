using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class NetworkPlayerSpawner : MonoBehaviourPunCallbacks
{
    private GameObject spawnedPlayerPrefab;
    private GameObject spawnedTrackPrefab;

    public GameObject xrOrigin;
    private GameLogic gameLogic;

    public void Start()
    {
        gameLogic = xrOrigin.GetComponent<GameLogic>();
    }
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();

        // track
        Vector3 distance = new Vector3((PhotonNetwork.PlayerList.Length -1) * 10, 0, 0);
        spawnedTrackPrefab = PhotonNetwork.Instantiate("Prefabs/Track", distance, Quaternion.identity);

        // player
        gameLogic.startPosition = distance;
        spawnedPlayerPrefab = PhotonNetwork.Instantiate("Prefabs/Network Player", distance, Quaternion.identity);
        Debug.Log(transform.position);

        gameLogic.finishPlate = GameObject.FindGameObjectsWithTag("Finish")[0];

    }

    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        PhotonNetwork.Destroy(spawnedPlayerPrefab);
        PhotonNetwork.Destroy(spawnedTrackPrefab);

    }
}
