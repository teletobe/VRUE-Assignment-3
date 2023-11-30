using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class NetworkPlayerSpawner : MonoBehaviourPunCallbacks
{
    private GameObject spawnedPlayerPrefab;

    public GameObject xrOrigin;
    private GameLogic gameLogic;

    public void Start()
    {
    }
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();

        // track
        Vector3 distance = new Vector3((PhotonNetwork.PlayerList.Length -1) * 10, 0, 0);

        xrOrigin.transform.position = distance;
        // player
        spawnedPlayerPrefab = PhotonNetwork.Instantiate("Prefabs/Network Player", distance, Quaternion.identity);

    }

    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        PhotonNetwork.Destroy(spawnedPlayerPrefab);
    }
}
